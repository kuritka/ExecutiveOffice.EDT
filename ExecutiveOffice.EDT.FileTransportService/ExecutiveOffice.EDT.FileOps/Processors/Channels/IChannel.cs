using System.Collections.Generic;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Processors.Channels
{
    internal interface IChannel
    {
        IEnumerable<FileInfo> Copy();

        void Delete(IEnumerable<FileInfo> targetFiles);
    }
}