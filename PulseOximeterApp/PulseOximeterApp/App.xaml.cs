using System.IO;
using System;
using Xamarin.Forms;
using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.Infrastructure.DependencyServices;

namespace PulseOximeterApp
{
    public partial class App : Application
    {
        #region Fields
        private static MicrocontrollerConnector _microcontrollerConnector;
        private static StatisticDB _statisticDB;
        #endregion

        #region Properties
        public static MicrocontrollerConnector Microcontroller
        {
            get
            {
                if (_microcontrollerConnector is null)
                {
                    _microcontrollerConnector = new MicrocontrollerConnector();
                }

                return _microcontrollerConnector;
            }
        }

        public static StatisticDB StatisticDataBase
        {
            get
            {
                if (_statisticDB is null)
                {
                    _statisticDB = new StatisticDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StatisticDataBase.db"));
                }

                return _statisticDB;
            }
        }
        #endregion

        public App()
        {
            DependencyService.Register<IShowMessageDependencyService, ShowMessageDependencyService>();

            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
