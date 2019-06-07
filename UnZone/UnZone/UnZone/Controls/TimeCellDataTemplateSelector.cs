using System;
using System.Collections.Generic;
using System.Text;
using UnZone.Models;
using Xamarin.Forms;
using TimeInfoType = UnZone.Models.TimeInfo.TimeInfoTypeEnum;

namespace UnZone.Controls
{
    public class TimeCellDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PersonTemplate { get; set; }
        public DataTemplate YouTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // have a look at the item and return the right type of cell
            if (((TimeInfo)item).TimeInfoType == TimeInfoType.Person)
                return PersonTemplate;
            else
                return YouTemplate;

        }
    }
}
