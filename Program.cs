﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace writeLog
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            bool console = false;

            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
                if (args[i] == "-console")
                {
                    console = true;
                    break;
                }
            }
            if (console)
            {
                Service1.RunCmd(new Service1());
            }
            else
            {
                System.ServiceProcess.ServiceBase sb = new System.ServiceProcess.ServiceBase();
                System.ServiceProcess.ServiceBase.Run(new Service1());
            }
        }
    }
}
