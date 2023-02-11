using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Transactions;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;

namespace featherlight_client
{
    public class MsgEnv
    {
        static void Quit()
        {
            Console.WriteLine("Exiting...");
            Thread.Sleep(800);
            Environment.Exit(0);
        }

        public static string othMsg = null;
        public static string bckMsg = null;
        public static bool diffMsg = false;

        void MsgScr(string loginUser, string da_ip, int portz1, int portz2)
        {
            string loginU = loginUser;
            string ab = "[" + loginU + "] (YOU): ";
            string c = "[" + loginU + "]: ";

            Thread bgListen = new Thread(new ThreadStart(BGListen));
            Thread bgSync = new Thread(new ThreadStart(BGSync));

            void BGListen()
            {
                while (true)
                {
                    bckMsg = othMsg;
                    othMsg = UDPListener.Main(portz1);
                    if (bckMsg == othMsg)
                    {
                        diffMsg = false;
                    }
                    else
                    {
                        diffMsg = true;
                    }
                }
            }

            

            void BGSync()
            {
                Thread.Sleep(520);
                
                while (true)
                {
                    if (diffMsg)
                    {
                        Console.WriteLine(othMsg);
                    }
                    else
                    {
                        Console.Clear();
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(ab);
                    Console.ForegroundColor = ConsoleColor.White;
                    string? input = Console.ReadLine();
                    if (input == "!exit")
                    {
                        Console.Clear();
                        Quit();
                    }
                    else if (input == "!help")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("!help - Shows commands.");
                        //custom commands here
                        Console.WriteLine("!leave - Disconnects you, returns back to menu.");
                        Console.WriteLine("!online - Check if you're online. (USELESS)");
                        //end of custom commands
                        Console.WriteLine("!exit - Quits app.");
                    }
                    else if (input == "!leave")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Disconnecting...");
                        Console.WriteLine("Online Not Done yet!");
                        Thread.Sleep(1500);
                        Console.Clear();
                        return;
                    }
                    else if (input == "!online")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Should be online.");
                    }
                    else
                    {
                        //assign ip and port
                        string sendInput = c + input;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(c);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(input);
                        Console.ForegroundColor = ConsoleColor.White;
                        SendPeer.Main(da_ip, portz2, sendInput);
                        Console.Clear();
                    }
                }
            }

            
            bgListen.Start();
            bgSync.Start();
            Thread.Sleep(300000);

        }//NOT USABLE

        void FrontSync(string loginUser, string da_ip, int portz1, int portz2)
        {
            //Thread1 - Syncs the connection
            Thread bgSync = new Thread(new ThreadStart(BGSYNC));

            void BGSYNC()
            {
                while(true)
                {

                }
            }

            //Thread2 - Sends the messages

            //Thread3 - Listens for Messages
            Thread bgListen = new Thread(new ThreadStart(BGListen));

            void BGListen()
            {
                while (true)
                {
                    bckMsg = othMsg;
                    othMsg = UDPListener.Main(portz1);
                    if (String.Equals(bckMsg, othMsg))
                    {
                        diffMsg = false;
                    }
                    else
                    {
                        diffMsg = true;
                    }
                }
            }

            //FrontEnd - Displays new messages and connection updates
            while(true)
            {
               
            }
        }

        public void Main()
        {
            //set vars
            string ur_user = "DefaultUser";
            string? da_ip = null;
            int da_port1 = 20; //1 used for listening LISTENP -> friends SENDERP
            int da_port2 = 80; //2 used for sending SENDERP -> friends LISTENP

            //set new username
            Console.WriteLine("Enter your Username. (Default is 'DefaultUser')");
            string? tur_user = Console.ReadLine();
            if (tur_user != null)
            {
                ur_user = tur_user;
            }
            else
            {
                ur_user = "DefaultUser";
            }

            //set ip
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Enter IP (REQUIRED)");
                string? t_ip = Console.ReadLine();
                if (t_ip != null)
                {
                    da_ip = t_ip;
                    break;
                }
                else
                {
                    Console.WriteLine("IP Can't be null here.");
                    Thread.Sleep(1500);
                }
            }

            //set port
            Console.Clear();
            Console.WriteLine("Enter port config.");
            Console.WriteLine("If your friend chooses 1, then choose 2 vice versa.");
            Console.WriteLine("DO NOT CHOOSE THE SAME CONFIG.");
            Console.WriteLine("Config 1 - ListenPort: 40, SendPort: 20");
            Console.WriteLine("Config 2 - ListenPort: 20, SendPort: 40");
            string? port_config = Console.ReadLine();
            if (port_config == "2")
            {
                da_port1 = 20;
                da_port2 = 40;
            }
            else if (port_config == "1")
            {
                da_port1 = 40;
                da_port2 = 20;
            }
            else if (port_config == "3")
            {
                Console.WriteLine("Enter First Port. (Listener)");
                string? stda_port1 = Console.ReadLine();
                if (stda_port1 != null)
                {
                    try
                    {
                        int tda_port = Int32.Parse(stda_port1);
                        da_port1 = tda_port;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    da_port1 = 40;
                    da_port2 = 20;
                }

                Console.WriteLine("Enter Second Port. (Sender)");
                string? stda_port2 = Console.ReadLine();
                if (stda_port2 != null)
                {
                    try
                    {
                        int tda_port2 = Int32.Parse(stda_port2);
                        da_port2 = tda_port2;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    da_port1 = 40;
                    da_port2 = 20;
                }
            }

            Console.WriteLine("Port1 = " + da_port1);
            Console.WriteLine("Port2 = " + da_port2);
            Console.ReadKey();

            MsgScr(ur_user, da_ip, da_port1, da_port2);
        }//Starter
    }
}
