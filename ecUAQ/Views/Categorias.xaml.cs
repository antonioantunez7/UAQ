using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace ecUAQ.Views
{
    public partial class Categorias : ContentPage
    {
        public Categorias()
        {
            InitializeComponent();
            cargaCategorias();
        }

        void cargaCategorias(){
            Debug.Write("Cargando categorias...");
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient cliente = new RestClient();
                Debug.Write("Voy hacer la petincion");
                //var eventos = await cliente.Get<Eventos>("http://189.211.201.181:75/GazzetaWebservice2/api/tblgaleria");
                var eventos = await cliente.Get<ListaGale>("http://189.211.201.181:75/GazzetaWebservice2/api/tblgaleria");
                Debug.Write(eventos);
                foreach (var algo in eventos.listaGaleria)
                {
                    Debug.Write(algo.url_imagen);
                }
                if (eventos != null)
                {
                    Debug.Write("Json: \n");
                    Debug.Write(eventos);
                    Debug.Write("Termine de cargar el webservice");
                }
            });
        }
    }
}
