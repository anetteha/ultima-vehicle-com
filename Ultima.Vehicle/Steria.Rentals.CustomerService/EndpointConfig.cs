
using System.Linq;
using NServiceBus.Persistence;
using Raven.Client.Document;

namespace Steria.Rentals.CustomerService
{
    using NServiceBus;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Client
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UseTransport<RabbitMQTransport>();

            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8081",
                DefaultDatabase = "ultima.vehicle"
            };
            configuration.UsePersistence<RavenDBPersistence>().SetDefaultDocumentStore(documentStore);
            configuration.RijndaelEncryptionService();
            configuration.Conventions().DefiningEncryptedPropertiesAs(type =>
                type.PropertyType.Name == "WireEncryptedString" ||
                type.GetCustomAttributes(true)
                    .Any(t => t.GetType().Name == "UltimaEncryptionAttribute"));
        }
    }
}
