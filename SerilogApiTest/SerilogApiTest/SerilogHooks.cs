using Serilog.Sinks.File.Archive;
using System;
using System.IO.Compression;

namespace SerilogApiTest
{
    /// <summary>
    /// 參考:https://github.com/cocowalla/serilog-sinks-file-archive
    /// </summary>
    public class SerilogHooks
    {
        public static ArchiveHooks MyArchiveHooks => new ArchiveHooks(CompressionLevel.Fastest, $"{AppDomain.CurrentDomain.BaseDirectory}\\Logs");
    }
}
