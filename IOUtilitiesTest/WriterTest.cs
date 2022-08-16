using Core;
using Interfaces;
using IOUtilities;
using IOUtilities.Exceptions;
using System.Reflection;
using Xunit.Sdk;

namespace IOUtilitiesTest
{

    public class WriterTest : BeforeAfterTestAttribute
    {
        string testFolder = Path.Combine(".", "test");
        string existedFile = "ExistedFile.txt";
        string usersFile = "users.txt";
        public WriterTest()
        {
            Directory.CreateDirectory(testFolder);
            ClearTestFolder();
            FileInfo file = new(Path.Combine(testFolder, existedFile));
            file.CreateText().Close();
        }
        [Fact]
        public void WriterShouldThrowFileExistException()
        {
            IWriter writer = new Writer(Path.Combine(testFolder, existedFile));
            var act = () => writer.Write(GetUsers());
            Assert.Throws<FileExistException>(act);
        }
        [Fact]
        public void WriterShouldWriteToFileUsers()
        {
            IWriter writer = new Writer(Path.Combine(testFolder, usersFile));
            writer.Write(GetUsers());

            Assert.Collection<string>(ReadUserFile(),
                user => Assert.Equal("user1\t12345", user),
                user => Assert.Equal("user2\t12345", user),
                user => Assert.Equal("user3\t12345", user),
                user => Assert.Equal("user4\t12345", user),
                user => Assert.Equal("user5\t12345", user));
        }

        private IEnumerable<string> ReadUserFile()
        {
            using (var sr = File.OpenText(Path.Combine(testFolder, usersFile)))
            {
                string line;
                while (String.IsNullOrEmpty(line = sr.ReadLine()) == false)
                    yield return line;
            }
        }
        private void ClearTestFolder()
        {
            var directory = new DirectoryInfo(testFolder);
            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }
        }

        private IEnumerable<User> GetUsers()
            => new List<User>
            {
                new User("user1", "12345"),
                new User("user2", "12345"),
                new User("user3", "12345"),
                new User("user4", "12345"),
                new User("user5", "12345"),
            };
    }
}