using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Robusta.TalentManager.WebApi.Client.WinApp.ViewModels;
using Robusta.TalentManager.WebApi.Client.WinApp.Views;

namespace Robusta.TalentManager.WebApi.Client.WinApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            EmployeeFindViewModel viewModel = new EmployeeFindViewModel();

            EmployeeFind view = new EmployeeFind();
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
