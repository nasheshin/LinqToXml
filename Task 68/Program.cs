#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Task_68
{
    internal static class Program
    {
        private static void Execute(string inputPath, string outputPath)
        {
            var document = XDocument.Load(inputPath);

            if (document.Root == null)
            {
                return;
            }
            
            var outputDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("root"));
            
            var records = document.Root?.Elements("record");
            foreach (var record in records)
            {
                var elementCompany = record.Elements("company").ToList()[0];
                var elementStreet = record.Elements("street").ToList()[0];
                var elementBrand = record.Elements("brand").ToList()[0];
                var elementPrice = record.Elements("price").ToList()[0];

                var newTrack = new XElement("station",
                    new XElement("info",
                        new XElement("brand", elementBrand.Value),
                        new XElement("price", elementPrice.Value)));
                
                newTrack.SetAttributeValue("company", elementCompany.Value);
                newTrack.SetAttributeValue("street", elementStreet.Value);
                outputDocument.Root?.Add(newTrack);
            }

            outputDocument.Save(outputPath);
        }

        private static void Main(string[] args)
        {
            const string? path = @"D:\HSE\Программирование\Lab6\LINQToXML\Task 68\Files";

            Execute(Path.Combine(path, "Input.xml"), Path.Combine(path, "Output.xml"));
        }
    }
}