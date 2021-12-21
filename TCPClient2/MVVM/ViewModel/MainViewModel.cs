﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TCPClient2.MVVM.ViewModel
{
    public class MainViewModel:BaseViewModel
    {


        public MainViewModel(MainWindow mainWindow)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ipAdress = IPAddress.Parse("10.1.18.29");
            var port = 27001;
            var ep = new IPEndPoint(ipAdress, port);
            try
            {
                socket.Connect(ep);
                if (socket.Connected)
                {
                    MessageBox.Show("Connected to the server . . .","Info");

                    while (true)
                    {
                        var message = mainWindow.MsgTxtBox.Text;
                        var bytes = Encoding.UTF8.GetBytes(message);
                        socket.Send(bytes);
                    }
                }
                else
                {
                    MessageBox.Show("Can not connect to the server . . .");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not connect to the server . . .");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
