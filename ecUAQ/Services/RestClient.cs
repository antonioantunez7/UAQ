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
                    var jsonArmado = "{'listaGaleria':" + jsonRespuesta + "}";
                    Debug.WriteLine(jsonArmado); 
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonArmado);
                }
            } catch(Exception ex){
                Debug.WriteLine("\nOcurrio un error en la funcion Get del Task"); 
                Debug.WriteLine(ex); 
            }
            return default(T);
        }
    }
}
