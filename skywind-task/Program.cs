using System;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        static void Main()
        {
            do
            {
                MainGame game = new MainGame();
                game.Play();

                Console.WriteLine("Press 'R' to replay or any other key to exit...");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (key != 'R' && key != 'r')
                    break;
                Console.Clear(); // Clear the console before starting a new game

            } while (true);
        }
    }
}