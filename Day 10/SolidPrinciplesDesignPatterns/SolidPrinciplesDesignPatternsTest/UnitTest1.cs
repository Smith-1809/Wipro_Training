using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidPrinciplesDesignPatterns;
using System;

namespace SolidPrinciplesDesignPatternsTest
{
    [TestClass]
    public class AssignmentTests
    {
        [TestMethod]
        public void Factory_ShouldCreatePdfInstance()
        {
            var doc = DocumentFactory.Create("pdf");
            Assert.IsInstanceOfType(doc, typeof(PdfDoc));
        }

        [TestMethod]
        public void Singleton_ShouldAlwaysReturnSameInstance()
        {
            var log1 = Logger.Instance;
            var log2 = Logger.Instance;
            Assert.AreSame(log1, log2);
        }

        [TestMethod]
        public void Solid_ReportService_ShouldExecuteSuccessfully()
        {
            // Arrange
            var report = new SalesReport { Title = "Test Report" };
            var formatter = new ExcelFormatter();
            var saver = new DiskSaver();
            var service = new ReportService(formatter, saver);

            // Act & Assert
            service.ExportReport(report, "test.txt");
        }
    }
}
