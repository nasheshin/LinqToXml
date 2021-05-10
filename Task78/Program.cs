#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Task78
{
    internal static class Program
    {
        private static void Execute(string inputPath, string outputPath)
        {
            var document = XDocument.Load(inputPath);

            if (document.Root == null)
                return;

            var outputDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("root"));
            
            var elements = document.Root?.Elements("debt");

            if (elements == null)
                return;
            
            var elementsByHouse = elements.GroupBy(elem => elem.Attribute("house")?.Value);
            foreach (var groupHouse in elementsByHouse)
            {
                var trackHouse = new XElement("house",
                    new XAttribute("number", groupHouse.Key));


                var elementsByEntrance = groupHouse.GroupBy(elem => elem.Attribute("entrance")?.Value);
                foreach (var groupEntrance in elementsByEntrance)
                {
                    var trackEntrance = new XElement("entrance",
                        new XAttribute("number", groupEntrance.Key));

                    foreach (var element in groupEntrance)
                    {
                        var newElement = new XElement("debt", element.Element("value")?.Value);
                        newElement.SetAttributeValue("name", element.Element("name")?.Value);
                        newElement.SetAttributeValue("flat", element.Attribute("flat")?.Value);
                        
                        trackEntrance.Add(newElement);
                    }
                    
                    trackHouse.Add(trackEntrance);
                }
                
                outputDocument.Root?.Add(trackHouse);
            }

            outputDocument.Save(outputPath);
        }

        private static void Main(string[] args)
        {
            const string? path = @"D:\HSE\Программирование\Lab6\LINQToXML\Task78\Files";

            Execute(Path.Combine(path, "Input.xml"), Path.Combine(path, "Output.xml"));
        }
    }
}