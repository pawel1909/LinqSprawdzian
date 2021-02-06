using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace LinqSprawdzian
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 2014;

            XDocument document = XDocument.Load("./../../../linq.xml");

            //Console.WriteLine(document);

            XElement root = document.Root;


            var Query =
                from item in document.Descendants("Customer")
                select item;

            var Query2 =
                from item in document.Descendants("Order")
                select item;
            /*
             * Śpięszę z wyjaśnieniem jakbyś zapomniał co jakiś czas temu zrobiłeś ewentualnie, żeby oszczędzić czas, bo pewnie po jakimś czasie i tak byś wpadł xD. 
             * W Query 3 tworzysz nowe Query(??jeszcze nie do końca rozumiem??) z pliku xml,
             * z którego później wyciągasz elementy .Element po nazwie z pliku xml. problem pojawił się przy dacie zamówienia,
             * ale i to się udało, bo wystarczyło poraz kolejny wyciągnąć query(??dalej nie wiem??) i później z niego wyciągać elementy(okazuje się, że może być tego więcej niż jeden :O).
             * a później klasycznie wyciągasz te wartości jak ci się podoba. swoją drogą pamiętaj o tym https://www.codingame.com/playgrounds/213/using-c-linq---a-practical-overview/distinct-intersect-and-where
             * */
            var Query3 = document.Descendants("Customer")
                .Select(o => (name: o.Element("CompanyName"), country: o.Element("Country"), city: o.Element("City"), date: o.Descendants("Order").Elements("OrderDate")))
                .OrderBy(x => x.country.Value)
                .ThenBy(x => x.city.Value)
                .ThenBy(x => x.name.Value);


            List<string> company = new List<string>();
            foreach (var item in Query3)
            {
                List<int> date = new List<int>();
                foreach (var item2 in item.date)
                {
                    date.Add(Convert.ToInt32(Convert.ToDateTime(item2.Value).Year));
                }

                if (date.Contains(x))
                    company.Add(item.name.Value);

            }

            if (company.Count == 0)
            {
                Console.WriteLine("empty");
            }
            else
            {
                foreach (var item in company)
                {
                    Console.WriteLine(item);
                }
            }

            //foreach (var item in Query)
            //{
            //    string name = item.Element("CompanyName").Value;
            //    string date = item.Element("Orders")?.Value;
            //    Console.WriteLine($"{name}: {date}");
            //}

            //foreach (var item in Query2)
            //{
            //    DateTime date = Convert.ToDateTime(item.Element("OrderDate")?.Value);
            //    Console.WriteLine(date.Year);
            //}

            //foreach (var item in document.Descendants("Customer"))
            //{
            //    string name = item.Element("CompanyName").Value;
            //    Console.WriteLine(name);
            //    foreach (var item2 in document.Descendants("Order"))
            //    {
            //        DateTime date = Convert.ToDateTime(item2.Element("OrderDate")?.Value);
            //        string name2 = item2.Element("CompanyName")?.Value;
            //        Console.WriteLine(name2);
            //        Console.WriteLine (date.Year);
            //    }
            //}
        }

    }
}
