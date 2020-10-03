using Collection;
using System;
namespace App
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CircularLinkedList<string> circularList = new CircularLinkedList<string>();
            circularList.Added += (object sender, AddToCollection<string> argument) => { Console.WriteLine(argument.Message); };
            circularList.Removed += (object sender, RemFromCollection<string> argument) => { Console.WriteLine(argument.Message); };

            circularList.Add("1");
            circularList.Add("2");
            circularList.Add("3");
            circularList.Add("4");
            foreach (var item in circularList)
            {
                Console.WriteLine(item);
            }
 
            circularList.Remove("2");
            Console.WriteLine("\n После удаления: \n");
            foreach (var item in circularList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n Содержит 3: \n");
            bool b =circularList.Contains("3");
            Console.WriteLine(b);
            
            
        }
    }
}