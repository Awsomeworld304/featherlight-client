using featherlight_client;
using System;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

string? loginUser = "";
bool userY = false;
bool hasLogin = false;
bool DebugMode = false;
bool authY = false;
bool onlineOFF = true;

//warning
if (onlineOFF == true)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("WARNING:");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("This is schizophrenia simulator for now.");
    Console.WriteLine("The online part is not done yet." + "\n");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Press any key to continue.");
    Console.ReadKey();
    Console.Clear();
}
else
    onlineOFF = false;

#if DEBUG
Console.WriteLine("Debug Mode");
Console.WriteLine("");
DebugMode = true;
#endif

void Connect()
{
    Console.WriteLine("1) Send" + "\n" + "2) Get");
    string? inpC = Console.ReadLine();
    if (inpC == "1")
    {
        Send();
    }

    if (inpC == "2")
    {
        Listen();
    }
}

void Listen()
{
    Console.Clear();
    Console.WriteLine("Enter Port to listen on.");
    string? new_portS = Console.ReadLine();
    int new_port = 0;
    try
    {
        int portP = Int32.Parse(new_portS);
        new_port = portP;
    }
    catch (FormatException e)
    {
        Console.WriteLine(e.Message);
    }
    //Put new connection stuff here
}

void Send()
{
    Console.Clear();
    Console.WriteLine("Enter IP");
    string? new_ip = Console.ReadLine();
    Console.WriteLine("Enter Port");
    string? new_portS = Console.ReadLine();
    int new_port = 0;
    try
    {
        int portP = Int32.Parse(new_portS);
        new_port = portP;
    }
    catch (FormatException e)
    {
        Console.WriteLine(e.Message);
    }
    Console.WriteLine("Enter Message");
    string? message = Console.ReadLine();
    //new stuff here
}

void MsgScr()
{
    Console.Clear();
    string loginU = loginUser;
    string ab = "[" + loginU + "] (YOU): ";
    string c = "[" + loginU + "]: ";
    Thread.Sleep(520);
    while (true)
    {
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
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(c);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

void MsgOpt()
{
    //config check
    //if (File.Exists(@"config.dat"))
    //{
      //  using (StreamReader sr = File.OpenText(@"config.dat"))
        //{
          //  string? s = "";
            //if ((s = sr.ReadLine()) != null)
            //{
               //help
           // }
        //}
   // }
    //else
    //{
     //   StreamWriter sw = File.CreateText(@"config.dat");
    //}

    Console.WriteLine("Choose Chat Option");
    Console.WriteLine("1) Online Chat (2-Way)");
    Console.WriteLine("2) Debug Chat (NO ONLINE)");
    Console.WriteLine("3) Online Connect Test (BROKEN)");
    Console.WriteLine("4) Exit");
    string? mC2 = Console.ReadLine();
    
    if (mC2 == "1")
    {
        var MsgEv = new MsgEnv();
        MsgEv.Main();
    }

    else if (mC2 == "2")
    {
        MsgScr();
    }

    else if (mC2 == "3")
    {
        Connect();
    }

    else if (mC2 == "4")
    {
        Quit();
    }
}

static void Quit()
{
    Console.WriteLine("Exiting...");
    Thread.Sleep(800);
    Environment.Exit(0);
}
void loginState()
{
    if(loginUser == null)
    {
        hasLogin = false;
    }
}

void preLogin()
{
    string fileN = @"LAG.dll";
    if (File.Exists(fileN))
    {
        using (StreamReader sr = File.OpenText(fileN))
        {
            string? s = "";
            if ((s = sr.ReadLine()) != null)
            {
                loginUser = s;
                
                if (loginUser != null)
                {
                    hasLogin = true;
                    userY = true;
                }
            }
            else
            {
                userY = false;
                hasLogin = false;
            }
            sr.Close();
        }
    }
    else
    {
        StreamWriter sw = File.CreateText(fileN);
        sw.Close();
        preLogin();
    }
}

void Auth(string? usr, bool newUsr, bool debugMode)
{
    //debug workaround lmao
    if (debugMode != true)
    {
        debugMode = true;
    }
    else
    {
        debugMode = true;
    }

    //LAG Login Authentication Gate :)
    string fileN = @"LAG.dll";
    if (newUsr)
    {
        //Write data
        using (StreamWriter sw = File.CreateText(fileN))
        {
            Console.WriteLine("Writing info");
            sw.WriteLine(usr);
            sw.Close();
            Console.WriteLine("Write done");
        }

        //Read data
        using (StreamReader sr = File.OpenText(fileN))
        {
            string? s = "";
            if ((s = sr.ReadLine()) != null)
            {
                string? compUsr = usr;
                Console.WriteLine(compUsr);

                if (compUsr == usr)
                {
                    authY = true;
                    loginUser = compUsr;
                }
                else {
                authY = false;
                }
            }
            sr.Close();
        }
    }
    else
    {
        //Reading LAG file.
        //Should be for auth but its not?? WTF
        //It prints it out in debug mode for testing.
        using (StreamReader sr = File.OpenText(fileN))
        {
            string? s = "";
            if ((s = sr.ReadLine()) != null)
            {
                string? compUsr = usr;
                Console.WriteLine(compUsr);

                if (compUsr == usr)
                {
                    authY = true;
                }
                else
                {
                    authY = false;
                }
            }
            sr.Close();
        }
    }
}

//Method Run Here
loginState();
preLogin();

while (hasLogin != true)
{
    if (userY)
    {
        Console.WriteLine("Please Login.");
        Console.WriteLine("1) Login");
        Console.WriteLine("2) Make Account");
        Console.WriteLine("3) Reset App Data");
        Console.WriteLine("4) Exit App");
        string? loginChoice = Console.ReadLine();
        if (loginChoice == "1")
        {
            Console.WriteLine("Enter Username");
            string? usrN = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("We don't use passwords because theres no point.");
            Console.WriteLine("All message data does not get saved, nor your creds.");
            Console.WriteLine("Your username is persistant, but you can delete that too.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Authenticating. Please Wait.");
            Auth(usrN, false, DebugMode);
            if (authY)
            {
                MsgScr();
            }
            else
            {
                hasLogin = false;
            }
        }

        else if (loginChoice == "2")
        {
            Console.WriteLine("Enter New Username");
            string? usrN = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("We don't use passwords because theres no point.");
            Console.WriteLine("All message data does not get saved, nor your creds.");
            Console.WriteLine("Your username is persistant, but you can delete that too.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Authenticating new account. Please Wait.");
            Auth(usrN, true, DebugMode);
            Thread.Sleep(750);
            Console.Clear();
        }

        else if (loginChoice == "3")
        {
            if (File.Exists(@"LAG.dll"))
            {
                File.Delete(@"LAG.dll");
                Console.WriteLine("Data cleared.");
                Quit();
            }
            else
            {
                Console.WriteLine("There is no data.");
                Quit();
            }
        }

        else if (loginChoice == "4")
        {
            Quit();
        }
    }
    else
    {
        Console.WriteLine("Enter New Username");
        string? usrN = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("We don't use passwords because theres no point.");
        Console.WriteLine("All message data does not get saved, nor your creds.");
        Console.WriteLine("Your username is persistant, but you can delete that too.");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("Authenticating new account. Please Wait.");
        Auth(usrN, true, DebugMode);
        Thread.Sleep(750);
        Console.Clear();
        userY = true;
    }

}

while (hasLogin)
{
    Console.WriteLine("Choose an option.");
    Console.WriteLine("1) Choose chat method and join.");
    Console.WriteLine("2) Options");
    Console.WriteLine("3) Reset App Data");
    Console.WriteLine("4) Exit");
    string? mC = Console.ReadLine();

    if (mC == "1")
    {
        MsgOpt();
    }

    else if (mC == "2")
    {
        Console.WriteLine("Not DONE");
        Thread.Sleep(1500);
        Quit();
    }

    else if (mC == "3")
    {
        if (File.Exists(@"LAG.dll"))
        {
            File.Delete(@"LAG.dll");
            Console.WriteLine("Data cleared.");
            Quit();
        }
        else
        {
            Console.WriteLine("There is no data.");
            Quit();
        }
    }

    else if (mC == "4")
    {
        Quit();
    }
}