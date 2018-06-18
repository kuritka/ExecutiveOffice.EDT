using ExecutiveOffice.EDT.FileOps.Configuration.Entities;
using ExecutiveOffice.EDT.FileOps.Pipe;
using System;

namespace ExecutiveOffice.EDT.FileOps.Steps.Zip
{
    public class Zip : IStep
    {
        ZipSettings _settings;

        public Zip(ZipSettings settings)
        {
            _settings = settings;
        }

        public void Execute(IStepContext context)
        {
            throw new NotImplementedException();
        }
    }
}
