using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace WLXMLDiff
{
    class Program
    {
        private static long totalWanted = 0;
        private static long totalHaving = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to WL.XML Diff.");
            Console.WriteLine("You will be asked to enter/paste two WL.XML filenames.");
            Console.WriteLine("One with the parts you WANT, One with the parts you already HAVE.");
            Console.WriteLine("The result will be a new WL.XML of the parts you are missing.");
            Console.WriteLine("For instance, if you want to build Brickvaults MF, reusing parts of the UCS MF, you enter Brickvaults XML first, followed by the UCS MF XML");
            Console.WriteLine();

            Console.Write("Enter Filename 1 (Parts you want):");
            var path1 = Console.ReadLine();
            var itemsWanted = ReadWantedList(path1);

            Console.Write("Enter Filename 2 (Parts you have):");
            var path2 = Console.ReadLine();
            var itemsHaving = ReadWantedList(path2);

            totalWanted = itemsWanted.Sum(i => i.MinQty);
            totalHaving = itemsHaving.Sum(i => i.MinQty);

            var remainingWantedList = DiffWantedlists(itemsWanted, itemsHaving);

            SaveWantedlist(remainingWantedList);

            var totalRemaining = remainingWantedList.Sum(i => i.MinQty);
            var totalReusing = totalWanted - totalRemaining;

            Console.WriteLine("{0} parts wanted in total, in file 1", totalWanted);
            Console.WriteLine("{0} parts can be reused from file 2", totalReusing);
            Console.WriteLine("{0} parts wanted after reuse", totalRemaining);

            Console.Read();
        }

        private static void SaveWantedlist(List<Item> remainingWantedList)
        {
            var serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("INVENTORY"));
            var stream = File.Create("Remaining WL.XML");
            serializer.Serialize(stream, remainingWantedList);
            stream.Close();
        }

        private static List<Item> DiffWantedlists(List<Item> itemsWanted, List<Item> itemsHaving)
        {
            return itemsWanted.Select(item => SubtractItemsFrom(itemsHaving, item)).Where(item => item != null).ToList();
        }

        private static Item SubtractItemsFrom(List<Item> itemsHaving, Item itemWanted)
        {
            var itemHaving = itemsHaving.FirstOrDefault(i => i.ItemId == itemWanted.ItemId && i.Color == itemWanted.Color);
            if (itemHaving == null)
            {
                return itemWanted;
            }

            if (itemHaving.MinQty < itemWanted.MinQty)
            {
                itemWanted.MinQty = itemWanted.MinQty - itemHaving.MinQty;
                return itemWanted;
            }

            return null;
        }

        private static List<Item> ReadWantedList(string path)
        {
            path = path.Trim('"');
            var serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("INVENTORY"));
            var stream = File.OpenRead(path);
            return serializer.Deserialize(stream) as List<Item>;
        }
    }
}
