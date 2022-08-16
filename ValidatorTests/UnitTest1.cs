using Interfaces;
using InputProcessing;
using NUnit.Framework;
using System;

namespace ValidatorTests
{
    public class Tests
    {
        private IValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new Validator();
        }

        [TestCase("Name\t1000", true)]
        [TestCase("Name\tasd", false)]
        [TestCase("Name\t32121\tzxc", false)]
        [TestCase("Name\t1", true)]
        public void BasicTest(string line, bool result)
        {
            Assert.AreEqual(result, validator.IsValid(line));
        }
    }
}