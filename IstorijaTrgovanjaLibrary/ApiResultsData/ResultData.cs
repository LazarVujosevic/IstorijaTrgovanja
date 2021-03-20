using System;
using System.Collections.Generic;
using System.Text;

namespace IstorijaTrgovanjaLibrary.ApiResultsData
{
    public class ResultData
    {
        public DateTime? CreatedTime { get; set; }

        public double? Open { get; set; }

        public double? High { get; set; }

        public double? Low { get; set; }

        public double? Close { get; set; }

        public int? Volume { get; set; }

        public double? MovingAverage { get; set; }
    }
}
