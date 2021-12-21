using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Task2.MVVM.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public MainViewModel(MainWindow mainWindow)
        {
            var ipAdress = IPAddress.Parse("10.1.18.29");
            var port = 27001;
            using(var socket=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var ep = new IPEndPoint(ipAdress, port);
                socket.Bind(ep);
                socket.Listen(10);
                while (true)
                {
                    var client = socket.Accept();
                    Task.Run(() =>
                    {
                        var length = 0;
                        var bytes = new byte[1024];
                        do
                        {
                            length = client.Receive(bytes);
                            var message = Encoding.UTF8.GetString(bytes, 0, length);
                            mainWindow.ImagesListBox.Items.Add(message);
                        } while (true);
                    });
                }
            }
        }
    }
}