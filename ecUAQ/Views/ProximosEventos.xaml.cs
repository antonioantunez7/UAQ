using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        List<Eventos> leventos;
        public ProximosEventos(int cveCategoria, string descCategoria)
        {
            InitializeComponent();
            //cargaEventosPorCategoria(cveCategoria,descCategoria);
            cargaEventos(cveCategoria, descCategoria);
        }

        void cargaEventos(int cveCategoria, string descCategoria)
        {
            leventos = new List<Eventos>();
            leventos.Add(new Eventos
            {
                titulo = "Cargando eventos, por favor espere..."
            });
            //ListaEventos.IsEnabled = false;//Le quita el evento del clic
            ListaEventos.ItemsSource = leventos;
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient cliente = new RestClient();
                var eventos = await cliente.Get2<ListaEventos>("http://189.211.201.181:86/CulturaUAQWebservice/api/tbleventos/categoria/" + cveCategoria);
                if (eventos != null) {
                    if (eventos.listaEventos.Count > 0)
                    {
                        leventos = new List<Eventos>();
                        foreach (var evento in eventos.listaEventos)
                        {
                            string url_portada = "http://189.211.201.181:86/" + evento.url_portada;
                            leventos.Add(new Eventos
                            {
                                idEvento = evento.idEvento,
                                titulo = evento.titulo,
                                descripcion = evento.descripcion,
                                organizador = evento.organizador,
                                lugarEvento = evento.lugarEvento,
                                notas = evento.notas,
                                fechaInicio = this.fechaSQLaNormal(evento.fechaInicio),
                                fechaFin = this.fechaSQLaNormal(evento.fechaFin),
                                url_portada = url_portada
                            });
                            Debug.Write(evento);
                        }
                        ListaEventos.ItemsSource = leventos;
                    } else{
                        leventos = new List<Eventos>();
                        leventos.Add(new Eventos
                        {
                            titulo = "No hay eventos para esta categoría"
                        });
                        //ListaEventos.IsEnabled = false;//Le quita el evento del clic
                        ListaEventos.ItemsSource = leventos;
                    }
                } else{
                }
            });
        }

        public string fechaSQLaNormal(string fecha){
            string[] fechaHoralNormal = fecha.Split('T');
            string[] fechaNormal = fechaHoralNormal[0].Split('-');
            return fechaNormal[2]+"/"+fechaNormal[1]+"/"+fechaNormal[0];     
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
