using System;
using System.Collections.Generic;
using System.Diagnostics;
using ecUAQ.Models;
using ecUAQ.Services;
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
            List<Models.Menu> menu = new List<Models.Menu>{//Le cambie Menu a Models.Menu porque al ejecutarlo en iOS manda error de ambiguo
                new Models.Menu { id= 1, titulo = "Inicio"/*, detalle = "Regresa a la página de inicio."*/, icono = "inicio.png"},
                new Models.Menu { id= 2, titulo = "Proyecto cultura"/*, detalle = "Regresa a la página de proyecto cultura."*/, icono = "proyectoCultura.png"},
                //new Models.Menu { id= 3, titulo = "Proximos eventos"/*, detalle = "Regresa a la página de proximos eventos."*/, icono = "proximos.png"},
                new Models.Menu { id= 3, titulo = "Cartelera"/*, detalle = "Regresa a la página de proximos eventos."*/, icono = "proximos.png"},
                new Models.Menu { id= 4, titulo = "Acerca de"/*, detalle = "Regresa a la página de acerca de."*/, icono = "acerca.png"},
                new Models.Menu { id= 5, titulo = "Salir"/*, detalle = "Cerrar la aplicación."*/, icono = "salir.png"}
            };
            ListaMenu.ItemsSource = menu;

            //Detail = new NavigationPage(new PaginaInicio());
            Detail = new NavigationPage(new Categorias());//Se cambia para que sea la cartelera la primera en cargar
        }

        public async void ListaMenu_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Models.Menu;
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
                /*if (menu.id == 3)//Proximos eventos
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new ProximosEventos());
                }*/
                if (menu.id == 3)//Categorias
                {
                    IsPresented = false;//Para que el menu desapasca cuando se le haga click
                    Detail = new NavigationPage(new Categorias());
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
                        //await this.Navigation.PopAsync();//Cierra la aplicación
                        System.Environment.Exit(0); 

                    }
                }
                ListaMenu.SelectedItem = null;//Para que automaticamente se deseleccione el elemento
            }
        }

        /*protected override void OnAppearing()
        {
            Debug.Write("Voy a cargar el webservice");
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient cliente = new RestClient();
                Debug.Write("Voy hacer la petincion");
                //var eventos = await cliente.Get<Eventos>("http://189.211.201.181:75/GazzetaWebservice2/api/tblgaleria");
                var eventos = await cliente.Get<ListaGale>("http://189.211.201.181:75/GazzetaWebservice2/api/tblgaleria");
                Debug.Write(eventos);
                foreach(var algo in eventos.listaGaleria){
                    Debug.Write(algo.url_imagen);
                }
                if (eventos != null)
                {
                    Debug.Write("Json: \n");
                    Debug.Write(eventos);
                    Debug.Write("Termine de cargar el webservice");
                }
            });
        }*/
    }
}
