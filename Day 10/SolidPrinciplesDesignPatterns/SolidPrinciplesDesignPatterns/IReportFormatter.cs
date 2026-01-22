using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDesignPatterns
{
    public interface IReportFormatter
    {
        string Format(Report report);
    }

    public class PdfFormatter : IReportFormatter
    {
        public string Format(Report report) => $"--PDF--\n{report.Title}\n{report.Content}";
    }

    public class ExcelFormatter : IReportFormatter
    {
        public string Format(Report report) => $"{report.Title},{report.Content} (Excel)";
    }
}
