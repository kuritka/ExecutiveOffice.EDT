using ExecutiveOffice.EDT.FileOps.Pipe;
using System.Collections.Generic;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps
{
    public interface IFileOpsBuilder
    {
        LinkedList<IStep> Build();

        IFileOpsBuilder AddConfiguration(FileInfo jsonFile);
    }
}
