using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinchAPI;

namespace SentryFinch
{
    class Program
    {
        static Finch myFinch = new Finch();
        static void Main(string[] args)
        {
            // **************************************************
            //
            // Title: Capstone
            // Description: 
            // Application Type: Console
            // Author: Harmon Goodman
            // Dated Created: 11/27/2017 
            // Last Modified: 12/10/2017 
            //
            // **************************************************
            Console.ForegroundColor = ConsoleColor.White;
            DisplayOpeningScreen();
            DisplayMenu();
            DisplayClosingScreen();
        }
        /// <summary>
        /// Initialize's Finch
        /// </summary>
        static void InitializeFinch()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            DisplayHeader("Initialize Finch");

            if (myFinch.connect())
            {
                Console.WriteLine("Your Finch is ready to go!");
                myFinch.setLED(255, 255, 255);
                myFinch.noteOn(266);
                myFinch.wait(1000);
                myFinch.setLED(0, 0, 0);
                myFinch.noteOff();
            }
            else
            {
                Console.WriteLine("I am having trouble talking to the Finch.");
                Console.WriteLine("Please check that it is connected.");
            }

            DisplayContinuePrompt();
        }

        static void DisplayMenu()
        {
            string menuChoice;
            bool exiting = false;

            while (!exiting)
            {
                DisplayHeader("Main Menu");

                Console.WriteLine("A) Initialize Finch Robot");
                Console.WriteLine("B) Maze");
                Console.WriteLine("C) Follow");
                Console.WriteLine("X) Exit");

                Console.Write("Enter Choice: ");
                menuChoice = Console.ReadLine().ToUpper();

                switch (menuChoice)
                {
                    case "A":
                        InitializeFinch();
                        break;

                    case "B":
                        Maze();
                        break;

                    case "C":
                        Follow();
                        break;

                    case "X":
                        exiting = true;
                        TerminateFinch();
                        break;

                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Finch will try to get out of a room
        /// </summary>
        static void Maze()
        {
            DisplayHeader("Running");
            myFinch.wait(1000);
            Console.WriteLine("Trying to get out of the room...");

            int speed = 100;
            int waitTime = 1000;
            bool running = true;
            while (running)
            {
                myFinch.setLED(255, 255, 255);
                myFinch.setMotors(speed, speed);

                if (myFinch.isObstacleLeftSide())
                {
                    myFinch.setLED(0, 0, 255);
                    myFinch.setMotors(-speed, -speed);
                    myFinch.wait(waitTime);
                    myFinch.setMotors(speed, 0);
                    myFinch.wait(waitTime);
                    myFinch.setLED(255, 255, 255);
                }
                if (myFinch.isObstacleRightSide())
                {
                    myFinch.setLED(255, 0, 0);
                    myFinch.setMotors(-speed, -speed);
                    myFinch.wait(waitTime);
                    myFinch.setMotors(0, speed);
                    myFinch.wait(waitTime);
                    myFinch.setLED(255, 255, 255);
                }
            }
        }
        /// <summary>
        /// Finch will follow you around (Stay Close)
        /// </summary>
        static void Follow()
        {
            DisplayHeader("Following");
            myFinch.wait(1000);
            Console.WriteLine("Following you...");
            int speed = 100;

            while (true)
            {
                if (myFinch.isObstacleLeftSide() && myFinch.isObstacleRightSide())
                {
                    myFinch.setMotors(speed, speed);
                }

                else if (!myFinch.isObstacleLeftSide() && !myFinch.isObstacleRightSide())
                {
                    myFinch.setMotors(0, 0);
                }

                else if (myFinch.isObstacleLeftSide())
                {
                    myFinch.setMotors(0, speed);
                }

                else if (myFinch.isObstacleRightSide())
                {
                    myFinch.setMotors(speed, 0);
                }
            }

        }
        /// <summary>
        /// Terminate Connection
        /// </summary>
        static void TerminateFinch()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            DisplayHeader("Terminate Finch");

            myFinch.disConnect();

            Console.WriteLine("The Finch Robot is now disconnected.");

            DisplayContinuePrompt();
        }

        static void DisplayOpeningScreen()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            DisplayHeader("Harmon's Capstone Project");

            Console.WriteLine("Welcome");

            DisplayContinuePrompt();

        }

        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press the Enter key to contiune");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void DisplayClosingScreen()
        {
            DisplayHeader("Thank you");

            DisplayContinuePrompt();
        }

        static void DisplayHeader(string headerTitle)
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"\t{headerTitle}");
            Console.WriteLine("----------------------------------------");
        }
    }
}
