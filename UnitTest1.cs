using NaughtyStrings;
using NUnit.Framework;
using System;
using Faker;
using System.Collections;
using System.Linq;

namespace NunitBugExample
{
    public class TestData
    {
        private static string GetRandomNaughtyString()
        {
            Random rnd = new Random();
            int r = rnd.Next(TheNaughtyStrings.All.Count);

            return TheNaughtyStrings.All.ElementAt(r).ToString();
        }

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("bob@bob.com").Returns(true);
                yield return new TestCaseData(Faker.InternetFaker.Email()).Returns(true);
                yield return new TestCaseData(Faker.InternetFaker.Domain()).Returns(false);
                yield return new TestCaseData(GetRandomNaughtyString()).Returns(false);
            }
        }

    }
    [TestFixture]
    class LogInTests
    {
        [TestCaseSource(typeof(TestData), "TestCases")]
        [TestCase("bob@bob.com", ExpectedResult = true)]
        [TestCase("yahoo.com",ExpectedResult = false)]
        [TestCase("<sc<script>ript>alert(123)</sc</script>ript>", ExpectedResult = false)]
        public bool CanVerifyEmail(string email)
        {
            Console.WriteLine();
            Console.WriteLine("Email input: {0}", email);

            return VerifyEmail(email);
        }

        private bool VerifyEmail(string email)
        {
            if (email.Contains('@'))
            {
                return true;
            }
            return false;
        }
    }
}
