using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDesignPatterns
{
    public abstract class Report : IReportMetadata, IReportContent
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public abstract void Generate();
    }

    public class SalesReport : Report
    {
        public override void Generate() => Content = "Sales Total: $10,000";
    }
}
