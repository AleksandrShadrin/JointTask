using Core;
using FinanceUtilities;
using Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UsersRewardsTests
{
    public class Tests
    {
        private IUsersRewards usersRewards;
        [SetUp]
        public void Setup()
        {
            usersRewards = new UsersRewards();
        }

        [Test]
        public void BasicTest()
        {
            var users = GenerateUsers();
            var percent = 10;
            var trueAnswers = new double[] { 110, 1100, 11000 };
            var result = usersRewards.AddRewards(users, percent).ToArray();
            for(var i = 0; i < users.Length; i++)
            {
                var exceptedValue = trueAnswers[i];
                var actualValue = double.Parse(result[i].GetAmount());
                Assert.That(exceptedValue, Is.EqualTo(actualValue).Within(1).Percent);
            }
        }

        private User[] GenerateUsers()
        {
            var users = new User[]
            {
                new User("zxc", "100"),
                new User("qwe", "1000"),
                new User("asd", "10000")
            };
            return users;
        }
    }
}