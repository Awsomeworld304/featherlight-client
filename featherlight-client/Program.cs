using System;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

string? loginUser = "Username";
string? loginPass = "Password";
bool hasLogin = false;
bool DebugMode = false;

#if DEBUG
Console.WriteLine("Debug Mode");
Console.WriteLine("");
DebugMode = true;
#endif

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
            while ((s = sr.ReadLine()) != null)
            {
                string? loginUser = s;
                string? loginPass = s;
                if (loginUser != null && loginPass != null)
                {
                    Console.WriteLine(loginUser, " ", loginPass);
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

preLogin();

void Auth(string usr, string pas, bool newUsr, bool debugMode)
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
        using (StreamWriter sw = File.CreateText(fileN))
        {
            Console.WriteLine("Writing info");
            sw.WriteLine(usr);
            sw.WriteLine(pas);
            Console.WriteLine("Write done");
        }

        using (StreamReader sr = File.OpenText(fileN))
        {
            string? s = "";
            while ((s = sr.ReadLine()) != null)
            {
                string? un = s;
                string? us = s;
                if (debugMode)
                {
                    Console.WriteLine(un, " ", us);
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
                Console.WriteLine("Read the file.");
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
        Console.ReadKey();
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
        Console.WriteLine("Not implamented yet!");
        Thread.Sleep(2500);
        Environment.Exit(0);
    }

    else if (loginChoice == "4")
    {
        Console.WriteLine("Exiting...");
        Thread.Sleep(800);
        Environment.Exit(0);
    }

}

while (hasLogin)
{
    Console.WriteLine("Logging you in. Please Wait.");
    
    Console.ReadKey();
}