using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    public class Book
    {
        //constructor
        public Book(string name)
        {
            grades = new List<double>();
            //on this object I want to set field parameter
            Name = name;
        }

        public object GetNumberOfGrades()
        {
            return grades.Count;
        }

        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;
                
                case 'D':
                    AddGrade(60);
                    break;

                case 'E':
                    AddGrade(50);
                    break;
                
                case 'F':
                    AddGrade(40);
                    break;
                
                default:
                    AddGrade(0);
                    break;


            }
        }
        public void AddGrade(double grade)
        {
            if(grade <= 100 && grade >=0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                // Console.WriteLine("Invalid value");
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
            
        }
        //event
        public event GradeAddedDelegate GradeAdded;

        public Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;
            
            //foreach(double grade in grades)
            
            for(var index = 0; index < grades.Count; index++)
            {
                result.High = Math.Max(grades[index], result.High);
                result.Low = Math.Min(grades[index], result.Low);
                result.Average += grades[index];
                
            }

            result.Average /= grades.Count;

            switch(result.Average)
            {
                case var d when d > 90.0:
                    result.Letter = 'A';
                    break;

                case var d when d > 80.0:
                    result.Letter = 'B';
                    break;

                case var d when d > 70.0:
                    result.Letter = 'C';
                    break;
                
                case var d when d > 60.0:
                    result.Letter = 'D';
                    break;
                
                default:
                    result.Letter = 'F';
                    break;
            }
            
            return result;
        }
        //field
        private List<double> grades;

        // public string Name
        // {
        //     get
        //     {
        //         return name;
        //     }
        //     set
        //     {
        //         if (!string.IsNullOrEmpty(value))
        //         {
        //             name = value;
        //         }
        //     }
        // }
        // private string name;
        public string Name
        {
            get;
            set;
        }
    }
}