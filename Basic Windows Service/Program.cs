﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Windows_Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {


            if ( Environment.UserInteractive )
            {
                
                // Running in console mode
                Console.WriteLine("Running in console mode...");
                MyFirstWinService service = new MyFirstWinService();
                service.StartInConsole();
            }
            else
            {
                // Running as a Windows Service
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new MyFirstWinService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
