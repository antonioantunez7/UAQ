using System;
using System.Diagnostics;
using ecUAQ.Models;
using Xamarin.Forms;
using XamForms.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ecUAQ.Views
{
    public partial class Calendario : ContentPage
    {
        Calendar calendar;
        CalendarVM _vm;
        public Calendario()
        {
            InitializeComponent();
            var SpecialDates = new List<SpecialDate>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient cliente = new RestClient();
                var eventos = await cliente.Get2<ListaEventos>("http://148.240.202.160:86/CulturaUAQWebservice/api/tbleventos");

                foreach (Eventos ev in eventos.listaEventos)
                {
                    System.Diagnostics.Debug.WriteLine(ev);
                    for (DateTime date = DateTime.Parse(ev.fechaInicio); date.Date < DateTime.Parse(ev.fechaFin); date = date.AddDays(1))
                    {
                        SpecialDates.Add(
                            new SpecialDate(date)
                        {
                            BackgroundColor = Color.DarkBlue,
                            TextColor = Color.White,
                            BorderColor = Color.Gray,
                            BorderWidth = 3,
                            Selectable = true
                        }
                        );
                    }
                }
                calendar = new Calendar
                {
                    //MaxDate = DateTime.Now.AddDays(30),
                    //MinDate = DateTime.Now.AddDays(-1),
                    DisableDatesLimitToMaxMinRange = true,
                    MultiSelectDates = false,
                    DisableAllDates = false,
                    WeekdaysShow = true,
                    ShowNumberOfWeek = true,
                    //BorderWidth = 1,
                    //BorderColor = Color.Transparent,
                    //OuterBorderWidth = 0,
                    //SelectedBorderWidth = 1,
                    ShowNumOfMonths = 1,
                    EnableTitleMonthYearView = true,
                    WeekdaysTextColor = Color.Teal,
                    StartDay = DayOfWeek.Monday,
                    SelectedTextColor = Color.Fuchsia,
                    SpecialDates = SpecialDates,
                    /*new SpecialDate(DateTime.Now.AddDays(3))
                    {
                        BackgroundColor = Color.Green,
                        TextColor = Color.Blue,
                        Selectable = true,
                        BackgroundPattern = new BackgroundPattern(1)
                        {
                            Pattern = new List<Pattern>
                            {
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Red},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Purple},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Green},
                                new Pattern{ WidthPercent = 1f, HightPercent = 0.25f, Color = Color.Yellow,Text = "Test", TextColor=Color.DarkBlue, TextSize=11, TextAlign=TextAlign.Middle}
                            }
                        }
                    },*/
                    /*new SpecialDate(DateTime.Now.AddDays(4))
                    {
                        Selectable = true,
                        BackgroundImage = FileImageSource.FromFile("icon.png") as FileImageSource
                    }*/
                };

                calendar.DateClicked += async (sender, e) =>
                {
                    System.Diagnostics.Debug.WriteLine(calendar.SelectedDates[0]);
                    System.Diagnostics.Debug.WriteLine(String.Format("{0:dd-MM-yyyy}", calendar.SelectedDates[0]));
                    // await Navigation.PushAsync(new PaginaInicio(String.Format("{0:dd-MM-yyyy}", calendar.SelectedDates)));
                    await Navigation.PushAsync(new PaginaInicio(String.Format("{0:dd-MM-yyyy}", calendar.SelectedDates[0])));
                    //this.Navigation = new NavigationPage(new PaginaInicio(String.Format("{0:dd-MM-yyyy}", calendar.SelectedDates)));
                };
                _vm = new CalendarVM();
                //var c2 = new MainPage();
                //calendar.SetBinding(Calendar.DateCommandProperty, nameof(_vm.DateChosen));
                //calendar.SetBinding(Calendar.SpecialDatesProperty, nameof(_vm.Attendances));
                //c2.BindingContext = _vm;







                var dates = new List<SpecialDate>();

                var specialDate = new SpecialDate(DateTime.Now);
                specialDate.BackgroundColor = Color.Green;
                specialDate.TextColor = Color.White;

                dates.Add(specialDate);

                calendar.SelectedDate = (DateTime.Now);


                var layout = new StackLayout { Padding = new Thickness(5, 10) };
                this.Content = layout;
                var label = new Label { Text = "This is a label.", TextColor = Color.FromHex("#77d065"), FontSize = 20 };

                layout.Children.Add(calendar);
            });


        }

    }
}
