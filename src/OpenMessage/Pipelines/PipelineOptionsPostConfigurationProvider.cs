﻿using System;
using Microsoft.Extensions.Options;

namespace OpenMessage.Pipelines
{
    internal sealed class PipelineOptionsPostConfigurationProvider<T> : IPostConfigureOptions<PipelineOptions<T>>
    {
        public void PostConfigure(string name, PipelineOptions<T> options)
        {
            if (options.PipelineTimeout == default)
                options.PipelineTimeout = TimeSpan.FromSeconds(5);

            if (!options.AutoAcknowledge.HasValue)
                options.AutoAcknowledge = true;
        }
    }
}