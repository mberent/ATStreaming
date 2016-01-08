using ATStreaming.Models;
using ATStreaming.Models.Services.Interfaces;
using ATStreaming.Streams.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Test.Streams.Inputs
{
    [TestClass]
    public class StockQuotesStreamTest
    {
        [TestMethod]
        public void Start_SourceReaderThrowException_OnError()
        {
            //Arrange
            var sourceReaderMock = new Mock<ISourceReader<StockQuote>>();
            sourceReaderMock.Setup(x => x.Read(It.IsAny<SourceDescriptor>())).Throws(new Exception());

            var observerMock = new Mock<IObserver<StockQuote>>();
            var stockQuotesStream = new StockQuotesStream(sourceReaderMock.Object);

            //Act
            stockQuotesStream.Inputs.Subscribe(observerMock.Object);
            stockQuotesStream.Start(new SourceDescriptor("test_test.csv"));

            //Assert
            observerMock.Verify(x => x.OnError(It.IsAny<Exception>()));
        }

        [TestMethod]
        public void Start_SourceReaderReadNElements_NOnNextAndCompleted()
        {
            //Arrange
            var sourceReaderMock = new Mock<ISourceReader<StockQuote>>();
            var stockQuotes = new[] { new StockQuote(), new StockQuote() };
            sourceReaderMock.Setup(x => x.Read(It.IsAny<SourceDescriptor>())).Returns(stockQuotes);

            var observerMock = new Mock<IObserver<StockQuote>>();
            var stockQuotesStream = new StockQuotesStream(sourceReaderMock.Object);

            //Act
            stockQuotesStream.Inputs.Subscribe(observerMock.Object);
            stockQuotesStream.Start(new SourceDescriptor("test_test.csv"));

            //Assert
            observerMock.Verify(x => x.OnNext(It.IsAny<StockQuote>()), Times.Exactly(stockQuotes.Count()));
            observerMock.Verify(x => x.OnCompleted());
        }
    }
}
