using System;
using System.Collections.Generic;
using ecUAQ.Models;
using Xamarin.Forms;

namespace ecUAQ.Views
{
    public partial class ProximosEventos : ContentPage
    {
        public ProximosEventos()
        {
            InitializeComponent();
            cargaProximosEventos();
        }

        void cargaProximosEventos()
        {
            List<Eventos> eventos = new List<Eventos>{
                new Eventos { titulo = "TORNEO DE PITARRA", descripcion = "EN LA SEMANA DE IDENTIDAD DE LA F.C.A. ", url_portada = "http://www.uaq.mx/proyectocultura/images_cartelera_general/pitarraf.jpg"},
                new Eventos { titulo = "EXHIBICIÓN DE JUEGO DE PELOTA", descripcion = "EN LA SEMANA DE IDENTIDAD DE LA F.C.A. ", url_portada = "http://www.uaq.mx/proyectocultura/images_cartelera_general/juegof.jpg"}
            };

            ListaEventos.ItemsSource = eventos;
        }

        public async void detalle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var evento = e.SelectedItem as Eventos;
            if (evento != null)
            {
                if (evento.titulo.Equals("TORNEO DE PITARRA"))
                {
                    await Navigation.PushAsync(new DetalleEvento(evento));
                } else if (evento.titulo.Equals("EXHIBICIÓN DE JUEGO DE PELOTA"))
                {
                    await Navigation.PushAsync(new DetalleEvento(evento));
                }
                ListaEventos.SelectedItem = null;//Para que automaticamente se deseleccione el elemento
            }   
        }
    }
}
