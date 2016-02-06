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
        public void Values_ThreeInputsAndDelayTwo_OneValue()
        {
            var start = 1;
            var end = 3;
            var expected = (decimal)((end - start) / start * 100);
            var rocIndex = new RocIndex(end - start);

            var result = 0.0M;
            var indexValue = rocIndex.Values.FirstAsync().Subscribe(next => result = next);

            Observable.Range(start, end)
                .Select(x => new StockQuote { Open = x })
                .Subscribe(rocIndex);


            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Values_SixInputsAndDelayTwo_FourValues()
        {
            var delay = 2;
            var inputs = new[] { 22.1M, 22.4M, 21, 19M, 20.5M, 22M };
            var rocIndex = new RocIndex(delay);

            decimal[] indexValues = null;

            rocIndex.Values.ToList().Subscribe(next => indexValues = next.ToArray());

            Observable.ToObservable(inputs)
                .Select(x => new StockQuote { Open = x })
                .Subscribe(rocIndex);

            var expected = inputs
                .Skip(delay)
                .Select((x, i) => (x - inputs[i]) / inputs[i] * 100);

            CollectionAssert.AreEqual(expected.ToArray(), indexValues);
        }
    }
}
