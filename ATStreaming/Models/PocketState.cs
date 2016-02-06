using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Models
{
    public class PocketState
    {
        public decimal Value { get; set; }
        public DateTime? Date { get; set; }

        public override string ToString()
        {
            return Date.HasValue ?
                String.Format("{0} {1}", Date.Value.ToShortDateString(), Value) :
                Value.ToString();
        }
    }
}
