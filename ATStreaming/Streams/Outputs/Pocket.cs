using ATStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ATStreaming.Streams.Outputs
{
    public class Pocket : IDisposable
    {
        private Subject<PocketState> _moneySubject = new Subject<PocketState>();

        private decimal _money;

        public IObservable<PocketState> Money
        {
            get
            {
                return _moneySubject.AsObservable();
            }
        }

        private long _pocketSize;
        private StockQuote _finalStockQuote;

        public Pocket(IObservable<StockQuote> delayedInputs, IObservable<bool> signals, decimal money)
        {
            _money = money;
            _moneySubject.OnNext(new PocketState { Value = money });

            delayedInputs.LastAsync().Subscribe(final => _finalStockQuote = final);

            delayedInputs
                .Zip(signals, (input, signal) => new StockQuoteSignal { StockQuote = input, Signal = signal })
                .DistinctUntilChanged(selector => selector.Signal)
                .Subscribe(BuyOrSell, SellAll);
        }

        private void BuyOrSell(StockQuoteSignal quote)
        {
            if (quote.Signal)
            {
                var toBuy = (int)(_money / quote.StockQuote.Open);
                _money -= toBuy * quote.StockQuote.Open;
                _pocketSize = toBuy;
            }
            else
            {
                _money += _pocketSize * quote.StockQuote.Open;
                _pocketSize = 0;

                _moneySubject.OnNext(new PocketState { Value = _money, Date = quote.StockQuote.Date });
            }
        }

        private void SellAll()
        {
            if (_finalStockQuote != null)
            {
                _money += _pocketSize * _finalStockQuote.Close;
                _pocketSize = 0;

                _moneySubject.OnNext(new PocketState { Value = _money });
                _moneySubject.OnCompleted();
            }
        }

        public void Dispose()
        {
            if (_moneySubject != null)
            {
                _moneySubject.Dispose();
            }
        }
    }
}
