using ExecutiveOffice.EDT.FileOps.Common;
using ExecutiveOffice.EDT.FileOps.Pipe;
using ExecutiveOffice.EDT.FileOps.Tests.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Tests.IntegrationTests.Pipe
{
    [TestClass]
    public class ToTests
    {

        private readonly FileInfo _oneTxtFileWithZeroFileSuffix = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "to.sftp.OneTxt.settings.json"));

        private readonly FileInfo _multipleTxtFilesWithZeroFileSuffix = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "to.sftp.OneTxt.settings.json"));

        private readonly FileInfo _sharedSettings = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "shared.settings.json"));

        private readonly DirectoryInfo _workingDirectory = new DirectoryInfo(Path.Combine("Pipe", "WorkingDirectory"));

        private readonly DirectoryInfo _testData = new DirectoryInfo(Path.Combine("IntegrationTests", "Pipe", "Data"));

        private readonly FileInfo _testFile1;

        private readonly FileInfo _testFile2;


        public ToTests()
        {
            _testFile1 = new FileInfo(Path.Combine(_workingDirectory.FullName, "testFile1.txt"));
            _testFile2 = new FileInfo(Path.Combine(_workingDirectory.FullName, "testFile2.txt"));
        }

        [TestInitialize]
        public void Initialize()
        {
            _workingDirectory
                .DeleteWithContentIfExists()
                .CreateIfNotExists();

            FileProvider.Sftp.FromSettings
               .DeleteWithContentIfExists()
               .CreateIfNotExists();

            FileProvider.Sftp.ToSettings
                .DeleteWithContentIfExists()
                .CreateIfNotExists();

            _testData.CopyContentTo(FileProvider.Sftp.FromSettings);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _workingDirectory.DeleteWithContentIfExists();
            FileProvider.Sftp.FromSettings.DeleteWithContentIfExists();
            FileProvider.Sftp.ToSettings.DeleteWithContentIfExists();
        }


        [TestMethod]
        public void ZeroSizeFileIsCreatedOnSftp()
        {
            //Arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_oneTxtFileWithZeroFileSuffix)
                .AddConfiguration(_sharedSettings)
                .Build();

            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");

            //act
            fileOpsManager.Execute();

            //assert
            FileProvider.Sftp.ToSettings.CopyContentTo(_workingDirectory);

            Assert.AreEqual(2, _workingDirectory.GetFiles().Length);

            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "testFile1.txt")));

            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "testFile1.txt.sm")));
        }


        [TestMethod]
        public void ZeroSizefilesAreCreatedOnSftp()
        {

        }


        [TestMethod]
        public void ZeroSizefileIsNotCreatedOnSftp()
        {

        }


        [TestMethod]
        public void ZeroSizefileIsWronglyDefinedOnSftp()
        {

        }



        [TestMethod]
        public void ZeroSizefileIsCreatedOnLocal()
        {

        }


        [TestMethod]
        public void ZeroSizefilesAreCreatedOnLocal()
        {

        }


        [TestMethod]
        public void ZeroSizefileIsNotCreatedOnLocal()
        {

        }


        [TestMethod]
        public void ZeroSizefileIsWronglyDefinedOnLocal()
        {

        }

    }
}
