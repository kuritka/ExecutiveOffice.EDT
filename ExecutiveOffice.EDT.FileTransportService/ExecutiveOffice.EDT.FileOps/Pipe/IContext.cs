using ExecutiveOffice.EDT.FileOps.Configuration.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Pipe
{
    public interface IStepContext 
    {
        IEnumerable<FileInfo> Files { get; }

        IStep ProcessingStep { get; }

        void Attach(FileInfo file);

        void Attach(IEnumerable<FileInfo> files);

        void Attach(FromSettings fromSettings);

        DirectoryInfo WorkingDirectory { get; }

        Guid Guid { get; }

        IStepContext Parent { get; }

        FromSettings FromSettings { get; }
    }
}
