﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new LibFFRNetwork.CoopHelper();

            Console.WriteLine($"Current state: {a.GetState()}");
            Console.ReadKey();
        }
    }
}
