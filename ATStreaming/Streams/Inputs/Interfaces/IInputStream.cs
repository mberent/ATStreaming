using ATStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Streams.Inputs.Interfaces
{
    public interface IInputStream<T>
    {
        IObservable<T> Inputs { get; }
        void Start(SourceDescriptor descriptor);
    }
}
