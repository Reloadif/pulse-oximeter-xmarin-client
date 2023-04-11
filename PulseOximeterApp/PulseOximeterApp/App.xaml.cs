using System.IO;
using System.Reflection;
using System;
using Xamarin.Forms;
using PulseOximeterApp.Data.DataBase;

namespace PulseOximeterApp
{
    public partial class App : Application
    {
        private static StatisticDB _statisticDB;

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

        public App()
        {
            DependencyService.Register<Infrastructure.DependencyServices.IMessageService, Infrastructure.DependencyServices.MessageService>();

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
