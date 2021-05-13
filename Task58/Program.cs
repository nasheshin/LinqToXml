#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Task58
{
    internal static class Program
    {
        private static void Execute(string inputPath, string outputPath, string s)
        {
            var document = XDocument.Load(inputPath);

            if (document.Root == null)
                return;

            XNamespace node = s;
            document.Root.SetAttributeValue(XNamespace.Xmlns + "node", node);

            foreach (var element in document.Root.Elements())
            {
                element.SetAttributeValue(node + "count", element.Nodes().Count());
                element.SetAttributeValue(XNamespace.Xml + "count", element.Elements().Count());
            }

            document.Save(outputPath);
        }

        private static void Main(string[] args)
        {
            const string path = @"D:\HSE\Программирование\Lab6\LINQToXML\Task58\Files";

            Console.WriteLine("Write S:");
            var s = Console.ReadLine();
            
            Execute(Path.Combine(path, "Input.xml"), Path.Combine(path, "Output.xml"), s);
        }
    }
}