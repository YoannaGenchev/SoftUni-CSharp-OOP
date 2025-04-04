using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes.Attributes
{
    public class MyRangeAttribute : MyValidationAttribute
    {
        public int MinValue { get; }
        public int MaxValue { get; }

        public MyRangeAttribute(int minValue, int maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public override bool IsValid(object obj)
        {
            return obj is int num && this.MinValue <= num && num <= this.MaxValue;
        }
    }
}
