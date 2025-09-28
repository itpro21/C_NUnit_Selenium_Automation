using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AdvantageShoppingTests.Utils
{
    public class TestDataReader
    {
        private static string TestDataPath => Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestData");

        public static IEnumerable<TestCaseData> GetRegisterUsers()
        {
            var filePath = Path.Combine(TestDataPath, "TestData.json");
            var json = File.ReadAllText(filePath);
            var users = JsonConvert.DeserializeObject<List<dynamic>>(json);

            foreach (var u in users)
            {
                yield return new TestCaseData((string)u.username, (string)u.email, (string)u.password, (string)u.confirmPassword);
            }
        }
    }
}