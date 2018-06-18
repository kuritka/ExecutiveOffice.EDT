using System;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Pipe
{
    public interface IAggregate
    {
        void ExecuteStep(IStep step);

        Guid Guid { get;}

        DirectoryInfo WorkingDirectory { get; }

        IStepContext Current { get; }
    }
}
