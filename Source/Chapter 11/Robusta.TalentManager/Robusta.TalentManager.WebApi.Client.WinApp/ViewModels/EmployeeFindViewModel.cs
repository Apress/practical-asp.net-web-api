using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Robusta.TalentManager.WebApi.Client.WinApp.Commands;
using Robusta.TalentManager.WebApi.Dto;

namespace Robusta.TalentManager.WebApi.Client.WinApp.ViewModels
{
    public class EmployeeFindViewModel : ViewModelBase
    {
        private int employeeId;
        private EmployeeDto employeeFound;

        public EmployeeFindViewModel()
        {
            this.FindCommand = new RelayCommand(p => FindEmployee());
        }

        public ICommand FindCommand { get; private set; }

        public int EmployeeId
        {
            get
            {
                return employeeId;
            }
            set
            {
                employeeId = value;
                RaisePropertyChanged(() => this.EmployeeId);
            }
        }

        public EmployeeDto EmployeeFound
        {
            get
            {
                return employeeFound;
            }
            set
            {
                employeeFound = value;
                RaisePropertyChanged(() => this.EmployeeFound);
            }
        }

        private async Task FindEmployee()
        {
            HttpClient client = new HttpClient();
            string creds = String.Format("{0}:{1}", "jqhuman", "p@ssw0rd!");
            byte[] bytes = Encoding.Default.GetBytes(creds);

            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            client.DefaultRequestHeaders.Authorization = header;

            // GET
            //HttpResponseMessage response = client
            //            .GetAsync("http://localhost/TalentManager/api/employees/" + this.EmployeeId)
            //                .Result;

            HttpResponseMessage response = await client
                .GetAsync(
                          "http://localhost/TalentManager/api/employees/"
                                  + this.EmployeeId); // Not calling .Result now


            if (response.IsSuccessStatusCode)
            {
                this.EmployeeFound = response.Content.ReadAsAsync<EmployeeDto>().Result;
            }
        }
    }
}
