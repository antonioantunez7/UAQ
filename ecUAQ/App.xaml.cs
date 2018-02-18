﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ecUAQ.Models;
using ecUAQ.Views;
using Plugin.Geofencing;
using Plugin.Notifications;
using Xamarin.Forms;
using XamForms.Controls;

namespace ecUAQ
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public App()
        {
            InitializeComponent();
            MainPage = new MenuPrincipal();//Se reemplaza por las lineas siguientes porque el menu se duplicaba
            /*if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new MainPage());*/
        }




        protected override void OnStart()
        {
            // Handle when your app starts
            //calendar.SpecialDates.Add(new SpecialDate(DateTime.Now.AddDays(5)) { BackgroundColor = Color.Fuchsia, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth = 8 });
            //calendar.SpecialDates.Add(new SpecialDate(DateTime.Now.AddDays(6)) { BackgroundColor = Color.Fuchsia, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth = 8 });
            //calendar.RaiseSpecialDatesChanged();

            // Handle when your app starts
            CrossGeofences.Current.StartMonitoring(new GeofenceRegion(
            "My House", // identifier - must be unique per registered geofence
                //99.675636
                new Position(19.273603, -99.675636), // center point    
            Distance.FromKilometers(1) // radius of fence
            ));

            base.OnStart();
            CrossGeofences.Current.RegionStatusChanged += (sender, args) =>
            {


                var msg = $"Geofence status for {args.Region.Identifier} changed to {args.Status}";
                CrossNotifications.Current.Send(new Notification
                {
                    Title = "Geofence Update",
                    Message = msg
                });
            };
        }
    }
}
