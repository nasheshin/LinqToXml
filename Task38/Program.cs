#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Task38
{
    internal static class Program
    {

        private static void BypassDocToChangeNames(XElement descendant)
        {
            if (descendant.Attribute("nameChanged")?.Value != "Yes")
            {
                descendant.Name = $"{descendant.Parent?.Name}-{descendant.Name}";
                descendant.SetAttributeValue("nameChanged", "Yes");
            }

            foreach (var newDescendant in descendant.Descendants())
                BypassDocToChangeNames(newDescendant);
        }

        private static void BypassDocToClear(XElement descendant)
        {
            descendant.Attribute("nameChanged")?.Remove();
            foreach (var newDescendant in descendant.Descendants())
                BypassDocToClear(newDescendant);
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
                BypassDocToChangeNames(descendant);
            }

            foreach (var descendant in document.Root.Descendants())
            {
                BypassDocToClear(descendant);
            }

            document.Save(outputPath);
        }

        private static void Main(string[] args)
        {
            const string? path = @"D:\HSE\Программирование\Lab6\LINQToXML\Task38\Files";

            Execute(Path.Combine(path, "Input.xml"), Path.Combine(path, "Output.xml"));
        }
    }
}