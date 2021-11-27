using System;
using Topshelf;

namespace Test2schedule
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Excute>(s =>
                {
                    s.ConstructUsing(excute => new Excute());
                    s.WhenStarted(excute => excute.Start());
                    s.WhenStopped(excute => excute.Stop());
                });

                x.RunAsLocalSystem();

                x.SetServiceName("ExecuteService");
                x.SetDisplayName("Execute Service");
                x.SetDescription("THUC HIEN LAY DU LIEU TU WEB");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetType());
            Environment.ExitCode = exitCodeValue;
        }
    }
}

