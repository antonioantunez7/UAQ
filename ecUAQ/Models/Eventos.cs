using System;
using Xamarin.Forms;

namespace ecUAQ.Models
{
    public class Eventos
    {
        Image image;
        public Eventos()
        {
            image = new Image
            {
                HeightRequest = 20,
                WidthRequest = 20,
            };

            image.Opacity = 0.5;
        }

        public int idEvento{
            get;
            set;
        }

        public string titulo{
            get;
            set;
        }

        public string descripcion{
            get;
            set;
        }

        public string organizador{
            get;
            set;
        }

        public string lugarEvento{
            get;
            set;
        }

        public string notas{
            get;
            set;
        }

        public string fechaInicio{
            get;
            set;
        }

        public string fechaFin{
            get;
            set;
        }

        public ImageSource url_portada
        {
            get { return image.Source; }
            set { image.Source = value; }
        }

        public int idGeolocalizacion{
            get;
            set;
        }

        public int idUsuario{
            get;
            set;
        }
    }
}
