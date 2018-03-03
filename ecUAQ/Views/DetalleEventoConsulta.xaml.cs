using System;
using System.Collections.Generic;
using ecUAQ.Models;
using Xamarin.Forms;

namespace ecUAQ.Views
{
    public partial class DetalleEventoConsulta : ContentPage
    {
        public DetalleEventoConsulta(int idEvento)
        {
            InitializeComponent();
            cargaDetalleEvento(idEvento);
        }

        async void cargaDetalleEvento(int idEvento){
            etiquetaCargando.Text = "Cargando detalle del evento, por favor espere...";
            svDetalleEventoConsulta.Content = etiquetaCargando;

            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient cliente = new RestClient();

                var evento = await cliente.GetEventoId<Eventos>("http://148.240.202.160:86/CulturaUAQWebservice/api/tbleventos/" + idEvento);
                if (evento != null)
                {
                    if (evento.idEvento > 0)
                    {
                        
                        string url_portada = "http://148.240.202.160:86/" + evento.url_portada;
                        List<Eventos> eventos = new List<Eventos>{
                            new Eventos { titulo = evento.titulo,
                                descripcion = evento.descripcion,
                                url_portada = url_portada,
                                organizador = evento.organizador,
                                lugarEvento = evento.lugarEvento,
                                notas = evento.notas,
                                fechaInicio = this.fechaSQLaNormal(evento.fechaInicio),
                                fechaFin = this.fechaSQLaNormal(evento.fechaFin)
                            }
                        };
                        DetalleDelEvento.ItemsSource = eventos;
                        svDetalleEventoConsulta.Content = DetalleDelEvento;
                    }
                    else
                    {
                        etiquetaCargando.Text = "No se encontró el evento.";
                        svDetalleEventoConsulta.Content = etiquetaCargando;
                    }
                }
                else
                {
                    etiquetaCargando.Text = "Error de conexión.";
                    svDetalleEventoConsulta.Content = etiquetaCargando;
                }
            });
        }

        public string fechaSQLaNormal(string fecha)
        {
            string[] fechaHoralNormal = fecha.Split('T');
            string[] fechaNormal = fechaHoralNormal[0].Split('-');
            return fechaNormal[2] + "/" + fechaNormal[1] + "/" + fechaNormal[0];
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
