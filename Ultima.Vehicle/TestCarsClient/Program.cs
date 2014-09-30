using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TestCarsClient
{
    class Program
    {
        private static MqttClient _client;
        private static readonly string[] Topic = { "car/status"};

        static void Main()
        {
            Console.WriteLine("Start test client");
            Start();
            Console.WriteLine("Send message from client to server");
            SendMessage();
            Console.WriteLine("Press enter key to quit");
            Console.ReadLine();
            End();
        }

        private static void SendMessage()
        {
            var temp = Convert.ToString("This is the message");
            _client.Publish("car/status", Encoding.UTF8.GetBytes(temp));
        }

        private static void End()
        {
            _client.Unsubscribe(Topic);
        }

        public static void Start()
        {
            ////http://m2mqtt.wordpress.com/using-mqttclient/
            ////http://3b5a813b11ad469faa02babc0a6edb45.cloudapp.net/
            ////Same as:
            ////137.135.201.38

            _client = new MqttClient("137.135.201.38");

            _client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;     
            _client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            _client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
            _client.MqttMsgPublished += client_MqttMsgPublished;


            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE };
            var grantedQos = _client.Subscribe(Topic, qosLevels);

            Console.WriteLine("Connect to server...");

            var state = _client.Connect("ABC12345", "guest", "guest", false, 0, false, null, null, true, 60);

            //_client.Connect(Guid.NewGuid().ToString());

            Console.WriteLine("Connected!");
        }

        private static void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            // write your code
            Console.WriteLine("Message will be delivered (exactly once) to all subscribers on the topic");
        }

        private static void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            // write your code
            Console.WriteLine("Subscription unregistered at broker in Azure");
        }

        private static void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            // write your code
            Console.WriteLine("Subscription registered at broker in Azure");
        }

        private static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // access data bytes throug e.Message
            Console.WriteLine("A message is published on a topic the client is subscribed to");
        }
    }
}
