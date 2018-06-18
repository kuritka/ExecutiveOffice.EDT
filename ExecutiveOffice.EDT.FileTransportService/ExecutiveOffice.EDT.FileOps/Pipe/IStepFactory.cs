using ExecutiveOffice.EDT.FileOps.Configuration.Entities;
using System.Collections.Generic;

namespace ExecutiveOffice.EDT.FileOps.Pipe
{
    internal interface IStepFactory
    {
        IEnumerable<IStep> Get(Settings settings);
    }
}
