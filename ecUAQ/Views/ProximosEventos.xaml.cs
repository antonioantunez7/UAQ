using System;
using System.Collections.Generic;
using ecUAQ.Models;
using Xamarin.Forms;

namespace ecUAQ.Views
{
    public partial class ProximosEventos : ContentPage
    {
        /*public ProximosEventos()
        {
            InitializeComponent();
            cargaProximosEventos();
        }*/

        public ProximosEventos(int cveCategoria, string descCategoria)
        {
            InitializeComponent();
            cargaEventosPorCategoria(cveCategoria,descCategoria);
        }

        void cargaProximosEventos()
        {
            List<Eventos> eventos = new List<Eventos>{
                new Eventos { idEvento = 1, titulo = "TORNEO DE PITARRA", 
                    descripcion = "EN LA SEMANA DE IDENTIDAD DE LA F.C.A. ", 
                    organizador = "Club Universitario Deporte Autóctono UAQ", 
                    lugarEvento = "Facultad de Contaduría y Administración UAQ, Campus Cerro de las Campanas",
                    notas = "LLegar puntual",
                    fechaInicio = "14/02/2017 08:00:00",
                    fechaFin = "14/02/2017 08:00:00", 
                    url_portada = "proximos.png"},
                new Eventos { idEvento = 2, titulo = "EXHIBICIÓN DE JUEGO DE PELOTA", 
                    descripcion = "EN LA SEMANA DE IDENTIDAD DE LA F.C.A. ", 
                    organizador = "Club Universitario Deporte Autóctono UAQ", 
                    lugarEvento = "Facultad de Contaduría y Administración UAQ, Campus Cerro de las Campanas", 
                    notas = "LLegar puntual",
                    fechaInicio = "14/02/2017 08:00:00",
                    fechaFin = "14/02/2017 08:00:00", 
                    url_portada = "acerca.png"}
            };


            ListaEventos.ItemsSource = eventos;
        }

        void cargaEventosPorCategoria(int cveCategoria, string descCategoria)
        {
            List<Eventos> eventos = new List<Eventos>{
                new Eventos { idEvento = 1, titulo = "TORNEO DE PITARRA "+descCategoria,
                    descripcion = "EN LA SEMANA DE IDENTIDAD DE LA F.C.A. ",
                    organizador = "Club Universitario Deporte Autóctono UAQ",
                    lugarEvento = "Facultad de Contaduría y Administración UAQ, Campus Cerro de las Campanas",
                    notas = "LLegar puntual",
                    fechaInicio = "14/02/2017 08:00:00",
                    fechaFin = "14/02/2017 08:00:00",
                    url_portada = "proximos.png"},
                new Eventos { idEvento = 2, titulo = "EXHIBICIÓN DE JUEGO DE PELOTA "+descCategoria,
                    descripcion = "EN LA SEMANA DE IDENTIDAD DE LA F.C.A. ",
                    organizador = "Club Universitario Deporte Autóctono UAQ",
                    lugarEvento = "Facultad de Contaduría y Administración UAQ, Campus Cerro de las Campanas",
                    notas = "LLegar puntual",
                    fechaInicio = "14/02/2017 08:00:00",
                    fechaFin = "14/02/2017 08:00:00",
                    url_portada = "acerca.png"}
            };
            ListaEventos.ItemsSource = eventos;
        }


        public async void detalle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var evento = e.SelectedItem as Eventos;
            if (evento != null)
            {
                await Navigation.PushAsync(new DetalleEvento(evento));
                ListaEventos.SelectedItem = null;//Para que automaticamente se deseleccione el elemento
            }   
        }
    }
}
