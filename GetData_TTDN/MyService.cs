using log4net;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetData_TTDN
{
    internal class MyService
    {
       
        private static readonly ILog log = LogManager.GetLogger(typeof(MyService));
        public MyService()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config"));
        }
        public async Task Start()
        {
            await Scheduler();
            log.Info("Start success \n");
        }

        public async Task Stop()
        {
            log.Info("Stop success \n");
        }
        public class connect
        {
            public string source { get; set; }
        }
        public class login
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class timestart
        {
            public int hours_start { get; set; }
            public int minutes_start { get; set; }
        }

        public class timestop
        {
            public int hours_stop { get; set; }
            public int minutes_stop { get; set; }
        }

        public class timerepeat
        {
            public int minutes_repeat { get; set; }
        }

        public class settime
        {
            public string time { get; set; }
        }
        private static async Task Scheduler()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("Appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            var timestart = config.GetSection("timestart").Get<timestart>();
            var timestop = config.GetSection("timestop").Get<timestop>();
            var timerepeat = config.GetSection("timerepeat").Get<timerepeat>();
            var settime = config.GetSection("settime").Get<settime>();

            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            
            IJobDetail job = JobBuilder.Create<Job>()
                .WithIdentity("myJob", "group1")
                .Build();


            ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity("trigger", "group1")
             .StartNow()
             .WithCronSchedule(settime.time)
             .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}
