// Importing required namespaces
using System;
using System.Collections.Generic;

namespace Program
{
    class MainGame
    {
        // Main variables
        private List<int> multipliers;
        private List<int> selectedMultipliers;
        private List<int> boxMultipliers;
        private int betAmount;
        private int spins;
        private int matchedMultipliers;
        private int totalWinnings;
        private string gameResult;
        private string testing;
        private List<int> spunMultipliers;

        // Constructor
        public MainGame()
        {
            // possible multipliers
            multipliers = new List<int> { 15, 16, 17, 18, 19, 20 };
            // Select 3 multipliers from the list
            selectedMultipliers = SelectMultipliers(multipliers);
            // Assign multipliers to each box
            boxMultipliers = AssignMultipliersToBoxes(selectedMultipliers);
            betAmount = GetBetAmount();
            spins = 0;
            matchedMultipliers = 0;
            totalWinnings = 0;
            gameResult = "- Bet Amount £" + betAmount + "\n";
            spunMultipliers = new List<int>();
        }

        // Main game logic
        public void Play()
        {
            //for (spins = 1; spins <= 4 && matchedMultipliers < 3; spins++)
            while(spins <= 4 && matchedMultipliers < 3)
            {
                // Add the grid
                DisplayGrid();

                Console.WriteLine("Press [ENTER] to spin");
                // Wait to press enter
                Console.ReadLine();

                int spunBox = SpinBox();
                Console.WriteLine("Number spun: " + spunBox);
                Console.WriteLine();

                // Display multiplier when space is pressed
                DisplayMultiplier(spunBox, boxMultipliers);

                Console.WriteLine("---------------------------------");
                Console.WriteLine("Press [ENTER] to continue");
                Console.ReadLine();

                int winnings = CalculateWinnings(spunBox);
                totalWinnings += winnings;

                Console.WriteLine("Winnings from spin " + spins + ": " + winnings);
                Console.WriteLine("Total Winnings so far: " + totalWinnings);
                Console.WriteLine("---------------------------------");

                // Adds the multiplier associated with the box to the list of spun multipliers
                spunMultipliers.Add(boxMultipliers[spunBox - 1]);

                // Check if the multiplier associated with the box is one of the selected multipliers
                if (selectedMultipliers.Contains(boxMultipliers[spunBox - 1]))
                {
                    // Increment matches
                    matchedMultipliers++;
                    gameResult += "- Spin " + spins + " opens box " + spunBox + ", a x" + boxMultipliers[spunBox - 1] + " multiplier is revealed\n";
                }
            }

            if (matchedMultipliers >= 2)
            {
                int mostFrequentMultiplier = GetMostFrequentMultiplier(spunMultipliers, selectedMultipliers);
                int totalWinnings = betAmount * mostFrequentMultiplier;


                // Final Result
                gameResult += "- The game is complete, the player has won x" + mostFrequentMultiplier + " and earned £" + totalWinnings;
                Console.Clear();
            }

            Console.WriteLine("Game Results:");
            Console.WriteLine(gameResult);
        }



        // Selects 3 unique multipliers from the list randomly
        private List<int> SelectMultipliers(List<int> multipliers)
        {
            Random random = new Random();
            List<int> selectedMultipliers = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(multipliers.Count);
                // get the multiplier at the random index
                int selectedMultiplier = multipliers[index];
                selectedMultipliers.Add(selectedMultiplier);
                // Remove it from the possible multiplier list
                multipliers.RemoveAt(index);
            }

            return selectedMultipliers;
        }

        // Assigns the selected multipliers to the boxes
        private List<int> AssignMultipliersToBoxes(List<int> selectedMultipliers)
        {
            List<int> boxMultipliers = new List<int>();
            // Only adds 3 multipliers
            boxMultipliers.AddRange(selectedMultipliers);
            boxMultipliers.AddRange(selectedMultipliers);
            boxMultipliers.AddRange(selectedMultipliers);

            return boxMultipliers;
        }

        // Prompt user to enter a bet amount
        private int GetBetAmount()
        {
            int betAmount;
            do
            {
                Console.Write("Enter Bet Amount: ");
                string input = Console.ReadLine();
                // Edge case
                if (!int.TryParse(input, out betAmount) || betAmount <= 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid bet amount greater than 0.");
                }
            } while (betAmount <= 0);

            return betAmount;
        }

        // Displays the grid
        private void DisplayGrid()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("| 1 | 2 | 3 |");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("| 4 | 5 | 6 |");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("| 7 | 8 | 9 |");
            Console.WriteLine("---------------------------------");
        }

        // Displays the multiplier associated with the spun box
        private void DisplayMultiplier(int spunBox, List<int> boxMultipliers)
        {
            string gridLine = GetGridLine(spunBox, boxMultipliers);
            Console.WriteLine(gridLine);
        }

        // Replaces the number on the grid with the associated multiplier
        private string GetGridLine(int spunBox, List<int> boxMultipliers)
        {
            // Store the string in a variable
            string gridLine = "---------------------------------\n";
            gridLine += "| 1 | 2 | 3 |\n";
            gridLine += "---------------------------------\n";
            gridLine += "| 4 | 5 | 6 |\n";
            gridLine += "---------------------------------\n";
            gridLine += "| 7 | 8 | 9 |\n";
            gridLine += "---------------------------------\n";

            // Updated box automatically
            gridLine = gridLine.Replace(" " + spunBox.ToString() + " ", " x" + boxMultipliers[spunBox - 1].ToString() + " ");

            return gridLine;
        }

        // Spins the box and returns the number
        private int SpinBox()
        {
            Random random = new Random();
            return random.Next(1, 10);
        }

        // Calculates the winnings based on the bet amount and multiplier
        private int CalculateWinnings(int spunBox)
        {
            return betAmount * boxMultipliers[spunBox - 1];
        }

        // Priority Queue style to find the most frequent multiplier from the spins and selected multipliers
        private int GetMostFrequentMultiplier(List<int> spins, List<int> selectedMultipliers)
        {
            // Store values in a dictionary
            Dictionary<int, int> frequencyMap = new Dictionary<int, int>();
            int mostFrequentMultiplier = spins[0];
            int maxFrequency = 0;

            foreach (int spin in spins)
            {
                if (!frequencyMap.ContainsKey(spin))
                    frequencyMap[spin] = 1;

                // Increment value if is in the dictionary
                frequencyMap[spin]++;
                if (frequencyMap[spin] > maxFrequency && selectedMultipliers.Contains(spin))
                {
                    maxFrequency = frequencyMap[spin];
                    mostFrequentMultiplier = spin;
                }
            }

            return mostFrequentMultiplier;
        }
    }
}
