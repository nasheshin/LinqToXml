#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Task8
{
    internal static class Program
    {
        private static void Execute(string filePath, string toCreatePath)
        {
            var reader = new StreamReader(filePath);
            
            var lines = new List<string>();
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            var wordsByLines = lines.Select(curLine => new List<string>(curLine.Split(' '))).ToList();
            foreach (var curLine in wordsByLines)
            {
                curLine.Sort();
            }

            var documentXml = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("root"));
            
            foreach (var curLine in wordsByLines)
            {
                var track = new XElement("line");
                foreach (var word in curLine)
                {
                    var trackWord = new XElement("word");
                    foreach (var character in word)
                    {
                        trackWord.Add(new XElement("char", character));
                    }
                    track.Add(trackWord);
                }
                documentXml.Root?.Add(track);
            }
            
            documentXml.Save(toCreatePath);
        }
        
        private static void Main(string[] args)
        {
            const string? pathTask8 = @"D:\HSE\Программирование\Lab6\LINQToXML\Task8\FilesTask8";

            Execute(Path.Combine(pathTask8, "File.txt"), Path.Combine(pathTask8, "Output.xml"));
        }
    }
}