using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program 
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Michael's Grade Book");
            book.GradeAdded += OnGradeAdded; // Event handler can do this multiple times.

            EnterGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The Letter grade is {stats.Letter}");
        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try // If exception try this
                {
                    var grade = double.Parse(input); // If exception throw miss below grade.
                    if(grade <= 100 && grade >= 0)
                    {
                        book.AddGrade(grade); // Behaviour is polymorphic depending on the type of object working with InMemoryBook || Book
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid {nameof(grade)}"); // throws ArgumentException if bool is not met.
                    }
                    
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message); // Catch exception of ArgumentException and log in console.
                }
                catch (FormatException ex) // Catch exception of FormatException and log in console.
                {
                    Console.WriteLine(ex.Message);
                }
                finally // Clean up the try block
                {
                    Console.WriteLine("**");
                }

            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added!");
        }
    }
}