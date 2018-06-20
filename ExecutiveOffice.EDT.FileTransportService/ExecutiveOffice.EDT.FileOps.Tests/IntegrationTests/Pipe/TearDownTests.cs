using ExecutiveOffice.EDT.FileOps.Common;
using ExecutiveOffice.EDT.FileOps.Pipe;
using ExecutiveOffice.EDT.FileOps.Tests.Common.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Tests.IntegrationTests.Pipe
{

    [TestClass]
    public class TearDownTests
    {
        private readonly FileInfo _unzipMultipleFiles = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "unzip.sftp.TwoFilesInside.settings.json"));
        private readonly FileInfo _settingsUnzipOneFile = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "unzip.sftp.OneFileInside.settings.json"));
        private readonly FileInfo _settingsUnzipZeroFile = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "unzip.sftp.ZeroFileInside.settings.json"));
        private readonly FileInfo _settingsUnzipEmptyfolder = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "unzip.sftp.EmptyFolder.settings.json"));
        private readonly FileInfo _settingsTearDownEmptySettings = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "tearDown.empty.settings.json"));
        private readonly FileInfo _sharedSettings = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "shared.settings.json"));
        private readonly FileInfo _settingsUnzipFromMultipleZips = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "unzip.sftp.AllZip.settings.json"));
        private readonly FileInfo _settingsWithoutFrom = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "tearDown.WithoutFrom.settings.json"));


        private readonly FileInfo _settingsLocalUnzipFromMultipleZips = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "unzip.local.AllZip.settings.json"));
        private readonly FileInfo _settingsLocalUnzipOneFile = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "unzip.local.OneFileInside.settings.json"));
        private readonly FileInfo _settingsLocalUnzipEmpty = new FileInfo(Path.Combine("IntegrationTests", "Pipe", "Configuration", "unzip.local.Empty.settings.json"));
        

        private readonly FileInfo _twoFilesInsideOneZip;
        private readonly FileInfo _oneFileInsideOneZip;
        private readonly FileInfo _noFileInsideOneZip;

        private readonly FileInfo _testFile1;
        private readonly FileInfo _testFile2;

        private readonly DirectoryInfo _testData = new DirectoryInfo(Path.Combine("IntegrationTests", "Pipe", "Data"));

        private readonly DirectoryInfo _workingDirectory =
          new DirectoryInfo(Path.Combine("Pipe", "WorkingDirectory"));


        private readonly DirectoryInfo _localDirectory =
          new DirectoryInfo(Path.Combine("Pipe", "LocalDirectory"));

        private readonly DirectoryInfo _localDirectoryEmpty =
          new DirectoryInfo(Path.Combine("Pipe", "LocalDirectoryEmpty"));


        public TearDownTests()
        {
            _twoFilesInsideOneZip = new FileInfo(Path.Combine(_workingDirectory.FullName, "TwoFilesInsideOneZip.zip"));
            _oneFileInsideOneZip = new FileInfo(Path.Combine(_workingDirectory.FullName, "OneFileInsideOneZip.zip"));
            _noFileInsideOneZip = new FileInfo(Path.Combine(_workingDirectory.FullName, "NoFileInsideOneZip.zip"));

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

            FileProvider.Sftp.EmptySettings
               .DeleteWithContentIfExists()
               .CreateIfNotExists();

            FileProvider.Sftp.ToSettings
                .DeleteWithContentIfExists()
                .CreateIfNotExists();

            _testData.CopyContentTo(FileProvider.Sftp.FromSettings);


            _localDirectory
                .DeleteWithContentIfExists()
                .CreateIfNotExists();


            _localDirectoryEmpty
                .DeleteWithContentIfExists()
                .CreateIfNotExists();

            _testData.CopyContentTo(_localDirectory);
        }




        [TestCleanup]
        public void Cleanup()
        {
            _workingDirectory.DeleteWithContentIfExists();
            _localDirectory.DeleteWithContentIfExists();
            FileProvider.Sftp.FromSettings.DeleteWithContentIfExists();
            FileProvider.Sftp.EmptySettings.DeleteWithContentIfExists();
            FileProvider.Sftp.ToSettings.DeleteWithContentIfExists();
        }

        [TestMethod]
        public void SuccesfullyTearDownOneFileOnSftp()
        {
            //arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_settingsUnzipZeroFile)
                .AddConfiguration(_sharedSettings)
                .Build();
            
            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");

            //act
            fileOpsManager.Execute();

            //assert
            FileProvider.Sftp.FromSettings.CopyContentTo(_workingDirectory);

            Assert.AreEqual(5, _workingDirectory.GetFiles().Length);
            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "testFile1.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "testFile2.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "IN2.zip")));
            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "OneFileInsideOneZip.zip")));
            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "TwoFilesInsideOneZip.zip")));
        }

        [TestMethod]
        public void SuccesfullyTearDownMultipleFilesOnSftp() {
            //arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_settingsUnzipFromMultipleZips)
                .AddConfiguration(_sharedSettings)
                .Build();

            //_two zips contains setsFile1.txt, so if I dont remove it, test fails 
            FileProvider.Sftp.FromSettings.DeleteOneFile(_oneFileInsideOneZip);

            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");


            //act
            fileOpsManager.Execute();

            //assert
            FileProvider.Sftp.FromSettings.CopyContentTo(_workingDirectory);

            Assert.AreEqual(2, _workingDirectory.GetFiles().Length);
            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "testFile1.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(_workingDirectory.FullName, "testFile2.txt")));
        }

        [TestMethod]
        public void SuccesfullyTearDownAllFilesOnSftp()
        {
            //arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_settingsUnzipFromMultipleZips)
                .AddConfiguration(_sharedSettings)
                .Build();

            //_two zips contains setsFile1.txt, so if I dont remove it, test fails 
            FileProvider.Sftp.FromSettings.DeleteOneFile(_testFile1);
            FileProvider.Sftp.FromSettings.DeleteOneFile(_testFile2);
            FileProvider.Sftp.FromSettings.DeleteOneFile(_oneFileInsideOneZip);

            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");


            //act
            fileOpsManager.Execute();

            //assert
            FileProvider.Sftp.FromSettings.CopyContentTo(_workingDirectory);

            Assert.AreEqual(0, _workingDirectory.GetFiles().Length);
        }


        [TestMethod]
        public void SuccesfullyTearDownOneFileOnLocal()
        {
            //Arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_settingsLocalUnzipOneFile)
                .AddConfiguration(_sharedSettings)
                .Build();

            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");
            
            //act
            fileOpsManager.Execute();

            //assert
            Assert.AreEqual(6, _localDirectory.GetFiles().Length);
            Assert.IsTrue(File.Exists(Path.Combine(_localDirectory.FullName, "testFile1.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(_localDirectory.FullName, "testFile2.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(_localDirectory.FullName, "IN2.zip")));
            Assert.IsTrue(File.Exists(Path.Combine(_localDirectory.FullName, "NoFileInsideOneZip.zip")));
            Assert.IsTrue(File.Exists(Path.Combine(_localDirectory.FullName, "TwoFilesInsideOneZip.zip")));
        }

        [TestMethod]
        public void SuccesfullyTearDownMultipleFilesOnLocal()
        {
            //Arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_settingsLocalUnzipFromMultipleZips)
                .AddConfiguration(_sharedSettings)
                .Build();

            _localDirectory.DeleteOneFile(_oneFileInsideOneZip);

            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");

            //act
            fileOpsManager.Execute();

            //assert
            Assert.AreEqual(5, _localDirectory.GetFiles().Length);
            Assert.IsTrue(File.Exists(Path.Combine(_localDirectory.FullName, "testFile1.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(_localDirectory.FullName, "testFile2.txt")));
        }

        [TestMethod]
        public void SuccesfullyTearDownFromEmptyDirectory()
        {

            //Arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_settingsLocalUnzipEmpty)
                .AddConfiguration(_sharedSettings)
                .Build();

            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");

            //act
            fileOpsManager.Execute();

            //assert
            Assert.AreEqual(0, _localDirectoryEmpty.GetFiles().Length);
        }



        [TestMethod]
        public void TearDownWithEmptyPipe()
        {
            //arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_settingsTearDownEmptySettings)
                .AddConfiguration(_sharedSettings)
                .Build();

            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");


            //act
            fileOpsManager.Execute();

            //assert
            FileProvider.Sftp.FromSettings.CopyContentTo(_workingDirectory);

            Assert.AreEqual(6, _workingDirectory.GetFiles().Length);
        }

        [TestMethod]
        public void TearDownWithPipeWithoutFrom()
        {
            //arrange
            var steps = new FileOpsBuilder()
                .AddConfiguration(_settingsWithoutFrom)
                .AddConfiguration(_sharedSettings)
                .Build();

            IFileOpsManager fileOpsManager = new FileOps.Pipe.FileOpsManager(steps, "identifier");


            //act
            fileOpsManager.Execute();

            //assert
            FileProvider.Sftp.FromSettings.CopyContentTo(_workingDirectory);

            Assert.AreEqual(6, _workingDirectory.GetFiles().Length);
        }

    }
}
