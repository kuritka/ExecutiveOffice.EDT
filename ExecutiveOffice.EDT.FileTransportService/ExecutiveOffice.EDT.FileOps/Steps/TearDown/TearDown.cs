using ExecutiveOffice.EDT.FileOps.Common;
using ExecutiveOffice.EDT.FileOps.Pipe;
using ExecutiveOffice.EDT.FileOps.Processors.Channels;
using System;

namespace ExecutiveOffice.EDT.FileOps.Steps.TearDown
{
    internal class TearDown : IStep
    {
        public void Execute(IStepContext context)
        {
            if (context == null) throw new ArgumentNullException($"{nameof(context)}");

            var ctx = context.Parent;

            while (ctx.ProcessingStep.GetType().FullName != typeof(ExecutiveOffice.EDT.FileOps.Steps.From.From).FullName  )
            {
                if (ctx.Parent == null) return; // throw new ArgumentOutOfRangeException($"Missing step {nameof(From.From)}");
                ctx = ctx.Parent;
            }

            IChannel channel =  ChannelFactory.Create(ctx.FromSettings, ctx.WorkingDirectory);
            try
            {
                channel.Delete(ctx.Files);

                context.WorkingDirectory.DeleteWithContentIfExists();

            }catch(Exception ex)
            {
                throw new ApplicationException("Exception occured during TearDown Delete",ex);
            }
        }

    }
}
