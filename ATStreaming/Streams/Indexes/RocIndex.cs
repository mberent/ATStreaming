﻿using ATStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace ATStreaming.Streams.Indexes
{
    public class RocIndex : IObserver<StockQuote>, IDisposable
    {
        private readonly Subject<StockQuote> _stockQuotes;

        public IObservable<decimal> Values { get; private set; }

        public RocIndex(int delay)
        {
            _stockQuotes = new Subject<StockQuote>();
            Values = _stockQuotes
                .Skip(delay)
                .Zip(_stockQuotes, (actual, past) => (actual.Open - past.Open) / past.Open * 100)
                .AsObservable();
        }

        public void OnCompleted()
        {
            _stockQuotes.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _stockQuotes.OnError(error);
        }

        public void OnNext(StockQuote value)
        {
            _stockQuotes.OnNext(value);
        }

        public void Dispose()
        {
            if (_stockQuotes != null)
            {
                _stockQuotes.Dispose();
            }
        }
    }
}
