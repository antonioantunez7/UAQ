using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ecUAQ.Models
{
    public class Eventos
    {
        public Eventos()
        {
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

        public string url_portada
        {
            get;
            set;
        }

        public int cveCategoria{
            get;
            set;
        }

        public string latitud{
            get;
            set;
        }

        public string longitud{
            get;
            set;
        }

        public int idUsuario{
            get;
            set;
        }
    }

    public class ListaEventos
    {
        public List<Eventos> listaEventos { get; set; }
    }

    public class VistaEventos
    {
        public List<Eventos> vistaEventos { get; set; }
    }

}
