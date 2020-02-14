using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Cybermapa
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebRequest();
        }

        public static void CreateWebRequest()
        {
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//Protocolo de Seguridad
            string URL = "https://cloud13.cybermapa.com/ws/ws.js?";
            var Data = @"{""user"":""PANA"",""pwd"": ""MUNDIAL2016"",""action"": ""DATOSACTUALES""}"; //body

            
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(URL); //llamo a la url
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Accept = "application/json";
            webRequest.ContentLength = Data.Length;

            StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream(), System.Text.Encoding.ASCII);//escribe lo que biene en Data
            requestWriter.Write(Data);
            requestWriter.Close();

            try

            {

                WebResponse webResponse = webRequest.GetResponse(); //guarda la respuesta
                Stream webStream = webResponse.GetResponseStream(); //devuelve el fluoj de datos
                StreamReader responseReader = new StreamReader(webStream); //lee los datos obtenidos

                string response = responseReader.ReadToEnd();//termina de leer los datos y los guarda en una variable

                Console.WriteLine(response); //se muestra los datos del json
                Console.ReadKey();

                var models = JsonConvert.DeserializeObject<IList<RBA>>(response);//deserealiza el json y accede a cada indice individual
                foreach (RBA model in models) //itera el modelo y muestra lo que hay en el JSON
                {
                    
                    Console.WriteLine(model.gps);
                    Console.ReadKey();
                    Console.WriteLine(model.evento);
                    Console.ReadKey();
        
                }

                //dynamic jsonObj = JsonConvert.DeserializeObject(response); //deserealiza el json
                //Console.WriteLine(jsonObj); //se muestra en json
                //Console.ReadKey();


                ////itera el array para mostrar los datos de cada campo con el mismo nombre
                //foreach (dynamic item in jsonObj)
                //Console.WriteLine($"gps : {item.gps} evento: {item.evento}");
                //Console.ReadKey();
                //var gps = jsonObj[0]["gps"].ToString(); //para cada valor
                //    Console.WriteLine(gps);
                //    Console.ReadKey();



            }

            catch (Exception e1)

            {

                Console.WriteLine(e1.Message); //en caso de no ser correcto manda error
                Console.Write("Error");
                Console.ReadKey();

            }
            }
        }
    }

