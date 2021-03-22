using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroudColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private string _connectionString;

        public BackgroudColorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Color Menu");
            Console.WriteLine(" 1) Dark Magenta");
            Console.WriteLine(" 2) Dark Cyan");
            Console.WriteLine(" 3) Dark Yellow");
            Console.WriteLine(" 4) Dark Green");
            Console.WriteLine(" 5) Dark Blue");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    DarkMagenta();
                    return this;
                case "2":
                    DarkCyan();
                    return this;
                case "3":
                    DarkYellow();
                    return this;
                case "4":
                    DarkGreen();
                    return this;
                case "5":
                    DarkBlue();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void DarkMagenta()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
        }

        private void DarkCyan()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
        }


        private void DarkYellow()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
        }

        private void DarkGreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
        }
        private void DarkBlue()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
        }

    }
}

