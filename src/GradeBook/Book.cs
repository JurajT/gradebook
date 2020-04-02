using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    //should be in its dedicated file
    public class NamedObject
    {
        //constructor
        public NamedObject(string name)
        {
            //Every Object that inherits from NamedObject have to provide name in constructor
            Name = name;
        }
        //property
        public string Name
        {
            get;
            set;
        }
    }
    // more common than abstract class
    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
    }
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
            Name = name;
        }

        public override void AddGrade(double grade)
        {
            using(var gradeFile = File.AppendText($"{this.Name}.txt"))
            {
                gradeFile.WriteLine(grade.ToString());
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }

        }
          
        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            // var gradeFile = File.OpenText($"{Name}.txt");
            // string line = "";
            // while((line = gradeFile.ReadLine()) != null)
            // {
            //     result.Add(double.Parse(line));
            // }
            // gradeFile.Close();
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(double.Parse(line));
                }
            }
            return result;
        }

        public override event GradeAddedDelegate GradeAdded;

    }
    public class InMemoryBook : Book
    {
        //constructor
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            //on this object I want to set field parameter
            Name = name;
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
        public override void AddGrade(double grade)
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
        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            foreach(var grade in grades)
            {
                result.Add(grade);
            }
            return result;
        }
        //field
        private List<double> grades;        
    }
}