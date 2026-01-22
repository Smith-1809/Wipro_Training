using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SolidPrinciplesDesignPatterns
{
    public interface IReportSaver
    {
        void Save(string filename, string content);
    }

    public class DiskSaver : IReportSaver
    {
        public void Save(string filename, string content) => File.WriteAllText(filename, content);
    }
}
