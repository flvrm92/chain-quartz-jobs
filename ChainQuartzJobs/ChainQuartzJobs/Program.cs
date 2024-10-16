// See https://aka.ms/new-console-template for more information
using ChainQuartzJobs.Jobs;
using Quartz;
using Quartz.Impl;

var scheduler = await StdSchedulerFactory.GetDefaultScheduler();

await scheduler.Start();

var firstJob = JobBuilder.Create<FirstJob>().WithIdentity("FirstJob").Build();
var secondJob = JobBuilder.Create<SecondJob>().WithIdentity("SecondJob").Build();

await scheduler.ScheduleJob(firstJob, TriggerBuilder.Create().StartAt(DateTime.Now.AddSeconds(5)).Build());

scheduler.ListenerManager.AddJobListener(new ChainingJobListener(scheduler, secondJob));

await Task.Delay(TimeSpan.FromSeconds(60));

await scheduler.Shutdown();