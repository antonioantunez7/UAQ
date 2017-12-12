using System;
using System.Collections.Generic;
using System.Diagnostics;
using ecUAQ.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace ecUAQ.Views
{
    public partial class Categorias : ContentPage
    {
        
        private Grid gridCategorias = new Grid();
        public Categorias()
        {
            InitializeComponent();
            mallaCategorias();
        }

        public void mallaCategorias()
        {
            /*var imagenCargando = new Image()
            {
                Source = "cargando.gif",
                HeightRequest = 50
            };
            vistaCategorias.Content = imagenCargando;*/
            //int cveCategoria = 0;
            //string descCategoria = "";
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
            etiquetaCargando.Text = "Cargando cartelera, por favor espere...";
            etiquetaCargando.FontFamily = "Futura-Medium";

            vistaCategorias.Content = etiquetaCargando;
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient cliente = new RestClient();
                //var categorias = await cliente.Get<ListaCategorias>("http://189.211.201.181:75/GazzetaWebservice2/api/tblcategorias");
                var categorias = await cliente.Get<ListaCategorias>("http://189.211.201.181:86/CulturaUAQWebservice/api/tblcategorias");
                //var categorias = await cliente.Get<ListaCategorias>("https://images.vexels.com/media/users/3/141217/isolated/lists/a2503907aa82c79fc1e8a82f58d722ed-tel-fono-inteligente-icono-ronda.png");
                Debug.Write(categorias);
                if (categorias != null)
                {
                    /*Agregar una etiqueta arriba del grid*/
                    var etiquetaTitulo = new Label()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = "Arial",
                        Opacity = 0.5
                    };
                    etiquetaTitulo.FontSize = 24;
                    etiquetaTitulo.FontAttributes = FontAttributes.Bold;
                    etiquetaTitulo.Text = "Cartelera";
                    etiquetaTitulo.FontFamily = "Futura-Medium";
                    /*Agregar una etiqueta arriba del grid*/

                    int totalRegistros = categorias.listaCategorias.Count;
                    int maximoColumnas = 2;
                    int auxColumnas = 0;
                    int renglones = 0;
                    if (totalRegistros > 0)
                    {
                        for (int i = 0; i < maximoColumnas; i++)
                        {
                            gridCategorias.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
                        }
                        for (int columnas = 0; columnas < totalRegistros; columnas++)
                        {
                            if (columnas == 0)
                            {
                                gridCategorias.RowDefinitions.Add(new RowDefinition() { Height = 150 });
                                auxColumnas = 0;
                            }
                            else if (auxColumnas / maximoColumnas == 1)
                            {//Si todavia faltan elementos 
                             //Crear renglon    
                                gridCategorias.RowDefinitions.Add(new RowDefinition() { Height = 150 });
                                renglones++;
                                auxColumnas = 0;
                            }
                            if (auxColumnas == maximoColumnas)
                            {
                                auxColumnas = 0;
                            }
                            else
                            {
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
                                label.FontFamily = "Futura-Medium";
                                label.FontAttributes = FontAttributes.Bold;
                                label.Text = categorias.listaCategorias[columnas].descCategoria;
                                gridCategorias.Children.Add(label, auxColumnas, renglones);

                                //Crear el objeto a insertar
                                int cveCategoria = categorias.listaCategorias[columnas].cveCategoria;
                                string descCategoria = categorias.listaCategorias[columnas].descCategoria;
                                string url_portada = "http://189.211.201.181:86/" + categorias.listaCategorias[columnas].url_portada;
                                //string url_portada = "https://pbs.twimg.com/profile_images/3673725732/da6f8684f131d039ee285cbf2bc52529.png";
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
                                tapGestureRecognizer.Tapped += (s, e) =>
                                {
                                    //imagen.Opacity = .5;
                                    eventosCategorias(cveCategoria, descCategoria);
                                };
                                imagen.GestureRecognizers.Add(tapGestureRecognizer);
                                gridCategorias.Children.Add(imagen, auxColumnas, renglones);
                            }
                            auxColumnas++;
                        }
                    }
                    vistaCategorias.Content = gridCategorias;
                }
            });
        }

        private async void eventosCategorias(int cveCategoria,string descCategoria)
        {
             //await this.DisplayAlert("Categoria: "+descCategoria, "Clave: " + cveCategoria, "SI", "NO");
            await Navigation.PushAsync(new ProximosEventos(cveCategoria,descCategoria));
        }
    }
}
