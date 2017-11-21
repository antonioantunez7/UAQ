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
                new Eventos { titulo =evento.titulo, 
                    descripcion = evento.descripcion, 
                    url_portada = evento.url_portada, 
                    organizador = evento.organizador,
                    lugarEvento = evento.lugarEvento,
                    notas = evento.notas,
                    fechaInicio = evento.fechaInicio,
                    fechaFin = evento.fechaFin
                }
            };
            DetalleDelEvento.ItemsSource = eventos;
        }

        void evento_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var evento = e.SelectedItem as Eventos;
            if (evento != null)
            {
                DetalleDelEvento.SelectedItem = null;//Para que automaticamente se deseleccione el elemento
            }  
        }


    }
}
