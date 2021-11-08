using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Delivery_Status
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string FilePath;
            Console.Write("Enter File Path: ");
            FilePath = Console.ReadLine().ToString();
            ReadWrite_ExcelFile.ReadSheet(FilePath);
            Console.ReadKey();
        }
    }
}
