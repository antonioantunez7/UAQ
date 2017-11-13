using System;
using System.Collections.Generic;
using ecUAQ.Models;
using Xamarin.Forms;

namespace ecUAQ.Views
{
    public partial class MenuPrincipal : MasterDetailPage
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            inicio();
        }

        void inicio()
        {
            List<Menu> menu = new List<Menu>{
                new Menu { titulo = "Inicio"/*, detalle = "Regresa a la página de inicio."*/, icono = "icon.png"},
                new Menu { titulo = "Proyecto cultura"/*, detalle = "Regresa a la página de proyecto cultura."*/, icono = "icon.png"},
                new Menu { titulo = "Proximos eventos"/*, detalle = "Regresa a la página de proximos eventos."*/, icono = "icon.png"},
                new Menu { titulo = "Acerca de"/*, detalle = "Regresa a la página de acerca de."*/, icono = "icon.png"},
                new Menu { titulo = "Salir"/*, detalle = "Cerrar la aplicación."*/, icono = "icon.png"}
            };
            ListaMenu.ItemsSource = menu;

            Detail = new NavigationPage(new PaginaInicio());
        }

        public async void ListaMenu_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Menu;
            if (menu != null)
            {
                if (menu.titulo.Equals("Inicio"))
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new PaginaInicio());
                }
                else if (menu.titulo.Equals("Proyecto cultura"))
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new ProyectoCultura());
                }
                else if (menu.titulo.Equals("Proximos eventos"))
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new ProximosEventos());
                }
                else if (menu.titulo.Equals("Acerca de"))
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new AcercaDe());
                }
                else if (menu.titulo.Equals("Salir"))
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    var resp = await this.DisplayAlert("Confirmación", "¿Salir de la app?", "SI", "NO");
                    if (resp)
                    {
                        await this.Navigation.PopAsync();//Cierra la aplicación
                    }
                }
                ListaMenu.SelectedItem = null;//Para que automaticamente se deseleccione el elemento
            }
        }
    }
}
