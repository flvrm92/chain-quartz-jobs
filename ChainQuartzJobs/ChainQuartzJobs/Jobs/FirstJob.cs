using Quartz;

namespace ChainQuartzJobs.Jobs;
public class FirstJob : IJob
{
  public async Task Execute(IJobExecutionContext context)
  {
    Console.WriteLine("FirstJob is running...");
    await Task.Delay(TimeSpan.FromSeconds(5)); // Simulate some work

    var dictionary = new Dictionary<int, string>
    {
      { 0, "Test 1" },
      { 1, "Test 2" }
    };
    
    context.JobDetail.JobDataMap["FirstJobResult"] = dictionary;

    Console.WriteLine("FirstJob completed.");
  }
}
