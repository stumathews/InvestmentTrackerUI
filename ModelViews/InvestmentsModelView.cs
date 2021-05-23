using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InvestmentTrackerApiClient;

namespace InvestmentTrackerUI.ModelViews
{
    public class InvestmentsModelView: ViewModelBase
    {
        private ObservableCollection<Investment> _investments;
        public ObservableCollection<Investment> Investments { 
            get { return _investments; } 
            set { 
                    _investments = value; 
                    OnPropertyChanged(nameof(Investments));
                }
        }

        public async Task<ICollection<Investment>> GetInvestments()
        {
             HttpClient httpClient = new HttpClient();
            var api = new InvestmentTrackerApiClient.InvestmentTrackerApiClient("http://investment-manager-backend.herokuapp.com", httpClient);
            //var api = new InvestmentTrackerApiClient.InvestmentTrackerApiClient("http://localhost:10000", httpClient);

            UserLoginInfo loginInfo = new UserLoginInfo { Username = "stuart", Password = "anything" };
            TokenResponse response = await api.TokenAsync(loginInfo);
            api.Token = response.Token;
            return await api.InvestmentAllAsync();
        }

        public InvestmentsModelView()
        {
            _investments = new ObservableCollection<Investment>();
            var awaiter = Task.Run(GetInvestments).GetAwaiter();
            awaiter.OnCompleted(()=> 
            {
                foreach(var investment in awaiter.GetResult())
                {
                    _investments.Add(investment);
                }
            });

            
            
           
        }
    }
}
