using ATStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace ATStreaming.Streams.Outputs
{
    public class Pocket
    {
        private decimal _money;
        private long _pocketSize;
        private StockQuote _finalStockQuote;

        public Pocket(IObservable<StockQuote> delayedInputs, IObservable<bool> signals, decimal money)
        {
            _money = money;

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
                var toBuy = (int)(_money / quote.StockQuote.Close);
                _money -= toBuy * quote.StockQuote.Close;
                _pocketSize = toBuy;
            }
            else
            {
                _money += _pocketSize * quote.StockQuote.Close;
                _pocketSize = 0;
            }
        }

        private void SellAll()
        {
            if (_finalStockQuote != null)
            {
                _money += _pocketSize * _finalStockQuote.Close;
                _pocketSize = 0;
            }
        }
    }
}
