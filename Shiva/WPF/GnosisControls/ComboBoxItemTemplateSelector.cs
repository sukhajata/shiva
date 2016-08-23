using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GnosisControls
{
    public class ComboBoxItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DropDownTemplate
        {
            get;
            set;
        }
        public DataTemplate SelectedTemplate
        {
            get;
            set;
        }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //ComboBoxItem comboBoxItem = (ComboBoxItem)VisualTreeHelper.GetParent(container);
            //if (comboBoxItem != null)
            //{
            //    return DropDownTemplate;
            //}
            //return SelectedTemplate;
            var presenter = (ContentPresenter)container;
            return (presenter.TemplatedParent is ComboBox) ? SelectedTemplate : DropDownTemplate;

        }
    }
}
