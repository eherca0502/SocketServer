using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            server();
        }
        public static void server()
        {
            //Configuraciones del servidor
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11200);

            try
            {
                //Creacion del socket que esta escuchando
                Socket listener =new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
                //Unir el EndPoint al socket
                listener.Bind(localEndPoint);
                //Cantidad de conexiones que recibe antes de indicar que esta ocupado
                listener.Listen(10);
                //Mensaje por pantalla
                Console.WriteLine("Esperando conexion");
                //Se recibe una conexion y se le entrega a un socket para que la maneje 

                Socket handler =listener.Accept();

                string data = null;
                byte[] bytes = null;

                while(true)
                {
                    bytes=new byte[1024];
                    //Recibir datos desde el cliente
                    int byteRec = handler.Receive(bytes);
                    // Convertir los datos desde bytes a string 
                    data += Encoding.ASCII.GetString(bytes,0,byteRec);
                    //Verificar cuando el cliente dejo de enviar datos
                    if (data.IndexOf("<EOF>") > -1)
                        break;
                }
                //Mostrar el mensaje del cliente por pantalla 
                Console.WriteLine("Texto del clirente:" + data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
