using DataLayer.Api.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace WpfApp1.Pages.Clients
{
    public class ClientsContext: ViewModelBase
    {
        public ClientsContext(List<ClientDto> list)
        {
            Data = list;
        }
        private IList<ClientDto> _allClients;
        public IList<ClientDto> AllClients
        {
            get
            {
                if (this._allClients == null)
                {
                    this._allClients = new List<ClientDto>();
                   

                }

                return this._allClients;
            }
            //set
            //{
            //    _allProduct = value;
            //}
        }

        IEnumerable _data;
        public IEnumerable Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (_data != value)
                {
                    _data = value;

                    OnPropertyChanged("Data");
                }
            }
        }
    }
}
