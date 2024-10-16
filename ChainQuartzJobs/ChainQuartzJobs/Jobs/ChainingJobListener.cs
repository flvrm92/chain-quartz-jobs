using Quartz;

namespace ChainQuartzJobs.Jobs;
internal class ChainingJobListener(IScheduler scheduler, IJobDetail nextJob) : IJobListener
{
  public string Name => "ChainingJobListener";

  public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
    => await Task.CompletedTask;

  public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    => await Task.CompletedTask;

  public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
  {
    if (jobException == null)
    {
      if (!context.JobDetail.Key.Equals(new JobKey("FirstJob"))) await Task.CompletedTask;
      
      var firstJobResult = context.JobDetail.JobDataMap["FirstJobResult"] as Dictionary<int, string>;
      if (firstJobResult is not null) nextJob.JobDataMap["FirstJobResult"] = firstJobResult;

      await scheduler.ScheduleJob(nextJob, TriggerBuilder.Create().StartNow().Build(), cancellationToken);
    }
  }
}
