using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public class FashionInfluencer : Influencer
    {
        private readonly double Factor = 0.1;
        private static readonly double Engagement = 4.0;

        public FashionInfluencer(string username, int followers) : base(username, followers, Engagement)
        {
        }

        public override int CalculateCampaignPrice()
        {
            return (int)Math.Floor(Followers * EngagementRate * Factor);
        }
    }
}
