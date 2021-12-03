using log4net;
using System;
using System.Threading.Tasks;
using Topshelf;

namespace GetData_TTDN
{
    class Program
    {

        static async Task Main(string[] args)
        {

            HostFactory.Run(x =>
            {
                x.Service<MyService>(s =>
                {
                    s.ConstructUsing(name => new MyService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetServiceName("MYSERVICE");
                x.SetDisplayName("MY_SERVICE");
                x.SetDescription("GET DATA");


            });
        }
        
    }
}

