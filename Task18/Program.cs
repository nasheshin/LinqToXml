#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Task18
{
    internal static class Program
    {
        private static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var knownKeys = new HashSet<TKey>();
            return source.Where(element => knownKeys.Add(keySelector(element)));
        }
        
        private static IEnumerable<Tuple<string, string>> BypassDoc(XElement element)
        {
            var attrs = element.Attributes().Select(attr => new Tuple<string, string>(attr.Name.ToString(), attr.Value)).ToList();
            foreach(var newElement in element.Elements())
                attrs.AddRange(BypassDoc(newElement));
            return attrs;
        }

        private static void Execute(string inputPath)
        {
            var document = XDocument.Load(inputPath);

            if (document.Root == null)
            {
                return;
            }

            var result = BypassDoc(document.Root).DistinctBy(pair => pair.Item1).ToList();
            var attrNames = result.Select(item => item.Item1).OrderBy(i => i).Distinct().ToList();
            var attrValues = result.Select(item => item.Item2).OrderBy(i => i).ToList();

            foreach (var attrName in attrNames)
            {
                Console.Write(attrName + " ");
            }

            Console.WriteLine();
            foreach (var attrName in attrValues)
            {
                Console.Write(attrName + " ");
            }
        }

        private static void Main(string[] args)
        {
            const string? path = @"D:\HSE\Программирование\Lab6\LINQToXML\Task18\Files";

            Execute(Path.Combine(path, "Input.xml"));
        }
    }
}