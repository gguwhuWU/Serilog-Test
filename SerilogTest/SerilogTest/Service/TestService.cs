using Microsoft.Extensions.Logging;
using SerilogTest.Extensions;
using SerilogTest.Interface.Service;
using System;

namespace SerilogTest.Service
{
    public class TestService : ITestService
    {
        private readonly ILogger<TestService> logger;
        public TestService(ILogger<TestService> logger)
        {
            this.logger = logger;
        }

        public void TestLog()
        {
            try
            {
                logger.LogInformation($"jojo~~~5");
                throw new Exception("my error!!!");
            }
            catch (Exception ex)
            {
                logger.LogAppError(ex.Message);
            }
        }

        public void TestLog2()
        {
            try
            {
                logger.LogInformation("the world~~~");
            }
            catch (Exception ex)
            {
                logger.LogAppError(ex.Message);
            }
        }
    }
}
