using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Ultima.Vehicle.Service
{
    public class StartUp:IWantToRunWhenBusStartsAndStops
    {
        public void Start()
        {
            DrawStartup.Ultima();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
