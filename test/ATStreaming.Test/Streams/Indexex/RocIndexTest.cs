using ATStreaming.Streams.Indexes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using ATStreaming.Models;
using Moq;

namespace ATStreaming.Test.Streams.Indexex
{
    [TestClass]
    public class RocIndexTest
    {
        [TestMethod]
        public void Values_ThreeInputAndRocDelayTwo_OneValue()
        {
            var start = 1;
            var end = 3;
            var expected = (decimal)((end - start) / start * 100);
            var rocIndex = new RocIndex(end - start);

            var result = 0.0M;
            var indexValue = rocIndex.Values.Take(1).Subscribe(next => result = next);

            Observable.Range(start, end)
                .Select(x => new StockQuote { Close = x })
                .Subscribe(rocIndex);


            Assert.AreEqual(expected, result);
        }
    }
}
