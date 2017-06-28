﻿using Linterhub.Engine.Extensions;
using Linterhub.Engine.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Linterhub.Engine.Runtime
{
    public class LinterRunner
    {
        private LinterWrapper linterRunner;

        public LinterRunner(LinterWrapper wrapper)
        {
            linterRunner = wrapper;
        }

        public List<EngineOutputSchema.ResultType> RunAnalyze(List<LinterWrapper.Context> contexts)
        {
            var results = new List<EngineOutputSchema.ResultType>();
            var n_contexts = new List<LinterWrapper.Context>();

            contexts.ForEach(context =>
            {
                if (context.Stdin == LinterWrapper.Context.stdinType.NotUse &&
                    context.RunOptions.Where((x) => x.Key == "{path}" && x.Value == context.Specification.Schema.Defaults.GetValueOrDefault("")).Count() != 0 &&
                    context.Specification.Schema.AcceptMask == false)
                {
                    context.Specification.Schema.Extensions.Select(x =>
                    {
                        return Directory.GetFiles(context.WorkingDirectory, x, System.IO.SearchOption.AllDirectories).ToList();
                    }).SelectMany(x => x).ToList()
                    .ForEach(x => {
                        var lo = new LinterOptions();
                        context.RunOptions.Select(y => {
                            if (y.Key == "{path}")
                            {
                                return new KeyValuePair<string, string>(y.Key, Path.GetFullPath(x)
                                    .Replace(Path.GetFullPath(context.WorkingDirectory), string.Empty)
                                    .TrimStart('/')
                                    .TrimStart('\\'));
                            }
                            return y;
                        }).ToList().ForEach(z => lo.Add(z.Key, z.Value));

                        n_contexts.Add(new LinterWrapper.Context()
                        {
                            ConfigOptions = context.ConfigOptions,
                            Stdin = context.Stdin,
                            WorkingDirectory = context.WorkingDirectory,
                            Specification = context.Specification,
                            RunOptions = lo
                        });
                    });

                }
                else
                {
                    n_contexts.Add(context);
                }
            });

            Parallel.ForEach(n_contexts, context =>
            {

                var res = linterRunner.RunAnalysis(context);
                var current = res.DeserializeAsJson<EngineOutputSchema.ResultType[]>();
                lock (results)
                {
                    foreach (var output in current)
                    {
                        var req = results.Where(x => x.Path == output.Path);
                        if (req.Count() > 0)
                        {
                            req.First().Messages.AddRange(output.Messages);
                        }
                        else if (output.Path != string.Empty)
                        {
                            results.Add(output);
                        }
                    }
                }

            });

            return results;

        }
    }
}