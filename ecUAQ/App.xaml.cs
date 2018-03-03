using System;
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
            CrossGeofences.Current.StopAllMonitoring();
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient cliente = new RestClient();
                var eventos = await cliente.Get2<ListaEventos>("http://148.240.202.160:86/CulturaUAQWebservice/api/tbleventos");
                foreach (Eventos ev in eventos.listaEventos)
                {
                    //19.273613, -99.675679
                    double latitud = System.Double.Parse(ev.latitud);
                    double longitud = System.Double.Parse(ev.longitud);
                    try
                    {
                        CrossGeofences.Current.StartMonitoring(new GeofenceRegion(
                        ev.titulo, new Position(latitud, longitud),
                        Distance.FromKilometers(1) // radius of fence
                    ));
                    } catch(Exception e)
                    {
                        Console.Write(e.ToString());
                        Debug.WriteLine(e.ToString());
                    }

                }

            });
            //base.OnStart();
            CrossGeofences.Current.RegionStatusChanged += (sender, args) =>
            {
                //var msg = $"Geofence status for {args.Region.Identifier} changed to {args.Status}";
                var msg = $"{args.Region.Identifier}";
                CrossNotifications.Current.Send(new Notification
                {
                    
                    Title = "Evento cerca de tí",
                    Message = msg,
                    Vibrate = true,
                    Sound = "pop"
                });
            };
        
            // Handle when your app starts
            //calendar.SpecialDates.Add(new SpecialDate(DateTime.Now.AddDays(5)) { BackgroundColor = Color.Fuchsia, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth = 8 });
            //calendar.SpecialDates.Add(new SpecialDate(DateTime.Now.AddDays(6)) { BackgroundColor = Color.Fuchsia, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth = 8 });
            //calendar.RaiseSpecialDatesChanged();

            // Handle when your app starts
            /*CrossGeofences.Current.StartMonitoring(new GeofenceRegion(
            "My House", // identifier - must be unique per registered geofence
                new Position(19.273585, -99.675642), // center point    
            Distance.FromKilometers(1) // radius of fence
            ));
            CrossNotifications.Current.Send(new Notification
            {
                Title = "Hola",
                Message = "Fernando"
            });
            base.OnStart();
            CrossGeofences.Current.RegionStatusChanged += (sender, args) =>
            {


                var msg = $"Geofence status for {args.Region.Identifier} changed to {args.Status}";
                CrossNotifications.Current.Send(new Notification
                {
                    Title = "Geofence Update",
                    Message = msg
                });
            };*/
        }
        protected override void OnSleep()
        {
            CrossGeofences.Current.RegionStatusChanged += (sender, args) =>
            {
                //var msg = $"Geofence status for {args.Region.Identifier} changed to {args.Status}";
                var msg = $"{args.Region.Identifier}";
                CrossNotifications.Current.Send(new Notification
                {
                    Title = "Evento cerca de tí",
                    Message = msg,
                    Vibrate = true,
                    Sound = "pop"
                });
            };
        }

        void OnActivated(object sender, Notification e)
        {

        }
    }
}
