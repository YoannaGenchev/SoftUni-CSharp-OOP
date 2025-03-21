using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        public string RandomString()
        {
            int index = Random.Shared.Next(this.Count);
            string str = this[index];
            this.RemoveAt(index);
            return str;
        }
    }
}
