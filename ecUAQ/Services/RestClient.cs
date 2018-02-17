using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ecUAQ
{
    public class RestClient
    {
        //http://189.211.201.181:75/GazzetaWebservice2/api/tblgaleria
        public async Task<T> Get<T>(string url){
            try{
                HttpClient cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(url);
                Debug.Write(respuesta);
                if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
                    var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                    var jsonArmado = "{'listaCategorias':" + jsonRespuesta + "}";
                    Debug.WriteLine(jsonArmado); 
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArmado);
                }
            } catch(Exception ex){
                Debug.WriteLine("\nOcurrio un error en la funcion Get del Task"); 
                Debug.WriteLine(ex); 
            }
            return default(T);
        }

        public async Task<T> Get2<T>(string url)
        {
            try
            {
                //Debug.Write(url);
                HttpClient cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(url);
                //Debug.Write(respuesta);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                    var jsonArmado = "{'listaEventos':" + jsonRespuesta + "}";
                    Debug.WriteLine(jsonArmado);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArmado);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nOcurrio un error en la funcion Get del Task");
                Debug.WriteLine(ex);
            }
            return default(T);
        }

        public async Task<T> Get3<T>(string url)
        {
            try
            {
                //Debug.Write(url);
                HttpClient cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(url);
                //Debug.Write(respuesta);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                    var jsonArmado = "{'vistaEventos':" + jsonRespuesta + "}";
                    Debug.WriteLine(jsonArmado);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArmado);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nOcurrio un error en la funcion Get del Task");
                Debug.WriteLine(ex);
            }
            return default(T);
        }

        public async Task<T> GetEventoId<T>(string url)
        {
            try
            {
                HttpClient cliente = new HttpClient();
                var respuesta = await cliente.GetAsync(url);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                    if (jsonRespuesta.Contains("idEvento"))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonRespuesta);
                    }
                    else
                    {
                        var jsonArmado = "{'idEvento':'0'}";
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArmado);
                    }
                }
                else
                {
                    var jsonArmado = "{'idEvento':'0'}";
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArmado);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nOcurrio un error en la funcion Get del Task");
                Debug.WriteLine(ex);
            }
            return default(T);
        }

    }
}
