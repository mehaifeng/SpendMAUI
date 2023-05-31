using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendMAUI.Models;
using SpendMAUI.Views.Templates;

namespace SpendMAUI.Views.Templates
{
    public class TemplatesSelector : DataTemplateSelector
    {
        public DataTemplate EachExpense { get; set;}
        public DataTemplate EachInCome { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return (item as Item).IsIncome ? EachInCome : EachExpense;
        }
    }
}
