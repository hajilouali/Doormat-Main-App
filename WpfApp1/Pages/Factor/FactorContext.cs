using DataLayer;
using DataLayer.Api.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace WpfApp1.Pages.Factor
{
    class FactorContext : ViewModelBase
    {
        public FactorContext(List<RowFactorViewModel> list, List<ProductAndServiceDto> Productss)
        {
            Data = list;
            Products = Productss;
            ProductAndServiceDtos = new ObservableCollection<ProductAndServiceDto>(Productss);
        }
        IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> _unitType;
        public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> UnitTypes
        {
            get
            {
                if (_unitType == null)
                {
                    _unitType = Telerik.Windows.Data.EnumDataSource.FromType<UnitType>();
                }

                return _unitType;
            }
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
        IEnumerable _Products;
        public IEnumerable Products
        {
            get
            {
                return _Products;
            }
            set
            {
                if (_Products != value)
                {
                    _Products = value;

                    OnPropertyChanged("Data");
                }
            }
        }
        private ObservableCollection<ProductAndServiceDto> _ProductAndServiceDtos;
        public ObservableCollection<ProductAndServiceDto> ProductAndServiceDtos
        {
            get
            {
                return this._ProductAndServiceDtos;
            }
            set
            {
                _ProductAndServiceDtos = value;
            }
        }
    }
}
