using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace WpfApp1
{
    class CustomLocalizationManager: LocalizationManager
    {
        public override string GetStringOverride(string key)
        {
            switch (key)
            {
                case "GridViewSearchPanelTopText":
                    return "جستجو:";
                //---------------------- RadGridView Filter Dropdown items texts: 
                case "GridViewGroupPanelText":
                    return "برای دسته بندی ، تیتر ستون را در اینجا رها کنید ...";
                case "GridViewFilterShowRowsWithValueThat":
                    return "نمایش ردیف ها با مقدار :";
                case "GridViewFilterSelectAll":
                    return "همه را نشان بده";
                case "GridViewFilterContains":
                    return "شامل";
                case "GridViewClearFilter":
                    return "حذف فیلتر ها";
                case "GridViewFilterIsEqualTo":
                    return "مساوی با";
                case "GridViewFilterIsNotEqualTo":
                    return "نا مساوی با";
                case "GridViewFilterOr":
                    return "یا";
                
                case "GridViewGroupPanelTopTextGrouped":
                    return "دسته بندی با :";
                case "GridViewFilterAnd":
                    return "و";
                case "GridViewFilter":
                    return "فیلتر";
            }
            return base.GetStringOverride(key);
        }
    }
}
