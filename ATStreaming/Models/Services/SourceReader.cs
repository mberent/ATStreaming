using ATStreaming.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Models.Services
{
    public class SourceReader : ISourceReader<StockQuote>
    {
        public IEnumerable<StockQuote> Read(SourceDescriptor descriptor)
        {
            if (!File.Exists(descriptor.FilePath))
            {
                throw new FileNotFoundException(descriptor.FilePath);
            }

            foreach (var line in File.ReadAllLines(descriptor.FilePath).Skip(1).Reverse())
            {
                var row = line.Split(',');
                var stockQuote = new StockQuote
                {
                    Company = descriptor.Company,
                    Market = descriptor.Market,
                    Date = DateTime.Parse(row[0]),
                    Open = Decimal.Parse(row[1], CultureInfo.InvariantCulture),
                    High = Decimal.Parse(row[2], CultureInfo.InvariantCulture),
                    Low = Decimal.Parse(row[3], CultureInfo.InvariantCulture),
                    Close = Decimal.Parse(row[4], CultureInfo.InvariantCulture),
                    Volume = Int64.Parse(row[5]),
                    Turnover = Decimal.Parse(row[6], CultureInfo.InvariantCulture)
                };


                yield return stockQuote;
            }
        }
    }
}
