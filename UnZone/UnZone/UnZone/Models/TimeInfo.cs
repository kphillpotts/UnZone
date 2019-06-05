using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnZone.Models 
{
    public class TimeInfo : ObservableObject
    {
        private string userName;
        private string location;
        private string timeZoneId;
        private string currentTime;

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        public string TimeZoneId
        {
            get => timeZoneId;
            set => SetProperty(ref timeZoneId, value);
        }

        public string CurrentTime
        {
            get => currentTime;
            set => SetProperty(ref currentTime, value);
        }


    }
}
