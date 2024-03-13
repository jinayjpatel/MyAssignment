#region [Imports]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MyAssignment.Services;
#endregion

namespace MyAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulator robot = new Simulator();
            //file in disk
            var FileUrl = @"D:\test.txt";
            //check file existance at mention location
            if (File.Exists(FileUrl) && (Path.GetExtension(FileUrl) == ".txt"))
            {

                //file lines
                string[] lines = File.ReadAllLines(FileUrl);

                //loop through each file line
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    Console.WriteLine(robot.Command(line));
                    //Console.ReadLine();
                }
                Console.WriteLine("Press any key to close");
            }
            else
            {
                //Suggestion
                Console.WriteLine("Not a .txt file or file not found. Please try again.");
                Console.Write(@"The correct command formats are as follows:
PLACE X,Y,DIRECTION
MOVE
RIGHT
LEFT
REPORT
-------------
Please review your input file and try again.");
                //Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
