using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Models.Services.Interfaces
{
    public interface ISourceReader<T>
    {
        IEnumerable<T> Read(SourceDescriptor descriptor);
    }
}
