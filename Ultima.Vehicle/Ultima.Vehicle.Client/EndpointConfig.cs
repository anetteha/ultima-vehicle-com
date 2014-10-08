
using NServiceBus.Persistence;
using Raven.Client.Document;

namespace Ultima.Vehicle.Client
{
    using NServiceBus;

    /*
        This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
        can be found here: http://particular.net/articles/the-nservicebus-host
    */
    public class EndpointConfig : IConfigureThisEndpoint
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
        }
    }
}
