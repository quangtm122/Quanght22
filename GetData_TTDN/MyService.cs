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
            log.Info("Start success \n");
        }
        public async Task Start()
        {
            await Scheduler();
        }

        public async Task Stop()
        {         

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
        private static async Task Scheduler()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("Appsettings.json", optional: false);

            IConfiguration config = builder.Build();
            var timestart = config.GetSection("timestart").Get<timestart>();
            var timestop = config.GetSection("timestop").Get<timestop>();
            var timerepeat = config.GetSection("timerepeat").Get<timerepeat>();

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
             .WithDailyTimeIntervalSchedule
                  (s =>
                      s.WithIntervalInMinutes(timerepeat.minutes_repeat)
                       .OnEveryDay()
                      .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(timestart.hours_start, timestart.minutes_start)) // start at
                      .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(timestop.hours_stop, timestop.minutes_stop)) // stop at
                  )
                .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}
