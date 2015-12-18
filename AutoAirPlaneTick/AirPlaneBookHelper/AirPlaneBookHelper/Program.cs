using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirPlaneBookHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            // 
            GrabHelp helper = new GrabHelp();
            helper.SearchCriteria();

            Console.WriteLine("Press any key to exit!");
            Console.ReadLine();
        }
    }
}
