using System;
using System.Collections.Generic;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Pipe
{
    public interface IStepContext 
    {
        IEnumerable<FileInfo> Files { get; }

        IEnumerable<FileInfo> PreviousFiles { get; }

        IStep ProcessingStep { get; }

        void Attach(FileInfo file);

        void Attach(IEnumerable<FileInfo> files);

        DirectoryInfo WorkingDirectory { get; }

        Guid Guid { get; }
    }
}
