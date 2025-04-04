using InfluencerManagerApp.Core.Contracts;
using InfluencerManagerApp.Models;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories;
using InfluencerManagerApp.Repositories.Contracts;
using InfluencerManagerApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Core
{
    public class Controller : IController
    {
        private InfluencerRepository influencers;
        private CampaignRepository campaigns;

        public Controller()
        {
            influencers = new InfluencerRepository();
            campaigns = new CampaignRepository();
        }

        public string ApplicationReport()
        {
            var sb = new StringBuilder();
            var orderedInfluencers = influencers.Models.OrderByDescending(m => m.Income).ThenByDescending(m => m.Followers);
            foreach (var influencer in orderedInfluencers)
            {
                sb.AppendLine(influencer.ToString());
                if (false == influencer.Participations.Any())
                {
                    continue;
                }

                sb.AppendLine("Active Campaigns:");
                var orderedCampaigns = influencer.Participations.OrderBy(p => p);
                foreach (var campaignName in orderedCampaigns)
                {
                    ICampaign campaign = campaigns.Models.FirstOrDefault(m => m.Brand == campaignName);
                    sb.AppendLine($"--{campaign.ToString()}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string AttractInfluencer(string brand, string username)
        {
            IInfluencer influencer = influencers.Models.FirstOrDefault(m => m.Username == username);
            if (influencer == null)
            {
                return string.Format(OutputMessages.InfluencerNotFound, influencers.GetType().Name, username);
            }

            ICampaign campaign = campaigns.Models.FirstOrDefault(m => m.Brand == brand);
            if (campaign == null)
            {
                return string.Format(OutputMessages.CampaignNotFound, brand);
            }

            if (influencer.Participations.Contains(brand))
            {
                return string.Format(OutputMessages.InfluencerAlreadyEngaged, username, brand);
            }

            if ((campaign.GetType().Name == nameof(ProductCampaign) && influencer.GetType().Name == nameof(BloggerInfluencer)) ||
                (campaign.GetType().Name == nameof(ServiceCampaign) && influencer.GetType().Name == nameof(FashionInfluencer)))
            {
                return string.Format(OutputMessages.InfluencerNotEligibleForCampaign, username, brand);
            }

            if (campaign.Budget < influencer.CalculateCampaignPrice())
            {
                return string.Format(OutputMessages.UnsufficientBudget, brand, username);
            }

            var amount = influencer.CalculateCampaignPrice();
            influencer.EnrollCampaign(brand);
            influencer.EarnFee(amount);

            campaign.Engage(influencer);
            return string.Format(OutputMessages.InfluencerAttractedSuccessfully, username, brand);
        }

        public string BeginCampaign(string typeName, string brand)
        {
            if (typeName != nameof(ProductCampaign) &&
                typeName != nameof(ServiceCampaign))
            {
                return string.Format(OutputMessages.CampaignTypeIsNotValid, typeName);
            }

            if (campaigns.Models.Any(m => m.Brand == brand))
            {
                return string.Format(OutputMessages.CampaignDuplicated, brand);
            }

            ICampaign campaign;
            if (typeName == nameof(ProductCampaign))
            {
                campaign = new ProductCampaign(brand);
            }
            else
            {
                campaign = new ServiceCampaign(brand);
            }

            campaigns.AddModel(campaign);
            return string.Format(OutputMessages.CampaignStartedSuccessfully, brand, typeName);
        }

        public string CloseCampaign(string brand)
        {
            ICampaign campaign = campaigns.Models.FirstOrDefault(m => m.Brand == brand);
            if (campaign == null)
            {
                return string.Format(OutputMessages.InvalidCampaignToClose);
            }

            const double minimumBudget = 10000;
            if (campaign.Budget <= minimumBudget)
            {
                return string.Format(OutputMessages.CampaignCannotBeClosed, brand);
            }

            const double bonusAmount = 2000;
            foreach(var contributor in campaign.Contributors)
            {
                var influencer = influencers.Models.FirstOrDefault(m => m.Username == contributor);
                influencer.EarnFee(bonusAmount);
                influencer.EndParticipation(brand);
            }

            campaigns.RemoveModel(campaign);
            return string.Format(OutputMessages.CampaignClosedSuccessfully, brand);
        }

        public string ConcludeAppContract(string username)
        {
            IInfluencer influencer = influencers.Models.FirstOrDefault(m => m.Username == username);
            if (influencer == null)
            {
                return string.Format(OutputMessages.InfluencerNotSigned, username);
            }

            if (influencer.Participations.Any())
            {
                return string.Format(OutputMessages.InfluencerHasActiveParticipations, username);
            }

            influencers.RemoveModel(influencer);
            return string.Format(OutputMessages.ContractConcludedSuccessfully, username);
        }

        public string FundCampaign(string brand, double amount)
        {
            ICampaign campaign = campaigns.Models.FirstOrDefault(m => m.Brand == brand);
            if (campaign == null)
            {
                return string.Format(OutputMessages.InvalidCampaignToFund);
            }

            if (amount <= 0)
            {
                return string.Format(OutputMessages.NotPositiveFundingAmount);
            }

            campaign.Gain(amount);
            return string.Format(OutputMessages.CampaignFundedSuccessfully, brand, amount);
        }

        public string RegisterInfluencer(string typeName, string username, int followers)
        {
            if (typeName != nameof(BusinessInfluencer) &&
                typeName != nameof(FashionInfluencer) &&
                typeName != nameof(BloggerInfluencer))
            {
                return string.Format(OutputMessages.InfluencerInvalidType, typeName);
            }

            if (influencers.Models.Any(m => m.Username == username))
            {
                return string.Format(OutputMessages.UsernameIsRegistered, username, influencers.GetType().Name);
            }

            IInfluencer influencer;
            if (typeName == nameof(BusinessInfluencer))
            {
                influencer = new BusinessInfluencer(username, followers);
            }
            else if (typeName == nameof(FashionInfluencer))
            {
                influencer = new FashionInfluencer(username, followers);
            }
            else
            {
                influencer = new BloggerInfluencer(username, followers);
            }

            influencers.AddModel(influencer);
            return string.Format(OutputMessages.InfluencerRegisteredSuccessfully, username);
        }
    }
}
