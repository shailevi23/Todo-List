using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class Program
    {
        public static List<string> toDoList = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            Console.WriteLine("What do you want to do?");

            while (true)
            {
                PerformActionFromUserInput(WelcomeMenu());
            }
        }

        static char WelcomeMenu()
        {
            WelcomeMenuText();
            return SelectFromMenu();
        }

        static void WelcomeMenuText()
        {
            Console.WriteLine("[S]ee all TODOs");
            Console.WriteLine("[A]dd a TODO");
            Console.WriteLine("[R]emove a TODO");
            Console.WriteLine("[E]xit");
        }

        static char SelectFromMenu()
        {
            char input = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");

            //Check if input char is a digit, we need an alphabet input
            while (Char.IsDigit(input))
            {
                Console.WriteLine("Incorrect input\n");
                WelcomeMenuText();
                input = Console.ReadKey().KeyChar;
            }

            return input;
        }

        static void PerformActionFromUserInput(char input)
        {
            input = Char.ToLower(input);

            switch (input)
            {
                case 's':
                    SeeAllToDoList();
                    break;
                case 'a':
                    AddToDo();
                    break;
                case 'r':
                    RemoveToDo();
                    break;
                case 'e':
                    ExitApp();
                    break;
                default:
                    Console.WriteLine("Incorrect input\n");
                    break;
            }
        }

        static void SeeAllToDoList()
        {
            int listSize = toDoList.Count();

            if (listSize == 0)
            {
                Console.WriteLine("No TODOs have been added yet\n");
            }
            else
            {
                for(int i=0; i < listSize; i++)
                {
                    Console.WriteLine($"{i+1}.{toDoList[i]}");
                }

                Console.WriteLine("What do you want to do?\n");
            }
        }

        static void AddToDo()
        {
            string input = ValidateUserInputForAddingNewToDo();
            toDoList.Add(input);
            Console.WriteLine($"TODO successfully added: {toDoList[toDoList.Count() - 1]}");
        }

        static void RemoveToDo()
        {
            if (toDoList.Count == 0)
            {
                Console.WriteLine("No TODOs have been added yet\n");
                return;
            }

            int index = ValidateUserInputForRemoveToDo();

            Console.WriteLine($"TODO removed: {toDoList[index]} ");
            toDoList.RemoveAt(index);
            Console.WriteLine("What do you want to do?\n");
        }

        static void ExitApp()
        {
            Environment.Exit(0);
        }


        // ---------------------------------------- Helpers ----------------------------------------

        // -- Add Flow Helpers --

        static string ValidateUserInputForAddingNewToDo()
        {
            string input = string.Empty;
            bool IsInputInvalid = true;

            do
            {
                Console.WriteLine("Enter the TODO description:");
                input = Console.ReadLine();

                if (string.Empty.Equals(input))
                {
                    Console.WriteLine("The description cannot be empty\n");
                }
                else if (toDoList.Contains(input))
                {
                    Console.WriteLine("The description must be unique\n");
                }
                else
                {
                    IsInputInvalid = false;
                }
            } while (IsInputInvalid);

            return input;
        }

        // -- Remove Flow Helpers --

        static int ValidateUserInputForRemoveToDo()
        {
            char input = '\r';
            int res = -1;

            do
            {
                Console.WriteLine("Select the index of the TODO you want to remove:");
                SeeAllToDoList();
                input = Console.ReadKey().KeyChar;

                if (input == '\r')
                {
                    Console.WriteLine("Selected index cannot be empty\n");
                }
                else if (!Char.IsDigit(input))
                {
                    Console.WriteLine("The given index is not valid\n");
                }
                else if (Char.IsDigit(input))
                {
                    res = int.Parse(input.ToString()) - 1;

                    if (res < 0 || res >= toDoList.Count)
                    {
                        Console.WriteLine("The given index is not valid\n");
                        res = -1;
                    }
                }
            } while (res == -1);

            return res;
        }
    }
}
