namespace Metrics.Integrations.Linters.coffeelint
{
    using System.Collections.Generic;

    public class LintResult : ILinterResult, ILinterModel
    {
        /// <summary>
        /// List of Warnings, generated by coffeelint
        /// </summary>
        public List<Warning> Records = new List<Warning>();
    }
}
