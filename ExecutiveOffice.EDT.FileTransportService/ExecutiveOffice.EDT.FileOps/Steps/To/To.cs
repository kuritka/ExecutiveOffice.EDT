﻿using ExecutiveOffice.EDT.FileOps.Configuration.Entities;
using ExecutiveOffice.EDT.FileOps.Pipe;
using ExecutiveOffice.EDT.FileOps.Processors.Channels;
using System;
using System.IO;

namespace ExecutiveOffice.EDT.FileOps.Steps.To
{
    public class To : IStep
    {
        private readonly ToSettings _settings;

        public To(ToSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute(IStepContext context)
        {
            DirectoryInfo flushDirectory = context.WorkingDirectory.CreateSubdirectory("Flush");

            context.Attach(context.Parent.Files);

            //TODO: fix To channel + channel factory to be able to accept IEnumerable<FileInfo> instead of DirectoryInfo
            foreach (var file in context.Parent.Files)
            {
                file.CopyTo(Path.Combine(flushDirectory.FullName,file.Name));
            }

            IChannel channel = ChannelFactory.Create(_settings, flushDirectory);
         
            channel.Copy();
        }
    }
}
