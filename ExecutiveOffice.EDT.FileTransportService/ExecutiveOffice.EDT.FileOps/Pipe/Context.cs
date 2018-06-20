using ExecutiveOffice.EDT.FileOps.Common;
using System;
using System.Collections.Generic;
using System.IO;
using ExecutiveOffice.EDT.FileOps.Configuration.Entities;

namespace ExecutiveOffice.EDT.FileOps.Pipe
{
    public class StepContext : IStepContext
    {
        private readonly List<FileInfo> _files = new List<FileInfo>();

        private readonly Guid _stepGuid = Guid.NewGuid();

        private readonly DateTime _processingDate = DateTime.UtcNow;

        private readonly Guid _guid;

        private readonly IStep _processingStep;

        private readonly DirectoryInfo _workingDirectory;

        private readonly IStepContext _parent;
        private FromSettings _fromSettings;

        public StepContext(IStepContext parentStepContext, IStep processingStep) : this(processingStep, parentStepContext.Guid, parentStepContext.WorkingDirectory, parentStepContext.FromSettings)
        {
            _parent = parentStepContext ?? throw new ArgumentNullException($"{parentStepContext}");
        }


        public StepContext(IStep processingStep, Guid guid, DirectoryInfo workingDirectory, FromSettings fromSettings = null)
        {
            _fromSettings = fromSettings;

            _workingDirectory = workingDirectory;

            _guid = guid;

            _processingStep = processingStep ?? throw new ArgumentNullException($"{processingStep}");
        }

        public void Attach(FileInfo file)
        {
            file.ThrowExceptionIfNullOrDoesntExists()
                .ThrowExceptionIfFileSizeExceedsMB(Constants.OneGB);

            _files.Add(file);
        }

        public void Attach(IEnumerable<FileInfo> files)
        {
            files.ThrowExceptionIfNull();

            foreach (var file in files)
            {
                Attach(file);
            }
        }

        public void Attach(FromSettings fromSettings)
        {
            _fromSettings = fromSettings ?? throw new ArgumentNullException($"{nameof(FromSettings)}");
        }

        public IEnumerable<FileInfo> Files => _files;

        public IStep ProcessingStep => _processingStep;

        public DirectoryInfo WorkingDirectory => _workingDirectory;

        public Guid Guid => _guid;

        public IStepContext Parent => _parent;

        public FromSettings FromSettings => _fromSettings;
    }
}
