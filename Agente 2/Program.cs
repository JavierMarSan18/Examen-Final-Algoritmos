using System;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using Experimental.System.Messaging;

namespace Agente_2
{
    class Program
    {
    	public static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "ueYRXmsJTgdq76OJSzOqYIfqSchg3aIFdyjwdYMN",
            BasePath = "https://examen-final-4dffa.firebaseio.com/"
        };
        static void Main(string[] args)
        {
        	//Declarar variables
        	bool condition = true;
        	int time = 1000;


        	//Conexión a la cola
            MessageQueue queueServer = new MessageQueue(".\\Private$\\ExamenFinal");

            while(condition)
           	{
           		DateTime now = DateTime.UtcNow.AddHours(-6);
           		string date = now.Day.ToString("00") + "-" + now.Month.ToString("00") + "-" + now.Year.ToString("0000") + " " + now.Hour.ToString("00") + ":" + now.Minute.ToString("00") + ":" + now.Second.ToString("00");
           		Message jsonData = queueServer.Receive();
	            jsonData.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
	            //string filename = jsonData.Body.ToString();
	            Data msg = JsonConvert.DeserializeObject<Data>(jsonData.Body.ToString());
	            string filename = msg.filename;

	            Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("               Extracción y Subida de Cola                 ");
                Console.WriteLine("                  Desarrollado por JJMS                    ");
                Console.WriteLine("-----------------------------------------------------------");
              

                Console.WriteLine($"Mensaje {filename}  Encontrado");
	            Console.Write("Subiendo a Base de Datos de Firebase");
	          	wait(time);
            	sendDB(date,msg);
            	Console.WriteLine("El mensaje se ha enviado con éxito");

            	Thread.Sleep(time);
                Console.Clear();

            }
        }
        static void wait(int time)
        {
        	  Thread.Sleep(time);
	          Console.Write(".");
	          Thread.Sleep(time);
	          Console.Write(".");
	          Thread.Sleep(time);
	          Console.Write(".\n\n");
           	  Thread.Sleep(time);
        }
        public static async void sendDB(string date, Data msg)
        {

        	IFirebaseClient client;
            client = new FireSharp.FirebaseClient(config);
            await client.UpdateTaskAsync("/atencion/"+date, msg);
        }

       	internal class Data
        {
            public string filename{get;set;}
        }
    }
}
