using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciplesDesignPatterns
{
    public interface IDocument
    {
        string GetInfo();
    }

    public class PdfDoc : IDocument
    {
        public string GetInfo() => "PDF Document created.";
    }

    public class WordDoc : IDocument
    {
        public string GetInfo() => "Word Document created.";
    }

    public static class DocumentFactory
    {
        public static IDocument Create(string type)
        {
            switch (type.ToLower())
            {
                case "pdf":
                    return new PdfDoc();
                case "word":
                    return new WordDoc();
                default:
                    throw new ArgumentException("Invalid Type");
            }
        }
    }
}
