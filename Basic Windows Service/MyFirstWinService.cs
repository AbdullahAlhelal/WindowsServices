using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basic_Windows_Service
{
    public partial class MyFirstWinService : ServiceBase
    {
        public MyFirstWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            string logDirectory = @"C:\Logs";
            string logFilePath = Path.Combine(logDirectory , "MyServiceLog.txt");


            // Check if the directory exists, if not, create it
            if ( !Directory.Exists(logDirectory) )
            {
                Directory.CreateDirectory(logDirectory);
            }


            // Set the current process priority to High
            Process process = Process.GetCurrentProcess();
            process.PriorityClass = ProcessPriorityClass.High;
            // Append the log with the timestamp
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service Started\n";
            File.AppendAllText(logFilePath , logMessage);

            // on faliure
            Thread workerThread = new Thread(WorkerTask);
            workerThread.Start();
        }
    

        protected override void OnStop()
        {


            string logDirectory = @"C:\Logs";
            string logFilePath = Path.Combine(logDirectory , "MyServiceLog.txt");


            // Ensure the directory exists before writing to the log
            if ( !Directory.Exists(logDirectory) )
            {
                Directory.CreateDirectory(logDirectory);
            }


            // Append the log with the timestamp
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service Stopped\n";
            File.AppendAllText(logFilePath , logMessage);
        }

        private void WorkerTask()
        {
            try
            {
                // Simulate work
                while ( true )
                {
                    //LogEvent("Service is running...");
                    Thread.Sleep(5000);

                    // Simulate a failure
                    throw new Exception("Simulated error for testing recovery.");
                }
            }
            catch ( Exception ex )
            {
                //LogEvent($"Error: {ex.Message}");
                // Exit the process to simulate failure
                Environment.Exit(1);
            }
        }
        public void StartInConsole()
        {
            OnStart(null); // Trigger OnStart logic
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine(); // Wait for user input to simulate service stopping
            OnStop(); // Trigger OnStop logic
            Console.ReadKey();

        }
    }
}
