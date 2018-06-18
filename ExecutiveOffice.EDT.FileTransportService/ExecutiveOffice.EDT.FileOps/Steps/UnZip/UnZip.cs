using ExecutiveOffice.EDT.FileOps.Configuration.Entities;
using ExecutiveOffice.EDT.FileOps.Pipe;
using System;

namespace ExecutiveOffice.EDT.FileOps.Steps.UnZip
{
    public class UnZip : IStep
    {
        ZipSettings _settings;

        public UnZip(ZipSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute(IStepContext context)
        {
            var compressor = new Processors.Compression.ZipFactory(_settings).Get();

            foreach (var file in context.PreviousFiles)
            {
                var uncompressedFiles =  compressor.Decompress(file, context.WorkingDirectory);

                context.Attach(uncompressedFiles);
            }
        }
    }
}
