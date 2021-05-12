#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Task88
{
    internal sealed class Mark
    {
        public Mark(string? @class, string? name, string? subject, string? value)
        {
            Class = @class;
            Name = name;
            Subject = subject;
            Value = value;
        }
        
        public string? Class { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? Value { get; set; }
        
    }
    
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
            
            var classes = document.Root?.Elements("class");

            if (classes == null)
                return;

            var allMarks = new List<Mark>();
            foreach (var @class in classes)
            {
                var curMarks = @class.Elements("pupil").Select(pupil => new Mark(
                    @class.Attribute("number")?.Value, pupil.Attribute("name")?.Value, pupil.Attribute("subject")?.Value,
                    pupil.Attribute("mark")?.Value));
                allMarks.AddRange(curMarks);
            }

            var allMarksBySubject = allMarks.GroupBy(mark => mark.Subject).OrderBy(x => x.Key);
            foreach (var groupSubject in allMarksBySubject)
            {
                var trackSubject = new XElement("subject",
                    new XAttribute("name", groupSubject.Key));
                
                var marksByPupil = groupSubject
                    .GroupBy(mark => new Tuple<string, string>(mark.Class, mark.Name))
                    .OrderBy(x => x.Key.Item1)
                    .ThenBy(x => x.Key.Item2);
                foreach (var groupPupil in marksByPupil)
                {
                    if (groupPupil.Count() < 2)
                        continue;
                    
                    var trackPupil = new XElement("pupil",
                        new XAttribute("class", groupPupil.Key.Item1),
                        new XAttribute("name", groupPupil.Key.Item2));

                    var markValues = groupPupil.Select(mark => mark.Value).OrderByDescending(x => x);
                    var markNumber = 1;
                    foreach (var markValue in markValues)
                    {
                        trackPupil.Add(new XAttribute($"m{markNumber}", markValue));
                        markNumber++;
                    }
                    
                    trackSubject.Add(trackPupil);
                }
                
                outputDocument.Root?.Add(trackSubject);
            }

            outputDocument.Save(outputPath);
        }

        private static void Main(string[] args)
        {
            const string? path = @"D:\HSE\Программирование\Lab6\LINQToXML\Task88\Files";

            Execute(Path.Combine(path, "Input.xml"), Path.Combine(path, "Output.xml"));
        }
    }
}