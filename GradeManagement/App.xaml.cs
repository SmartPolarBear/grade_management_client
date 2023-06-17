using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace GradeManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Config => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        public App()
        {
        }
    }
}