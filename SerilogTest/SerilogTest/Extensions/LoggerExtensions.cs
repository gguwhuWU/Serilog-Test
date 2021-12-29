using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Runtime.CompilerServices;

namespace SerilogTest.Extensions
{
    /// <summary>
    /// 參考:https://stackoom.com/question/3Dcsf/C-ASP-NET-Core-Serilog%E6%B7%BB%E5%8A%A0%E7%B1%BB%E5%90%8D%E5%92%8C%E6%96%B9%E6%B3%95%E8%BF%9B%E8%A1%8C%E8%AE%B0%E5%BD%95
    /// </summary>
    public static class LoggerExtensions
    {
        public static void LogAppError<T>(this ILogger<T> logger, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            using var prop = LogContext.PushProperty("MemberName", memberName);
            LogContext.PushProperty("FilePath", sourceFilePath);
            LogContext.PushProperty("LineNumber", sourceLineNumber);
            logger.LogError(message);
        }
    }
}
