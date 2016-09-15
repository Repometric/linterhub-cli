namespace Metrics.Integrations.Linters.Phpmetrics
{
    using Extensions;
    using System.Collections.Generic;
    using System.IO;

    public class Lint : Linter
    {
        public override ILinterResult Parse(Stream stream)
        {
            return new LintResult
            {
                FilesList = stream.DeserializeAsJson<List<File>>()
            };
        }

        public override ILinterModel Map(ILinterResult result)
        {
            return (ILinterModel)result;
        }
    }
}