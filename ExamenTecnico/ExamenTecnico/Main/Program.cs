using Entities_POJO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main
{
    class Program
    {
        private static Usuarios usuarioActivo = new Usuarios();

        static void Main(string[] args)
        {
            usuarioActivo.NOMBRE_USUARIO = "";
            usuarioActivo.NOMBRE = "";
            usuarioActivo.CEDULA = 0;
            usuarioActivo.APELLIDO = "";
            
            string triggerExit = "loop";
            string lecturaBOT = "";

            Console.WriteLine("Bienvenido al DennisBOT\n");
            Console.WriteLine("         Comandos del DennisBOT:\n\n#register:Registrarse en el DennisBOT.\n#login: Ingresar al DennisBOT con mi cuenta.\n#exit: Salir de mi cuenta.\n#getout: Cerrar el DennisBOT\n" +
            "#translate: Traducir una frase.\n#myhistory: Historial de mis traducciones.\n#populars: Mostrar las frases mas populares entre los usuarios.\n#idioms: idiomas registrados.\n" +
            "#newidiom: Registrar un nuevo idioma.");

            while (triggerExit == "loop")
            {
                lecturaBOT = Console.ReadLine();

                switch (lecturaBOT)
                {
                    case "#register":
                        registrarUsuario();
                        break;

                    case "#login":
                        comandoLogin();
                        break;

                    case "#exit":
                        if (usuarioActivo.NOMBRE_USUARIO.Equals(""))
                        {
                            Console.WriteLine("\nAre you kidding, ingresa primero con tu cuenta antes de querer salir de esta!!!\n");
                        }
                        else
                        {
                            Console.WriteLine("\n       Hasta luego " + usuarioActivo.NOMBRE_USUARIO +"\n");
                            usuarioActivo.NOMBRE_USUARIO = "";
                            usuarioActivo.NOMBRE = "";
                            usuarioActivo.CEDULA = 0;
                            usuarioActivo.APELLIDO = "";

                        }
                        break;

                    case "#getout":
                        Console.WriteLine("\n***************Arrivederci***************\n");
                        triggerExit = "GetOut";
                        break;

                    case "#translate":
                        if (usuarioActivo.NOMBRE_USUARIO.Equals(""))
                        {
                            Console.WriteLine("\nPrimero ingrese a su cuenta, yo se que da pereza pero diay, nada que hacer.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nDigame su frase y yo me encargo de todo loco.\n");
                            string fraseLectura = Console.ReadLine();
                            //comandoTranslate(fraseLectura);
                            comandoTranslate(fraseLectura);
                        }
                        break;

                    case "#myhistory":
                        if (usuarioActivo.NOMBRE_USUARIO.Equals(""))
                        {
                            Console.WriteLine("\nPor favor ingrese en su cuenta primero.\n");
                        }
                        else
                        {
                            mostrarConsultasFrases(usuarioActivo.CEDULA);
                        }
                        break;

                    case "#populars":
                        Console.WriteLine("\nLo estamos breteando, esperese!!!\n");
                        break;

                    case "#idioms":
                        if (usuarioActivo.NOMBRE_USUARIO.Equals(""))
                        {
                            Console.WriteLine("\nPor favor ingrese en su cuenta primero.\n");
                        }
                        else
                        {
                            mostrarIdiomas();
                        }
                        break;

                    case "#newidiom":
                        if (usuarioActivo.NOMBRE_USUARIO.Equals(""))
                        {
                            Console.WriteLine("\nPor favor ingrese en su cuenta primero.\n");
                        }
                        else
                        {
                            registrarIdioma();
                        }
                        break;

                    default:
                        Console.WriteLine("Comando incorrecto, creo que andas perdido amigo!!!");
                        break;
                }
            }

            Console.ReadKey();
        }

        //****************************************************************************************************************
        static void comandoLogin()
        {
            string lecturaBOT = "";
            Console.WriteLine("\nPor favor ingrese su nombre de usuario.\n");
            lecturaBOT = Console.ReadLine();
            try
            {
                JObject resp = RestClient.GetRequest("http://localhost:52383/api/Usuarios/GetByUserName/" + lecturaBOT);
                string a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                usuarioActivo = JsonConvert.DeserializeObject<Usuarios>(a);
                Console.WriteLine("\nBienvenido " + usuarioActivo.NOMBRE_USUARIO + " que me cuenta, listo para hacer unas traducciones bien locas!!!\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nEl nombre de usuario no se encuentra registrado, te gustaria registrarte en el DennisBOT?\nsi o no?");
                string decision = Console.ReadLine();
                switch (decision)
                {
                    case "si":
                        registrarUsuario();
                        break;

                    case "no":
                        Console.WriteLine("\n       Como quiera!!!.\n");
                        break;

                    case "Si":
                        registrarUsuario();
                        break;

                    case "No":
                        Console.WriteLine("\n       Como quiera!!!.\n");
                        break;

                    default:
                        Console.WriteLine("\n       Asumire que no.\n");
                        break;

                }
            }
        }

        //****************************************************************************************************************
        static void registrarUsuario()
        {
            Dictionary<string, string> diccionarioTemp = new Dictionary<string, string>();
            
            Console.WriteLine("\nIngrese su cedula.\n");
            diccionarioTemp.Add("CEDULA", Console.ReadLine());
            Console.WriteLine("\nIngrese su nombre.\n");
            diccionarioTemp.Add("NOMBRE", Console.ReadLine());
            Console.WriteLine("\nIngrese su apellido.\n");
            diccionarioTemp.Add("APELLIDO", Console.ReadLine());
            Console.WriteLine("\nIngrese su nombre de nombre usuario.\n");
            diccionarioTemp.Add("NOMBRE_USUARIO", Console.ReadLine());

            RestClient.PostRequest(diccionarioTemp, "http://localhost:52383/api/Usuarios");
            Console.WriteLine("\nSe agrego satisfactoriamente al usuario, que cool.\n");
        }

        //************************************************************************************************************************************************************************
        static void comandoTranslate(string fraseLectura)
        {
            int check = 0;
            string idioma = escogerIdioma();
            if (idioma.Equals("espaÃ±ol"))
            {
                idioma = "español";
            }
            string[] words = fraseLectura.Split(' ');
            foreach (string word in words)
            {
                try
                {
                    //Verificar si la palabra esta registrada en la BD.
                    JObject resp = RestClient.GetRequest("http://localhost:52383/api/Palabras/GetByPalabra/" + word);
                    string a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                    Palabras palabraTemp = new Palabras();
                    palabraTemp = JsonConvert.DeserializeObject<Palabras>(a);

                    try
                    {
                        //Si la palabra esta registrada, se intentara buscar la traduccion.
                        resp = RestClient.GetRequest("http://localhost:52383/api/Palabras/GetPalabraEnIdioma?palabraVar=" + palabraTemp.PALABRA_PRIMER_REGISTRO + "&nombre_idioma=" + idioma);
                        a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                        Palabras palabraTraducida = new Palabras();
                        palabraTraducida = JsonConvert.DeserializeObject<Palabras>(a);
                    }
                    catch (Exception i)
                    {
                        //la traduccion en el idioma que el cliente selecciono no se encuentra, por lo cual se procede a registrarse.
                        Console.WriteLine(palabraTemp.PALABRA + ": no se encuentra registrada en "+idioma+", vamos a registrarla.\n");
                        Console.WriteLine("\nDime cual es la traduccion de "+palabraTemp.PALABRA + " en "+idioma+"n");
                        string wordTranslated = Console.ReadLine();
                        registrarPalabraTraduccion(wordTranslated, idioma, palabraTemp.PALABRA, palabraTemp.TIPO);
                        check++;
                    }
                }
                catch (Exception e)
                {

                    try
                    {
                        //La palabra no se encuentra registrada en la BD.
                        JObject resp = RestClient.GetRequest("http://localhost:52383/api/Palabras/GetByPrimeraPalabra/" + word);
                        string a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                        Palabras palabraTemp = new Palabras();
                        palabraTemp= JsonConvert.DeserializeObject<Palabras>(a);
                        check++;
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("\n"+word + ": no se encuentra registrada, procedamos a registrarla.\n");
                        Console.WriteLine("\nQue es esta palabra un sustantivo, verbo, adjetivo, articulo?\n");
                        string tipo = Console.ReadLine();
                        int validacionRegistro = registrarPrimeraPalabra(word, tipo);
                        if (validacionRegistro==1)
                        {
                            Console.WriteLine("\nRegistremos tambien la traduccion al idioma que escogiste\n");
                            string wordTranslated = Console.ReadLine();
                            registrarPalabraTraduccion(word, idioma, word, tipo);
                            registrarPalabraTraduccion(wordTranslated , idioma , word, tipo);
                            Console.WriteLine("\nSe completo el registro de la traduccion.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nUpss algo ha salido mal.\n");
                        }
                        check++;
                    }
                }

            }
            if (check == 0)
            {
                string traduccion = "";
                //AQUI
                foreach (string word in words)
                {
                    try
                    {
                        //Verificar si la palabra esta registrada en la BD.
                        JObject resp = RestClient.GetRequest("http://localhost:52383/api/Palabras/GetByPalabra/" + word);
                        string a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                        Palabras palabraTemp = new Palabras();
                        palabraTemp = JsonConvert.DeserializeObject<Palabras>(a);

                        try
                        {
                            //Si la palabra esta registrada, se intentara buscar la traduccion.
                            resp = RestClient.GetRequest("http://localhost:52383/api/Palabras/GetPalabraEnIdioma?palabraVar=" + palabraTemp.PALABRA_PRIMER_REGISTRO + "&nombre_idioma=" + idioma);
                            a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                            Palabras palabraTraducida = new Palabras();
                            palabraTraducida = JsonConvert.DeserializeObject<Palabras>(a);
                            Console.WriteLine(palabraTemp.PALABRA + ": traduccion " + idioma + " es " + palabraTraducida.PALABRA);
                            traduccion = traduccion + " " +palabraTraducida.PALABRA;
                        }
                        catch (Exception i)
                        {
                            
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Upss algo ha salido mal.");
                    }
                }
                try
                { 
                    Console.WriteLine("\n" + traduccion + "\n");

                    JObject resp = RestClient.GetRequest("http://localhost:52383/api/ConsultasFrases/GetContador");
                    string a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                    ConsultasFrases consulta = new ConsultasFrases();
                    consulta = JsonConvert.DeserializeObject<ConsultasFrases>(a);

                    Dictionary<string, string> diccionarioTemp = new Dictionary<string, string>();
                    diccionarioTemp.Add("CODIGO_CONSULTA", consulta.CODIGO_CONSULTA);
                    diccionarioTemp.Add("CEDULA", Convert.ToString(usuarioActivo.CEDULA));
                    diccionarioTemp.Add("FRASE", traduccion);
                    diccionarioTemp.Add("TRADUCCION_ESPANOL", "N/A");
                    diccionarioTemp.Add("CANTIDAD_PALABRAS", Convert.ToString(words.Length));
                    diccionarioTemp.Add("FECHA_CONSULTA", Convert.ToString(DateTime.Today));
                    diccionarioTemp.Add("POPULARIDAD", "N/A");
                    RestClient.PostRequest(diccionarioTemp, "http://localhost:52383/api/ConsultasFrases");
                }
                catch (Exception o)
                {
                    Console.WriteLine("Rayos, no pudimos registrar la traduccion en tu cuenta.");
                }
                
            }
            else if (check == 1)
            {
                Console.WriteLine("\nVuelve a ingresar #translate y veamos la traduccion de la frase!!!\n");
            }
        }

        static void mostrarConsultasFrases(int cedula)
        {

            try
            {
                Console.WriteLine("\nSus frases han sido: \n");

                JObject resp = RestClient.GetRequest("http://localhost:52383/api/ConsultasFrases");
                string a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                JArray test = JArray.Parse(a);

                var arregloConsultas = test.ToObject<List<ConsultasFrases>>();
                
                foreach (ConsultasFrases element in arregloConsultas)
                {
                    Console.WriteLine("\n" + element.CEDULA + ": " + element.FECHA_CONSULTA + ", en el idioma " + "\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nUps no hemos podido traer todas las frases.");
            }

        }

        static void mostrarIdiomas()
        {
            try
            {
                Console.WriteLine("\nSus frases han sido: \n");

                JObject resp = RestClient.GetRequest("http://localhost:52383/api/Idiomas");
                string a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                JArray test = JArray.Parse(a);

                var arregloConsultas = test.ToObject<List<Idiomas>>();

                int contador = 1;
                foreach (Idiomas element in arregloConsultas)
                {
                    
                    Console.WriteLine("\n" + contador +"." +element.NOMBRE_IDIOMA + " de " + element.PAIS_ORIGEN + "\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nUps no hemos podido traer todos los idiomas.");
            }
        }
        static void registrarIdioma()
        {
            try
            {
                //La palabra no se encuentra registrada en la BD.
                Dictionary<string, string> diccionarioTemp = new Dictionary<string, string>();
                Console.WriteLine("\nCual es el idioma que quieres registrar?\n");
                string NOMBRE_IDIOMA = Console.ReadLine();
                diccionarioTemp.Add("NOMBRE_IDIOMA", NOMBRE_IDIOMA);
                Console.WriteLine("\nPais de origen?\n");
                string PAIS_ORIGEN = Console.ReadLine();
                diccionarioTemp.Add("PAIS_ORIGEN", PAIS_ORIGEN);

                RestClient.PostRequest(diccionarioTemp, "http://localhost:52383/api/Idiomas");
                Console.WriteLine("\nSe agrego satisfactoriamente el idioma, que cool.\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nEl idioma no se ha podido registrar, probablemente ya existe jajaja.\n");
            }
        }






        static int registrarPrimeraPalabra(string palabra,string tipo)
        {
            try
            {
                //La palabra no se encuentra registrada en la BD.
                Dictionary<string, string> diccionarioTemp = new Dictionary<string, string>();
                diccionarioTemp.Add("PALABRA", palabra);
                Console.WriteLine("\nEn que idioma esta "+palabra+"?\n");
                diccionarioTemp.Add("NOMBRE_IDIOMA", Console.ReadLine());
                diccionarioTemp.Add("PALABRA_PRIMER_REGISTRO", palabra);
                diccionarioTemp.Add("TIPO", tipo);

                RestClient.PostRequest(diccionarioTemp, "http://localhost:52383/api/Palabras/PostPrimera");
                Console.WriteLine("\nSe agrego satisfactoriamente la palabra, que cool.\n");
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nLa palabra no se ha podido registrar.\n");
                return 0;
            }
        }

        static void registrarPalabraTraduccion(string traduccion,string idioma, string palabraPrimerRegistro, string tipo)
        {
            try
            {
                //La palabra no se encuentra registrada en la BD.
                Dictionary<string, string> diccionarioTemp = new Dictionary<string, string>();
                diccionarioTemp.Add("PALABRA", traduccion);
                diccionarioTemp.Add("NOMBRE_IDIOMA", idioma);
                diccionarioTemp.Add("PALABRA_PRIMER_REGISTRO", palabraPrimerRegistro);
                diccionarioTemp.Add("TIPO", tipo);

                RestClient.PostRequest(diccionarioTemp, "http://localhost:52383/api/Palabras");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nLa palabra no se ha podido registrar en el idioma "+idioma+"\n");
            }
        }

        //****************************************************************************************************************
        static string escogerIdioma()
        {
            string idiomaLectura = "";
            int validacion = 0;

            while (validacion == 0)
            {
                Console.WriteLine("\nUpps, se me olvido que tambien ocupo el idioma, dime a que idioma quieres traducir\n");
                idiomaLectura = Console.ReadLine();
                try
                {
                    JObject resp = RestClient.GetRequest("http://localhost:52383/api/Idiomas/GetByName/" + idiomaLectura);
                    string a = JsonConvert.SerializeObject(resp.SelectToken("Data"));
                    Idiomas idioma = JsonConvert.DeserializeObject<Idiomas>(a);
                    idiomaLectura = idioma.NOMBRE_IDIOMA;
                    validacion = 1;
                }
                catch (Exception e)
                {
                    validacion = 0;
                }
                
            }

            return idiomaLectura;
        }
    }
}
