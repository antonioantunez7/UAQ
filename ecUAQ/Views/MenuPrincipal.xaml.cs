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
                new Menu { id= 1, titulo = "Inicio"/*, detalle = "Regresa a la página de inicio."*/, icono = "inicio.png"},
                new Menu { id= 2, titulo = "Proyecto cultura"/*, detalle = "Regresa a la página de proyecto cultura."*/, icono = "proyectoCultura.png"},
                new Menu { id= 3, titulo = "Proximos eventos"/*, detalle = "Regresa a la página de proximos eventos."*/, icono = "proximos.png"},
                new Menu { id= 4, titulo = "Acerca de"/*, detalle = "Regresa a la página de acerca de."*/, icono = "acerca.png"},
                new Menu { id= 5, titulo = "Salir"/*, detalle = "Cerrar la aplicación."*/, icono = "salir.png"}
            };
            ListaMenu.ItemsSource = menu;

            Detail = new NavigationPage(new PaginaInicio());
        }

        public async void ListaMenu_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Menu;
            if (menu != null)
            {
                //if (menu.titulo.Equals("Inicio"))
                if (menu.id == 1)//Inicio
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new PaginaInicio());
                }
                if (menu.id == 2)//Proyecto cultura
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new ProyectoCultura());
                }
                if (menu.id == 3)//Proximos eventos
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new ProximosEventos());
                }
                if (menu.id == 4)//Acerca de
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new AcercaDe());
                }
                if (menu.id == 5)//Salir
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
