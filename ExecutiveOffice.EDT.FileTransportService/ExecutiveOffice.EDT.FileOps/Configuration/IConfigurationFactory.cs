using ExecutiveOffice.EDT.FileOps.Configuration.Entities;
using System.Collections.Generic;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Configuration
{
    internal interface IConfigurationFactory 
    {
        Settings Get(IEnumerable<FileInfo> file);
    }
}
