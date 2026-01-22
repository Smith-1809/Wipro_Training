using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDesignPatterns
{
    public interface IReportMetadata
    {
        string Title { get; set; }
    }

    public interface IReportContent
    {
        string Content { get; set; }
        void Generate();
    }
}
