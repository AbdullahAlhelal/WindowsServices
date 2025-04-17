using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Basic_Windows_Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller :Installer
    {

        private ServiceProcessInstaller _serviceProcessInstaller;
        private ServiceInstaller _serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();


            //// Use NetworkService account
            //_serviceProcessInstaller = new ServiceProcessInstaller
            //{
            //    Account = ServiceAccount.NetworkService
            //};

            // Configure the Service Process Installer
            _serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem // Adjust as needed (e.g., NetworkService, LocalService)
            };

            // Configure the Service Installer
            _serviceInstaller = new ServiceInstaller
            {
                ServiceName = "MyFirstWinService" , // Must match the ServiceName in your ServiceBase class
                DisplayName = "My First Windows Service" ,
                Description = "MyFirstWinService",
                StartType = ServiceStartMode.Manual // Or Automatic, depending on requirements
            };

            // Add installers to the installer collection
            Installers.Add(_serviceProcessInstaller);
            Installers.Add(_serviceInstaller);
        }
    }
}
