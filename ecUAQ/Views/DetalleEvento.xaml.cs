using System;
using System.Collections.Generic;
using ecUAQ.Models;
using Xamarin.Forms;

namespace ecUAQ.Views
{
    public partial class DetalleEvento : ContentPage
    {
        public DetalleEvento(Eventos evento)
        {
            InitializeComponent();
            cargaDetalleEvento(evento);
        }

        void cargaDetalleEvento(Eventos evento){
            List<Eventos> eventos = new List<Eventos>{
                new Eventos { titulo =evento.titulo, descripcion = evento.descripcion, url_portada = evento.url_portada}
            };
            DetalleDelEvento.ItemsSource = eventos;
        }
    }
}
