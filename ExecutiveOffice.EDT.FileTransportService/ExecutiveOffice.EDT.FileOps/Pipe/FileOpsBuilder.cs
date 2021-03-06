﻿using System;
using System.Collections.Generic;
using ExecutiveOffice.EDT.FileOps.Pipe;
using System.IO;
using ExecutiveOffice.EDT.FileOps.Common;
using ExecutiveOffice.EDT.FileOps.Configuration;

namespace ExecutiveOffice.EDT.FileOps
{
    public class FileOpsBuilder : IFileOpsBuilder
    {
        private Dictionary<string, FileInfo> _configFiles;

        public FileOpsBuilder()
        {
            _configFiles = new Dictionary<string, FileInfo>();
        }

        public IFileOpsBuilder AddConfiguration(FileInfo jsonFile)
        {
            jsonFile
                .ThrowExceptionIfNullOrDoesntExists()
                .ThrowExceptionIfExtensionIsDifferentFrom(Constants.FileExtensions.Json)
                .ThrowExceptionIfFileSizeExceedsMB(1);
            try
            {
                _configFiles.Add(jsonFile.Name, jsonFile);

            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{jsonFile.Name} is already added.", ex);
            }
            return this;
        }


        public LinkedList<IStep> Build()
        {
            IStepFactory stepFactory = new StepFactory();

            var settings = new ConfigurationFactory().Get(_configFiles.Values);

            var steps = stepFactory.Get(settings);
            
            return new LinkedList<IStep>(steps);
        }
    }
}
