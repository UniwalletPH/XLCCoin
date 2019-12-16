using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using XLCCoin.Application;
using XLCCoin.Application.Interfaces;
using XLCCoin.Infrastructure;
using XLCCoin.Infrastructure.Persistence;

namespace XLCCoin.NodeApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public IConfiguration Configuration { get; private set; }
        
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            IConfigurationBuilder _builder = new ConfigurationBuilder()
                                           .SetBasePath(Directory.GetCurrentDirectory())
                                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = _builder.Build();

            ServiceCollection _services = new ServiceCollection();

            _services.AddSingleton<MainWindow>();
            _services.AddTransient<MainWindowVM>();

            _services.AddSingleton(Configuration);

            _services.AddInfrastructure(Configuration);
            _services.AddApplication();

            var _serviceProvider = _services.BuildServiceProvider();

            IServiceProvider serviceProvider = _services.BuildServiceProvider();

            MainWindow mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
