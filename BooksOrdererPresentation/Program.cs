using BooksOrdererApp;
using BooksOrdererDomain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BooksOrdererPresentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _bookService = new SortBookService();
            Dictionary<string, string> selected;
            List<Book> result = new List<Book>();

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .Build();

            var booksSection = config.GetSection("Books").Get<Book[]>();
            var sorting = config.GetSection("Sorting").Get<SortingOption[]>();

            Console.WriteLine("Coleção de livros");

            foreach (var book in booksSection)
            {
                Console.WriteLine($"{book.Id} {book.Title} - Author: {book.Author} - Edition: {book.Edition}");
            }

            Console.WriteLine();

            var showMenu = true;
            while (showMenu)
            {
                Console.WriteLine("Digite o número da respectiva ordenação que deseja e aperte enter.");
                var index = 1;
                foreach (var item in sorting)
                {
                    Console.WriteLine($" {index++} - {item.Texto}");
                }

                Console.WriteLine();
                var option = Console.ReadLine();
                if (option.ToUpper() == "S")
                {
                    showMenu = false;
                }


                try
                {
                    selected = config.GetSection($"SortOptions:Option{option}").Get<Dictionary<string, string>>();
                    result = _bookService.OrderBooks(selected);

                    //resultado
                    Console.WriteLine($"Resultado ordenado por {sorting[int.Parse(option) - 1].Texto}:");
                    Console.WriteLine();
                    foreach (var item in result)
                    {
                        Console.WriteLine(item.Title);
                    }
                    Console.WriteLine();
                    Console.WriteLine("...aperte enter");
                    Console.ReadKey();
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public class SortingOption
        {
            public string Texto { get; set; }
        }
    }
}
