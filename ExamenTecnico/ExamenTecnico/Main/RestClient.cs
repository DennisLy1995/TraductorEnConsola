using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class RestClient
    {

        public static JObject GetRequest(string URL)
        {

            WebClient webClient = new WebClient();
            try
            {
                string valor = webClient.DownloadString(URL);
                JObject temp = JObject.Parse(valor);
                return temp;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static string PostRequest(Dictionary<String, string> DictionaryData,string URL)
        {
           
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Method = "POST";
                string postData = JsonConvert.SerializeObject(DictionaryData, Formatting.None);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                //Console.WriteLine((CType(response, HttpWebResponse)).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nUpss algo ha salido mal!!!\n");
                return null;
            }

        }

        /*
         
         Function PostRequest(ByVal DictionaryData As Dictionary(Of String, String), ByVal URL As String) As String
        Try
            Dim request As WebRequest = WebRequest.Create(URL)
            request.Method = "POST"
            Dim postData As String = JsonConvert.SerializeObject(DictionaryData, Formatting.None)
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
            request.ContentType = "application/json"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            Console.WriteLine((CType(response, HttpWebResponse)).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As StreamReader = New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            Console.WriteLine(responseFromServer)
            reader.Close()
            dataStream.Close()
            response.Close()
            Return responseFromServer
        Catch ex As Exception
            Console.WriteLine("Error en el PostRequest")
            Console.WriteLine(ex)
            Return Nothing
        End Try

    End Function
         
         */





    }
}
