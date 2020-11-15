using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Experimental.System.Messaging;

namespace Agente_1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Declarar Variables
            bool condition = true;
            int time = 1000;
            //Conexión a la cola
            MessageQueue queueServer = new MessageQueue(".\\Private$\\ExamenFinal");

            while(condition)
            {

                //Crear objeto
                Data data = new Data();
                //Asignar Valor aleatorio
                data.filename = Path.GetRandomFileName();

                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("             Generador de Nombres de Aleatorios            ");
                Console.WriteLine("                   Desarrollado por JJMS                   ");
                Console.WriteLine("-----------------------------------------------------------");
                Console.Write($"Mensaje {data.filename}  Encolando");
                wait(time);
                sendData(queueServer, data);
                //Mensaje de proceso exitos
                Console.WriteLine($"El mensaje se ha encolado con éxito");
                Thread.Sleep(time);
                //Método para encolar el mensaje
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
        static void sendData(MessageQueue queueServer, Data data)
        {
                //Convertir a Json
                string jsonData = JsonConvert.SerializeObject(data);

                //Empaquetar mensaje
                Message msg = new Message();
                msg.Body = jsonData;

                //Encolar mensaje
                queueServer.Send(msg);
        }
        internal class Data
        {
            public string filename{get;set;}
        }
    }
}
