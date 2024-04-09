using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] string promp;
    static int connectionPort = 5056;
    string hostname = "lochlainn2000pi";
    static string hostIP = "";
    static TcpClient client;


    private void Start()
    {
        if (hostIP == "")
        {

            foreach (var ip in Dns.GetHostByName(hostname).AddressList)
            {
                print(ip.AddressFamily + ": " + ip.ToString() );
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    hostIP = ip.ToString();
                    break;
                }
            }
        }
        print("Host ip set to : "+hostIP);
    }

    static public string ImageFromPrompt(string prompt)
    {
        client = new TcpClient();
        print("connecting to: "+ hostIP);
        client.Connect(hostIP, connectionPort);
        prompt = prompt.Replace(' ', '_');

        string fileName = "processed_image_" + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] bufferSmall = new byte[1024];
        byte[] bufferBig = new byte[2048];
        int msg = nwStream.Read(bufferSmall, 0, 1024);
        print(Encoding.UTF8.GetString(bufferSmall, 0, msg));

        nwStream.Write(Encoding.UTF8.GetBytes(prompt));
        nwStream.Write(Encoding.UTF8.GetBytes(fileName));
        using (MemoryStream ms = new MemoryStream())
        {
            int bytesRead;
            while ((bytesRead = nwStream.Read(bufferBig, 0, bufferBig.Length)) > 0)
            {
                ms.Write(bufferBig, 0, bytesRead);
            }
            PhotoManager.SetPhotoBytes(fileName, ms.ToArray());
        }
        print("Image received and saved");

        return fileName;
    }
    
}
