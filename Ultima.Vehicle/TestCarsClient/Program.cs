using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TestCarsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MqttClient client = new MqttClient(IPAddress.Parse("192.168.10.53"));

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
            {
            // access data bytes throug e.Message
            }
        }
    }
}
