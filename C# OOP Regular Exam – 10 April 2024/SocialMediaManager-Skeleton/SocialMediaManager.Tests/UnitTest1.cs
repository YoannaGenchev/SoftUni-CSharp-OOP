using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace SocialMediaManager.Tests
{
    public class Tests
    {
        private static string GetRandomString()
        {
            var randomTextLength = Random.Shared.Next(minValue: 5, maxValue: 50);
            return GetRandomString(randomTextLength);
        }

        private static string GetRandomString(int length)
        {
            var symbols = new char[length];
            for (var i = 0; i < length; i++)
            {
                var randomLetterIndex = Random.Shared.Next(maxValue: 26);
                symbols[i] = (char)('a' + randomLetterIndex);
            }

            return new string(symbols);
        }

        private InfluencerRepository influencerRepository;
        private Influencer influencer;

        [SetUp]
        public void Setup()
        {
            influencerRepository = new InfluencerRepository();

            string username = GetRandomString();
            int followers = Random.Shared.Next();

            influencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(influencer);
        }

        [Test]
        public void Test()
        {
            Assert.Pass();
        }

        [Test]
        public void TestInfluencerCreation()
        {
            string username = GetRandomString();
            int followers = Random.Shared.Next();

            var influencer = new Influencer(username, followers);
            Assert.IsNotNull(influencer);
            Assert.AreEqual(username, influencer.Username);
            Assert.AreEqual(influencer.Followers, followers);
        }

        [Test]
        public void TestInfluencerRepositoryCreation()
        {
            var testInfluencerRepository = new InfluencerRepository();
            Assert.IsNotNull(testInfluencerRepository);
            Assert.IsEmpty(testInfluencerRepository.Influencers);
        }

        [Test]
        public void TestRegisterInfluencerWorksCorretly()
        {
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
            string username = GetRandomString();
            int followers = Random.Shared.Next();

            var testInfluencer = new Influencer(username, followers);
            influencerRepository.RegisterInfluencer(testInfluencer);
            Assert.AreEqual(influencerRepository.Influencers.Count, 2);
            Assert.AreEqual(influencerRepository.Influencers.Last(), testInfluencer);
        }

        [Test]
        public void TestRegisterInfluencerThrowsCorretly()
        {
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RegisterInfluencer(null));
            Assert.Throws<InvalidOperationException>(() => influencerRepository.RegisterInfluencer(influencer));
        }

        [Test]
        public void TestRemoveInfluencerWorksCorretly()
        {
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);
            string randomString = GetRandomString();

            Assert.IsFalse(influencerRepository.RemoveInfluencer(randomString));
            Assert.AreEqual(influencerRepository.Influencers.Count, 1);

            Assert.IsTrue(influencerRepository.RemoveInfluencer(influencer.Username));
            Assert.IsTrue(influencerRepository.Influencers.Count == 0);
        }

        [Test]
        public void TestRemoveInfluencerThrowsCorretly()
        {
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RemoveInfluencer(null));
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RemoveInfluencer(" "));
            Assert.Throws<ArgumentNullException>(() => influencerRepository.RemoveInfluencer(string.Empty));
        }

        [Test]
        public void TestGetInfluencerWorksCorretly()
        {
            string randomString = GetRandomString();

            Assert.IsNull(influencerRepository.GetInfluencer(randomString));
            Assert.IsNotNull(influencerRepository.GetInfluencer(influencer.Username));
            Assert.AreEqual(influencerRepository.GetInfluencer(influencer.Username), influencer);
        }

        [Test]
        public void TestGetInfluencerWithMostFollowersWorksCorrectly()
        {
            string username = GetRandomString();
            var testInfluencer = new Influencer(username, influencer.Followers - 1);
            influencerRepository.RegisterInfluencer(testInfluencer);

            Assert.IsNotNull(influencerRepository.GetInfluencerWithMostFollowers());
            Assert.AreEqual(influencerRepository.GetInfluencerWithMostFollowers(), influencer);
        }
    }
}