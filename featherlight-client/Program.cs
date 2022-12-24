using Microsoft.VisualBasic;
using System;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

string loginUser = "";
string loginPass = "";
bool hasLogin = false;
bool DebugMode = false;
bool ran1 = false;
bool authY = false;

#if DEBUG
Console.WriteLine("Debug Mode");
Console.WriteLine("");
DebugMode = true;
#endif

void MsgScr()
{
    string loginU = loginUser;
    string ab = "[" + loginU + "] Write: ";
    string c = "[" + loginU + "]: ";
    Thread.Sleep(520);
    Console.WriteLine(ab);
    string? input = Console.ReadLine();
    if (input == "exit")
    {
        Console.Clear();
        Quit();
    }
    else
    {
        Console.WriteLine(c + input);
    }
}

void MsgOpt()
{
    Console.WriteLine("Choose Chat Option");
    Console.WriteLine("1) Debug Chat");
    Console.WriteLine("2) Exit");
    string? mC2 = Console.ReadLine();
    
    if (mC2 == "1")
    {
        MsgScr();
    }

    else if (mC2 == "2")
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
    if(loginUser == null && loginPass == null)
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
                if (ran1 != true)
                {
                    loginUser = s;
                    ran1 = true;
                    preLogin();
                }
                else if (ran1)
                {
                    loginPass = s;
                }
                
                if (loginUser != null && loginPass != null)
                {
                    Console.WriteLine(loginUser, " and ", loginPass);
                    hasLogin = true;
                }
            }
        }
    }
    else
    {
        StreamWriter sw = File.CreateText(fileN);
        preLogin();
    }
}

loginState();
preLogin();

void Auth(string? usr, string? pas, bool newUsr, bool debugMode)
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
            sw.WriteLine(pas);
            Console.WriteLine("Write done");
        }

        //Read data
        using (StreamReader sr = File.OpenText(fileN))
        {
            string? s = "";
            if ((s = sr.ReadLine()) != null)
            {
                string compUsr = "";
                string compPas = "";
                while (ran1 != true)
                {
                    compUsr = s;
                    ran1 = true;
                    Console.WriteLine("The scanned usr " + compUsr);
                }
                if (ran1)
                {
                    compPas = s;
                    Console.WriteLine("The scaned pass " + compPas);
                }

                if (compUsr == usr && compPas == pas)
                {
                    authY = true;
                }
                else {
                authY = false;
                }
            }
        }
    }
    else
    {
        //Reading LAG file.
        //It prints it out in debug mode for testing.
        using (StreamReader sr = File.OpenText(fileN))
        {
            string? s = "";
            while ((s = sr.ReadLine()) != null)
            {
                if (debugMode)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}

while (hasLogin != true)
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
        Console.WriteLine("Enter Password");
        string? passW = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Authenticating. Please Wait.");
        Auth(usrN, passW, false, DebugMode);
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
        Console.WriteLine("Enter New Password");
        string? passW = Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Authenticating new account. Please Wait.");
        Auth(usrN, passW, true, DebugMode);
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