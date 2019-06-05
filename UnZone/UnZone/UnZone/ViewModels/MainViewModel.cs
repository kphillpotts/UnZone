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
            Locations.Add(new TimeInfo() { UserName = "Kym", CurrentTime = "10:49", Location = "Melbourne", TimeZoneId = "AEST" });
            Locations.Add(new TimeInfo() { UserName = "Frank", CurrentTime = "14:49", Location = "Berlin", TimeZoneId = "CST" });
            Locations.Add(new TimeInfo() { UserName = "Helen", CurrentTime = "23:00", Location = "Copenhagen", TimeZoneId = "CST" });
            Locations.Add(new TimeInfo() { UserName = "James", CurrentTime = "11:30", Location = "Seattle", TimeZoneId = "PST" });

        }
    }
}
