using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnZone.Models;

namespace UnZone.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IList<TimeInfo> Locations { get; }

        public MainViewModel()
        {
            Locations = new ObservableCollection<TimeInfo>();
            Locations.Add(new TimeInfo() { UserName = "Kym", CurrentTime = "10:49", Location = "Melbourne", TimeZoneId = "AEST", TimeInfoType = TimeInfo.TimeInfoTypeEnum.Person });
            Locations.Add(new TimeInfo() { UserName = "Frank", CurrentTime = "14:49", Location = "Berlin", TimeZoneId = "CST", TimeInfoType = TimeInfo.TimeInfoTypeEnum.Person });
            Locations.Add(new TimeInfo() { UserName = "Helen", CurrentTime = "23:00", Location = "Copenhagen", TimeZoneId = "CST", TimeInfoType = TimeInfo.TimeInfoTypeEnum.Person });
            Locations.Add(new TimeInfo() { UserName = "You", CurrentTime = "23:00", Location = "Melborune", TimeZoneId = "AEST", TimeInfoType = TimeInfo.TimeInfoTypeEnum.You });
            Locations.Add(new TimeInfo() { UserName = "James", CurrentTime = "11:30", Location = "Seattle", TimeZoneId = "PST", TimeInfoType = TimeInfo.TimeInfoTypeEnum.Person });

        }
    }
}
