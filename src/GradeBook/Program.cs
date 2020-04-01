using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            var book = new Book("Duri's Grade Book");
            //subscribe to event
            book.GradeAdded += OnGradeAdded;
            book.GradeAdded += OnGradeAdded;
            
            while(true){
                Console.WriteLine("Enter grade or 'q' for quit");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
                
            };

            

            var stats = book.GetStatistics();
            
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The maximum grade is {stats.High}");
            Console.WriteLine($"The minimum grade is {stats.Low}");
            Console.WriteLine($"The letter grade is {stats.Letter}");

        }
        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("Grade was added");
        }
    }

    
}
