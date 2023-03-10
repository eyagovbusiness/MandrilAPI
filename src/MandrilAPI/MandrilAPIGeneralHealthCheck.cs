using MandrilBot;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MandrilAPI
{
    public class MandrilAPIGeneralHealthCheck : IHealthCheck
    {
        public MandrilAPIGeneralHealthCheck()
        {

        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext aContext, CancellationToken aCancellationToken = default)
        {
            var lAllocatedMegaBytes = GC.GetTotalMemory(forceFullCollection: false) / 1000000; // divided to get MB

            if(lAllocatedMegaBytes >= 25)
            {
                return Task.FromResult(HealthCheckResult.Degraded($"Large GC memory heap: {lAllocatedMegaBytes} MB"));
            }
            else if(lAllocatedMegaBytes >=30) 
            {
                GC.Collect();
                return Task.FromResult(HealthCheckResult.Unhealthy($"Too large GC memory heap: {lAllocatedMegaBytes} MB"));
            }
            else
                return Task.FromResult(HealthCheckResult.Healthy($"Good size for the GC memory heap: {lAllocatedMegaBytes} MB"));

        }
    }
}