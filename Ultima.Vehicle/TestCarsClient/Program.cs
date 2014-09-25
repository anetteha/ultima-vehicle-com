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
            
        }

        public void Start()
        {
            ////http://m2mqtt.wordpress.com/using-mqttclient/

            MqttClient client = new MqttClient("http://3b5a813b11ad469faa02babc0a6edb45.cloudapp.net/");

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;     
            client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
            client.MqttMsgPublished += client_MqttMsgPublished;
        }

        private void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            throw new NotImplementedException();
            // write your code
        }

        private void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            throw new NotImplementedException();
            // write your code
        }

        private void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            throw new NotImplementedException();
            // write your code
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            throw new NotImplementedException();
            // access data bytes throug e.Message
        }
    }
}
