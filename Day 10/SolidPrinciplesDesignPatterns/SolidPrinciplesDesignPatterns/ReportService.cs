using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDesignPatterns
{
    public class ReportService
    {
        private readonly IReportFormatter _formatter;
        private readonly IReportSaver _saver;

        // Constructor Injection
        public ReportService(IReportFormatter formatter, IReportSaver saver)
        {
            _formatter = formatter;
            _saver = saver;
        }

        public void ExportReport(Report report, string filename)
        {
            report.Generate();
            string formattedData = _formatter.Format(report);
            _saver.Save(filename, formattedData);
        }
    }
}
