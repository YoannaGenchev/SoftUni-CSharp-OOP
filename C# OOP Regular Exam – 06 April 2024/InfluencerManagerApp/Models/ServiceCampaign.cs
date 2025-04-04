using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public class ServiceCampaign : Campaign
    {
        private static readonly double TotalBudget = 30000;
        public ServiceCampaign(string brand) : base(brand, TotalBudget)
        {
        }

        //public static HashSet<string> AllowedTypes = new() { nameof(BusinessInfluencer), nameof(BloggerInfluencer) };
    }
}
