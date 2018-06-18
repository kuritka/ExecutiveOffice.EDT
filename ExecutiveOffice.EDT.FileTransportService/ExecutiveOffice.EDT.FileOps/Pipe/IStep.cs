using System.Collections.Generic;

namespace ExecutiveOffice.EDT.FileOps.Pipe
{
    public interface IStep
    {
        void Execute(IStepContext stepContext);        
    }
}
