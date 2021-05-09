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

        private static void BypassDocToMarkInstructions(XElement descendant)
        {
            Console.WriteLine(descendant.NextNode.NodeType);

            foreach (var newDescendant in descendant.Descendants())
                BypassDocToMarkInstructions(newDescendant);
        }

        private static void Execute(string inputPath, string outputPath)
        {
            var document = XDocument.Load(inputPath);

            if (document.Root == null)
            {
                return;
            }

            foreach (var descendant in document.Root.Descendants())
            {
                BypassDocToMarkInstructions(descendant);
            }

            //document.Save(outputPath);
        }

        private static void Main(string[] args)
        {
            const string? path = @"D:\HSE\Программирование\Lab6\LINQToXML\Task48\Files";

            Execute(Path.Combine(path, "Input.xml"), Path.Combine(path, "Output.xml"));
        }
    }
}