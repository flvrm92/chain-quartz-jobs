using Quartz;

namespace ChainQuartzJobs.Jobs;
public class SecondJob : IJob
{
  public async Task Execute(IJobExecutionContext context)
  {
    Console.WriteLine("SecondJob is running...");

    var result = context.JobDetail.JobDataMap["FirstJobResult"] as Dictionary<int, string>;
    if (result is not null)
    {
      result.TryGetValue(1, out var value);
      Console.WriteLine(value);
    }

    await Task.Delay(TimeSpan.FromSeconds(5)); // Simulate some work
    Console.WriteLine("SecondJob completed.");
  }
}
