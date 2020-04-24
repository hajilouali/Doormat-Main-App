using DataLayer.Api.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace WpfApp1.Pages.Products
{
    public class ProductsContext : ViewModelBase
    {
        public ProductsContext(List<ProductAndServiceDto> list)
        {
            Data = list;
            SelectedItemProductType = ProductTypes.FirstOrDefault();
            SelectedItemUnitType= UnitTypes.FirstOrDefault();
        }
        IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> _productTypes;
        public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> ProductTypes
        {
            get
            {
                if (_productTypes == null)
                {
                    // Calling this static factory method will return an IEnumerable
                    // full of view models. The Value property of each view model is
                    // the actual enum value, so you would typically set 
                    // RadComboBox.SelectedValueMemberPath to be equal to "Value". 
                    // The DisplayName property of each view model returns the first 
                    // of the following properties that is not null:
                    // - DisplayShortName (Silverlight only)
                    // - Description
                    // - Name
                    // So you would typically set the DisplayMemberPath of the combo to
                    // be equal to "DisplayName". But you can use any of the other
                    // properties of the view model if you want.

                    _productTypes = Telerik.Windows.Data.EnumDataSource.FromType<ProductType>();
                }

                return _productTypes;
            }
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

        private IList<ProductAndServiceDto> _allProduct;
        public IList<ProductAndServiceDto> AllProduct
        {
            get
            {
                if (this._allProduct == null)
                {
                    this._allProduct = new List<ProductAndServiceDto>();
                    this._allProduct.Add(new ProductAndServiceDto() { 
                    id=1,ProductCode="100",ProductName="پادری آلومینیومی",ProductType= DataLayer.Api.Response.ProductType.Product,
                        UnitPrice=100,UnitType=DataLayer.Api.Response.UnitType.Metr
                    });

                }

                return this._allProduct;
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
        EnumMemberViewModel _selectedItemProductType;
        public EnumMemberViewModel SelectedItemProductType
        {
            get
            {
                return _selectedItemProductType;
            }
            set
            {
                if (_selectedItemProductType != value)
                {
                    _selectedItemProductType = value;

                    OnPropertyChanged("SelectedItemProductType");

                   
                }
            }
        }
        EnumMemberViewModel _selectedItemUnitType;
        public EnumMemberViewModel SelectedItemUnitType
        {
            get
            {
                return _selectedItemUnitType;
            }
            set
            {
                if (_selectedItemUnitType != value)
                {
                    _selectedItemUnitType = value;

                    OnPropertyChanged("SelectedItemUnitType");


                }
            }
        }
    }
}
