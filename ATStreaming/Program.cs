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
using System.IO;

namespace ATStreaming
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            var directory = "Files";

            foreach (var filePath in Directory.EnumerateFiles(directory))
            {
                var descriptor = new SourceDescriptor(filePath);
                var reader = new SourceReader();
                var delay = 10;
                var inputSource = new StockQuotesStream(reader);
                var rocIndex = new RocIndex(delay);

                inputSource.Inputs.Subscribe(rocIndex);
                var pocket = new Pocket(inputSource.Inputs.Skip(delay), rocIndex.Values.Select(x => x > 0 ? true : false), 1000);

                decimal finalValue = 0;
                pocket.Money.Subscribe(Console.WriteLine);

                inputSource.Start(descriptor);
                Console.WriteLine(String.Format("{0}: {1}", Path.GetFileName(filePath), finalValue));
            }
        }
    }
}
