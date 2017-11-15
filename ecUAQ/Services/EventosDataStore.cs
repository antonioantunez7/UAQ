using System;
using System.Net.Http;

namespace ecUAQ.Services
{
    public class EventosDataStore
    {
        HttpClient cliente;//Se inicializa un cliente de donde obtener los datos
        public EventosDataStore()
        {
            cliente = new HttpClient();//Se crea la instancia del cliente
            cliente.BaseAddress = new Uri($"{App.BackendUrl}/");//Se asigna la url
        }
    }
}
