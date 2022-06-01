using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using CowboysManager.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowboysManager
{
    public class CowboysManager
    {
        private Random random = new Random();
        //private static UserService userService = new UserService();
        private readonly IUserService _userService;
        private readonly IPlatformService _platformService;
        private readonly IAuthentication _authService;
        private Encryption encryption = new Encryption();
        private User loggedInUser;
        
        private int incrementalDelay = 1000;
        private int delay;
        public CowboysManager(IUserService userService, IPlatformService platformService, IAuthentication authentcation)
        {
            _userService = userService;
            _platformService = platformService;
            _authService = authentcation;


            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            var select = Console.ReadLine();
            switch (select)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
            }
        }


        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Options:");
            Console.WriteLine("1: View your saved platforms");
            Console.WriteLine("2: Add new platform");
            Console.WriteLine("3: Exit the application");

            string service = Console.ReadLine();
            switch (service)
            {
                case "1":
                    Console.WriteLine("");
                    ViewServices();
                    break;

                case "2":
                    AddService();
                    break;

                case "3":
                    Console.WriteLine("Closing application... Have a nice day!");
                    Environment.Exit(0);

                    break;
            }

        }

        public void AddService()
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the Platform you would like to add ");
            string PlatformName = Console.ReadLine();
            Console.WriteLine("Enter Platform Username ");
            string PlatformUsername = Console.ReadLine();
            Console.WriteLine("Random password for " + PlatformName + " has beern generated:");
            var password = RandomString(16);
            Console.WriteLine(password);
            string salttostring = Encoding.ASCII.GetString(loggedInUser.PasswordSalt);
            var key = salttostring.Substring(0, 32);
            var encryptedString = Encryption.EncryptString(key, password);
            var platform = new Platform()
            {
                Name = PlatformName,
                Username = PlatformUsername,
                EncryptedPassword = encryptedString,
                UserId = loggedInUser.Id
            };
            try
            {
                _platformService.CreatePlatform(platform);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
                AddService();
            }

            Console.WriteLine("Platform created");
            Thread.Sleep(2000);
            Menu();
        }

        public void ViewServices()
        {
            var Platform = _platformService.GetAllPlatformsByUserId(loggedInUser.Id).ToList();
            string salttostring = Encoding.ASCII.GetString(loggedInUser.PasswordSalt);
            var key = salttostring.Substring(0, 32);
            Console.WriteLine("Select a platform");
            foreach (var platform in Platform)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Platform name: " + platform.Name);
                Console.WriteLine("Platform username: " + platform.Username);
                var decrytedString = Encryption.DecryptString(key, platform.EncryptedPassword);
                Console.WriteLine("Platform password: " + decrytedString);
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("Press '1' to go back to the main menu.");
            Console.WriteLine("Press '3' to exit the application.");

            string readkey = Console.ReadLine();
            switch (readkey)
            {

                case "1":

                    Menu();

                    break;


                case "3":

                    Console.WriteLine("Closing application...");

                    Environment.Exit(0);

                    break;

            }
        }

        public void Login()
        {
            Console.Clear();
            Console.WriteLine("Please log in. ");
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();

            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                Console.WriteLine("Incorrect username or password. Please try again.");
                Thread.Sleep(1000);
                Login();
            }
            if (_authService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                loggedInUser = user;
                Console.WriteLine("Logging in...");
                Thread.Sleep(1000);
                CowboysLogo();
            }
            else
            {
                Console.WriteLine("Incorrect username or password. Please try again");
                delay = incrementalDelay * 2;
                incrementalDelay = delay;
                Thread.Sleep(incrementalDelay);
                Login();
            }
        }

        public void Register()
        {
            Console.Clear();
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();

            User user = _authService.PasswordHasher(username, password);
            
            try
            {
                _userService.CreateUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
                Register();
            }

            Console.WriteLine("Account registered. Please wait...");
            Thread.Sleep(2000);
            Login();
        }
        //Flyt til Encryption
        public string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void CowboysLogo()
        {
            Console.Clear();
            Console.WriteLine("                             Welcome to Cowboys Password!                       ");
            Console.WriteLine("");
            Console.WriteLine("                                         /#@///&@/                              ");
            Console.WriteLine("                                    .//@@@&&&@@@/@@/                            ");
            Console.WriteLine("                               *//@@@@&&@@&&&&@@@/@@/                           ");
            Console.WriteLine("                            /&@@@&&@@@@@&&&&&&&@@@/@%/                          ");
            Console.WriteLine("                           /@@&&&&&&&&&&&&&&&&&&@@@/@/        /(@@@/            ");
            Console.WriteLine("                          /@@&&&&&&&&&&&&&&&&&&&@@@/%@/    //@@&&&@/            ");
            Console.WriteLine("               //@@@@@@@@@@@@&&&&&&&@@@@///@@@&%&@@@@@/*//@@@&&@&&@/            ");
            Console.WriteLine("             /&@@&&&&&&&@@&@@@@////////////////#@@@@@@@@@&&&@@@&@@/             ");
            Console.WriteLine("             /@@&&&&&&&&&&&&@@@@@@@@@&&&&@@&&&&&&&&&&&&&@@@@@@&@//              ");
            Console.WriteLine("             /@@&&@&&&&&&&&@@@@@@@@ ,@@ @@@@@@@@@@@@@@@@@@@@&@#/                ");
            Console.WriteLine("             /@@&&&&&&&&@@@  .@@@@    @@@@@@@@@@@@@@@@@@@@@@/,                  ");
            Console.WriteLine("             *(@@&&@&&&@@@@   /@@     @@@@@@@&&&&@@@@@@@//                      ");
            Console.WriteLine("              */@@&&@@&@@@@@@@@@&&&&&&&&&&&&&//&&@@@@/                          ");
            Console.WriteLine("                /@@@&@@@@@@@&@@&&&&&&&&&&&&&&//&&@@@@/                          ");
            Console.WriteLine("                  /@@@@@@@@@@@@&&&@&&&&&&&&&&/&&@@@@/                           ");
            Console.WriteLine("                    //@@@@@@@@@@@@&&&&&&&&&&(/&&@@@/                            ");
            Console.WriteLine("                        ///@@@@@@@@@@&&&&&&&/&&@@@/                             ");
            Console.WriteLine("                              //@@@@@@@&&&&/&@@@@/                              ");
            Console.WriteLine("                                  /@@@@@&&&&@@@&/                               ");
            Console.WriteLine("                                    /@@@@&@@@@/                                 ");
            Console.WriteLine("                                     /@@@@@@@/                                  ");
            Console.WriteLine("                                      /@@@@/                                    ");
            Console.WriteLine("                                      *%@/                                      ");
            Console.WriteLine("                                       /                                        ");
            Thread.Sleep(3000);
            Menu();
        }

    }
}
