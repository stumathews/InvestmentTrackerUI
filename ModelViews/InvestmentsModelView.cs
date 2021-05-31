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
        public ObservableCollection<Investment> Investments 
        { 
            get { return _investments; } 
            set { 
                    _investments = value;                 
                    OnPropertyChanged(nameof(Investments));
                    SelectedInvestment = _investments.First();
                }
        }

        private Investment _selectedInvestment;
        public Investment SelectedInvestment
        {
            get { return _selectedInvestment; }
            set {
                _selectedInvestment = value;
                OnPropertyChanged(nameof(SelectedInvestment));
                }
        }

        public virtual async Task<ICollection<Investment>> GetInvestments()
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

    public class MockInvestmentsModelView : InvestmentsModelView
    {
        public override async Task<ICollection<Investment>> GetInvestments()
        {
            return await Task.Run(()=> new ObservableCollection<Investment> 
            {
               new Investment
               { 
                    Name="HSBC FTSE 250 Index Fund Acc C", 
                    Currency=22, 
                    Description="The Fund aims to provide growth over the long term, which is a period of 5 years or more, by tracking the performance of the FTSE 250 Index (the “Index”). The Index is made up of the 250 largest companies after the 100 largest stock market listed companies in the United Kingdom, as defined by the Index provider", 
                    Points=12, 
                    InitialInvestment=13,
                    Value=22,
                    Symbol="$" 
                                
                },
               new Investment
               { 
                   Name="Mattel Group Inc", 
                   Currency=556, 
                   Description="Mattel, Inc. manufactures and markets a range of toy products around the world.", 
                   Points=6, 
                   InitialInvestment=13453, 
                   Value=2234, 
                   Symbol="$"
               },
               new Investment 
               { 
                   Name="RM2 International SA", 
                   Currency=2222, 
                   Description="RM2 International S.A. is a pallet development, manufacture, supply and management company.", 
                   Points=1, 
                   InitialInvestment=13234, 
                   Value=2442, 
                   Symbol="£" 
                },
               new Investment 
               { 
                   Name="Marlborough Multi Cap Income Fund P Inc", 
                   Currency=1234, 
                   Description="The aim of the Fund is to provide income, that is, money paid out from an investment as dividends from shares, as well as to deliver capital growth, that is to increase the value of your investment. It is recommended that the Fund is held for a minimum of 5 years.", 
                   Points=12, 
                   InitialInvestment=13000, 
                   Value=2200, 
                   Symbol="£" 
               }
            });
        }
    }
}
