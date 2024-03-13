
#region [Imports]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace MyAssignment.Services
{
    public class Simulator
    {
        #region[const]
        public const string OUT_OF_BOUNDS_MESSAGE = "Command ignored - out of bounds";
        public const string NOT_PLACED_YET_MESSAGE = "Command ignored - robot not placed yet";
        public const string COMMAND_NOT_RECOGNISED_MESSAGE = "Command ignored - robot did not understand this command";
        public const string VALID_COMMANDS_MESSAGE = "Error during command handling.\nValid commands are:\nPLACE X,Y,Z\nMOVE\nLEFT\nRIGHT\nREPORT";


        private const int xLowerBoundary = 0;
        private const int yLowerBoundary = 0;
        #endregion

        #region [Local Variables]
        private int xUpperBoundary = -1;
        private int yUpperBoundary = -1;
        private int xPosition = -1;
        private int yPosition = -1;
        private string direction = string.Empty;
        private bool isPlaced = false;
        #endregion

        #region [Constructor]
        // Default table size 5,5 if not supplied
        public Simulator()
        {
            xUpperBoundary = 5;
            yUpperBoundary = 5;
        }

        // Custom table size if supplied
        public Simulator(int tableSizeX, int tableSizeY)
        {
            xUpperBoundary = tableSizeX;
            yUpperBoundary = tableSizeY;
        }
        #endregion

        #region [General Methods to Move Robo]
        /// <summary>
        /// Validating ROBO position on 5X5 tabletop
        /// </summary>
        /// <returns></returns>
        private bool ValidatePosition()
        {
            if ((xPosition < xLowerBoundary) || (yPosition < yLowerBoundary))
                return false;
            else if ((xPosition > xUpperBoundary) || (yPosition > yUpperBoundary))
                return false;
            else
                return true;
        }
        /// <summary>
        /// Set place of ROBO on 5X5 tabletop
        /// </summary>
        /// <param name="command">read line from txt file, PLACE 0,0,NORTH</param>
        /// <returns></returns>
        private string Place(string command)
        {
            string result = string.Empty;
            char[] delimiterChars = { ',', ' ' };
            string[] wordsInCommand = command.Split(delimiterChars);

            xPosition = Int32.Parse(wordsInCommand[1]);
            yPosition = Int32.Parse(wordsInCommand[2]);
            direction = wordsInCommand[3];

            if (!ValidatePosition())
                result = OUT_OF_BOUNDS_MESSAGE;
            else
                isPlaced = true;

            return result;
        }
        /// <summary>
        /// Shows the current status of the toy.
        /// </summary>
        /// <returns></returns>
        private string Report()
        {
            return "Output: " + xPosition + "," + yPosition + "," + direction;
        }
        /// <summary>
        /// Moves the toy 1 unit in the facing direction.
        /// </summary>
        /// <returns></returns>
        private string Move()
        {
            string result = string.Empty;
            int originalX = this.xPosition;
            int originalY = this.yPosition;

            switch (direction)
            {
                case "NORTH":
                case "N":
                    yPosition++; break;
                case "WEST":
                case "W":
                    xPosition--; break;
                case "SOUTH":
                case "S":
                    yPosition--; break;
                case "EAST":
                case "E":
                    xPosition++; break;
            }

            if (!ValidatePosition())
            {
                xPosition = originalX;
                yPosition = originalY;
                result = OUT_OF_BOUNDS_MESSAGE;
            }
            return result;
        }
        /// <summary>
        /// turns the toy 90 degrees left.
        /// </summary>
        private void Left()
        {
            switch (direction)
            {
                case "NORTH":
                case "N":
                    direction = "WEST"; break;
                case "WEST":
                case "W":
                    direction = "SOUTH"; break;
                case "SOUTH":
                case "S":
                    direction = "EAST"; break;
                case "EAST":
                case "E":
                    direction = "NORTH"; break;
            }
        }
        /// <summary>
        /// turns the toy 90 degrees right.
        /// </summary>
        private void Right()
        {
            switch (direction)
            {
                case "NORTH":
                case "N":
                    direction = "EAST"; break;
                case "E":
                case "EAST":
                    direction = "SOUTH"; break;
                case "S":
                case "SOUTH":
                    direction = "WEST"; break;
                case "W":
                case "WEST":
                    direction = "NORTH"; break;
            }
        }
        /// <summary>
        /// Read txt file line by line to find exact position of ROBO
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Command(string input)
        {
            string command = input.ToUpper();
            string result = string.Empty;

            try
            {
                if (command.Contains("PLACE"))
                    result = Place(command);

                else if (!isPlaced)
                    result = NOT_PLACED_YET_MESSAGE;

                else if (command.Contains("REPORT"))
                    result = Report();

                else if (command.Contains("MOVE"))
                    result = Move();

                else if (command.Contains("LEFT"))
                    Left();

                else if (command.Contains("RIGHT"))
                    Right();

                else
                    result = COMMAND_NOT_RECOGNISED_MESSAGE;
            }
            catch
            {
                result = VALID_COMMANDS_MESSAGE;
            }

            return result;
        }
        #endregion
    }
}
