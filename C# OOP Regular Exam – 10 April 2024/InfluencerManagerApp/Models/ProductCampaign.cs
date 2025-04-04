using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public class ProductCampaign : Campaign
    {
        private static readonly double TotalBudget = 60000;
        public ProductCampaign(string brand) : base(brand, TotalBudget)
        {
        }

        //public static HashSet<string> AllowedTypes = new() { nameof(BusinessInfluencer), nameof(FashionInfluencer) };
    }
}
