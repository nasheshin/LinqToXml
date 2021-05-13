#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Task48
{
    internal static class Program
    {

        private static void BypassDocToMarkInstructions(XElement element)
        {
            foreach (var newElement in element.Elements())
                BypassDocToMarkInstructions(newElement);

            var elementsCount = element.Elements().Count();
            if (elementsCount <= 1) return;
            
            var elemIndex = 0;
            foreach (var curElement in element.Elements())
            {
                if (elemIndex == elementsCount - 2)
                    curElement.AddAfterSelf(new XElement("has-instructions", element.Nodes().OfType<XProcessingInstruction>().Any()));
                elemIndex++;
            }
        }

        private static void Execute(string inputPath, string outputPath)
        {
            var document = XDocument.Load(inputPath);

            if (document.Root == null)
            {
                return;
            }

            BypassDocToMarkInstructions(document.Root);

            document.Save(outputPath);
        }

        private static void Main(string[] args)
        {
            const string? path = @"D:\HSE\Программирование\Lab6\LINQToXML\Task48\Files";

            Execute(Path.Combine(path, "Input.xml"), Path.Combine(path, "Output.xml"));
        }
    }
}