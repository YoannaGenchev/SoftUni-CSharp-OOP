using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Models
{
    public class BloggerInfluencer : Influencer
    {
        private readonly double Factor = 0.2;
        private static readonly double Engagement = 2.0;

        public BloggerInfluencer(string username, int followers) : base(username, followers, Engagement)
        {
        }

        public override int CalculateCampaignPrice()
        {
            return (int)Math.Floor(Followers * EngagementRate * Factor);
        }
    }
}
