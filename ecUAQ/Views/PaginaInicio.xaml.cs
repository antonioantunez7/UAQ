using System;
using System.Collections.Generic;
using System.Diagnostics;
using ecUAQ.Models;
using Xamarin.Forms;

namespace ecUAQ.Views
{
    public partial class PaginaInicio : ContentPage
    {
        private Grid gridEventos = new Grid();
        public PaginaInicio(string fecha)
        {
            InitializeComponent();
            mallaEventos(fecha);
        }

        public void mallaEventos(string fecha)
        {
            var etiquetaCargando = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "Arial",
                Opacity = 0.5
            };
            etiquetaCargando.FontSize = 24;
            etiquetaCargando.FontAttributes = FontAttributes.Bold;
            etiquetaCargando.Text = "Cargando eventos, por favor espere...";
            etiquetaCargando.FontFamily = "Futura-Medium";

            vistaEventos.Content = etiquetaCargando;
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient cliente = new RestClient();
                //var categorias = await cliente.Get<ListaCategorias>("http://189.211.201.181:75/GazzetaWebservice2/api/tblcategorias");
                if (fecha != "")
                {
                    var eventos = await cliente.Get3<VistaEventos>("http://189.211.201.181:86/CulturaUAQWebservice/api/tbleventos/fecha/" + fecha);
                    Debug.Write(eventos);
                    if (eventos != null)
                    {

                        int totalRegistros = eventos.vistaEventos.Count;
                        int maximoColumnas = 2;
                        int auxColumnas = 0;
                        int renglones = 0;
                        if (totalRegistros > 0)
                        {
                            for (int i = 0; i < maximoColumnas; i++)
                            {
                                gridEventos.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
                            }
                            for (int columnas = 0; columnas < totalRegistros; columnas++)
                            {
                                if (columnas == 0)
                                {
                                    gridEventos.RowDefinitions.Add(new RowDefinition() { Height = 170 });
                                    auxColumnas = 0;
                                }
                                else if (auxColumnas / maximoColumnas == 1)
                                {//Si todavia faltan elementos 
                                 //Crear renglon    
                                    gridEventos.RowDefinitions.Add(new RowDefinition() { Height = 170 });
                                    renglones++;
                                    auxColumnas = 0;
                                }
                                if (auxColumnas == maximoColumnas)
                                {
                                    auxColumnas = 0;
                                }
                                else
                                {
                                    /*Se crea el objeto de tipo evento*/
                                    string url_portadaE = "http://189.211.201.181:86/" + eventos.vistaEventos[columnas].url_portada;
                                    Eventos eventoX = new Eventos
                                    {
                                        idEvento = eventos.vistaEventos[columnas].idEvento,
                                        titulo = eventos.vistaEventos[columnas].titulo,
                                        descripcion = eventos.vistaEventos[columnas].descripcion,
                                        organizador = eventos.vistaEventos[columnas].organizador,
                                        lugarEvento = eventos.vistaEventos[columnas].lugarEvento,
                                        notas = eventos.vistaEventos[columnas].notas,
                                        fechaInicio = this.fechaSQLaNormal(eventos.vistaEventos[columnas].fechaInicio),
                                        fechaFin = this.fechaSQLaNormal(eventos.vistaEventos[columnas].fechaFin),
                                        url_portada = url_portadaE
                                    };
                                    /*Se crea el objeto de tipo evento*/
                                    var label = new Label()
                                    {
                                        //BackgroundColor = Color.Black,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        VerticalTextAlignment = TextAlignment.End,
                                        HorizontalTextAlignment = TextAlignment.Center,
                                        FontFamily = "Arial",
                                        TextColor = Color.Black,
                                        Opacity = 1
                                    };
                                    label.FontSize = 8;
                                    label.FontFamily = "Futura-Medium";
                                    label.FontAttributes = FontAttributes.Bold;
                                    label.Text = eventos.vistaEventos[columnas].titulo;
                                    gridEventos.Children.Add(label, auxColumnas, renglones);

                                    //Crear el objeto a insertar
                                    int idEvento = eventos.vistaEventos[columnas].idEvento;
                                    string titulo = eventos.vistaEventos[columnas].titulo;
                                    string url_portada = "http://189.211.201.181:86/" + eventos.vistaEventos[columnas].url_portada;
                                    Debug.Write(url_portada);
                                    var imagen = new Image()
                                    {
                                        Source = url_portada,
                                        HeightRequest = 120,
                                        VerticalOptions = LayoutOptions.Start,
                                        Opacity = 0.8
                                    };
                                    //Se crea el evento del clic de la imagen
                                    var tapGestureRecognizer = new TapGestureRecognizer();
                                    tapGestureRecognizer.Tapped += (s, e) => {
                                        detalleEvento(eventoX);
                                    };
                                    imagen.GestureRecognizers.Add(tapGestureRecognizer);
                                    gridEventos.Children.Add(imagen, auxColumnas, renglones);
                                }
                                auxColumnas++;
                            }
                        }
                        vistaEventos.Content = gridEventos;
                    }
                }
                else
                {
                    var eventos = await cliente.Get3<VistaEventos>("http://189.211.201.181:86/CulturaUAQWebservice/api/tbleventos");
                    Debug.Write(eventos);
                    if (eventos != null)
                    {

                        int totalRegistros = eventos.vistaEventos.Count;
                        int maximoColumnas = 2;
                        int auxColumnas = 0;
                        int renglones = 0;
                        if (totalRegistros > 0)
                        {
                            for (int i = 0; i < maximoColumnas; i++)
                            {
                                gridEventos.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
                            }
                            for (int columnas = 0; columnas < totalRegistros; columnas++)
                            {
                                if (columnas == 0)
                                {
                                    gridEventos.RowDefinitions.Add(new RowDefinition() { Height = 170 });
                                    auxColumnas = 0;
                                }
                                else if (auxColumnas / maximoColumnas == 1)
                                {//Si todavia faltan elementos 
                                 //Crear renglon    
                                    gridEventos.RowDefinitions.Add(new RowDefinition() { Height = 170 });
                                    renglones++;
                                    auxColumnas = 0;
                                }
                                if (auxColumnas == maximoColumnas)
                                {
                                    auxColumnas = 0;
                                }
                                else
                                {
                                    /*Se crea el objeto de tipo evento*/
                                    string url_portadaE = "http://189.211.201.181:86/" + eventos.vistaEventos[columnas].url_portada;
                                    Eventos eventoX = new Eventos
                                    {
                                        idEvento = eventos.vistaEventos[columnas].idEvento,
                                        titulo = eventos.vistaEventos[columnas].titulo,
                                        descripcion = eventos.vistaEventos[columnas].descripcion,
                                        organizador = eventos.vistaEventos[columnas].organizador,
                                        lugarEvento = eventos.vistaEventos[columnas].lugarEvento,
                                        notas = eventos.vistaEventos[columnas].notas,
                                        fechaInicio = this.fechaSQLaNormal(eventos.vistaEventos[columnas].fechaInicio),
                                        fechaFin = this.fechaSQLaNormal(eventos.vistaEventos[columnas].fechaFin),
                                        url_portada = url_portadaE
                                    };
                                    /*Se crea el objeto de tipo evento*/
                                    var label = new Label()
                                    {
                                        //BackgroundColor = Color.Black,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        VerticalTextAlignment = TextAlignment.End,
                                        HorizontalTextAlignment = TextAlignment.Center,
                                        FontFamily = "Arial",
                                        TextColor = Color.Black,
                                        Opacity = 1
                                    };
                                    label.FontSize = 8;
                                    label.FontFamily = "Futura-Medium";
                                    label.FontAttributes = FontAttributes.Bold;
                                    label.Text = eventos.vistaEventos[columnas].titulo;
                                    gridEventos.Children.Add(label, auxColumnas, renglones);

                                    //Crear el objeto a insertar
                                    int idEvento = eventos.vistaEventos[columnas].idEvento;
                                    string titulo = eventos.vistaEventos[columnas].titulo;
                                    string url_portada = "http://189.211.201.181:86/" + eventos.vistaEventos[columnas].url_portada;
                                    Debug.Write(url_portada);
                                    var imagen = new Image()
                                    {
                                        Source = url_portada,
                                        HeightRequest = 120,
                                        VerticalOptions = LayoutOptions.Start,
                                        Opacity = 0.8
                                    };
                                    //Se crea el evento del clic de la imagen
                                    var tapGestureRecognizer = new TapGestureRecognizer();
                                    tapGestureRecognizer.Tapped += (s, e) => {
                                        detalleEvento(eventoX);
                                    };
                                    imagen.GestureRecognizers.Add(tapGestureRecognizer);
                                    gridEventos.Children.Add(imagen, auxColumnas, renglones);
                                }
                                auxColumnas++;
                            }
                        }
                        vistaEventos.Content = gridEventos;
                    }
                }

                //var categorias = await cliente.Get<ListaCategorias>("https://images.vexels.com/media/users/3/141217/isolated/lists/a2503907aa82c79fc1e8a82f58d722ed-tel-fono-inteligente-icono-ronda.png");

            });
        }

        public async void detalleEvento(Eventos e)
        {
            var evento = e;
            if (evento != null)
            {
                await Navigation.PushAsync(new DetalleEvento(evento));
            }
        }

        public string fechaSQLaNormal(string fecha)
        {
            string[] fechaHoralNormal = fecha.Split('T');
            string[] fechaNormal = fechaHoralNormal[0].Split('-');
            return fechaNormal[2] + "/" + fechaNormal[1] + "/" + fechaNormal[0];
        }

    }
}
