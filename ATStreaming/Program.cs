using ATStreaming.Models;
using ATStreaming.Models.Services;
using ATStreaming.Streams.Inputs;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using ATStreaming.Streams.Indexes;
using ATStreaming.Streams.Outputs;

namespace ATStreaming
{
    class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            var descriptor = new SourceDescriptor(@"Files\APPLE_NASDAQ.csv");
            var reader = new SourceReader();
            var delay = 6;
            var inputSource = new StockQuotesStream(reader);
            var rocIndex = new RocIndex(delay);

            inputSource.Inputs.Subscribe(rocIndex);
            var pocket = new Pocket(inputSource.Inputs.Skip(delay), rocIndex.Values.Select(x => x > 0 ? true : false), 1000);

            inputSource.Start(descriptor);

        }
    }
}
