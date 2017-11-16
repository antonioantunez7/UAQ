using System;
using System.Collections.Generic;

namespace ecUAQ.Models
{
    public class Galeria
    {
        public Galeria()
        {
            
        }

        public string url_imagen{
            get;
            set;
        }

        public string fechaRegistro{
            get;
            set;
        }
    }
    public class ListaGale
    {
        public List<Galeria> listaGaleria { get; set; }
    }
}
