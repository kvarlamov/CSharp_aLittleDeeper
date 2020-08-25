using System;
using System.Linq;
using System.Collections.Generic;
using p04_Linq;

public class Test
{
    class Student
    {
        public string Name{get;set;}
        public string TeacherName{get;set;}
        public int MaxMark{get;set;}
    }
 
    class Teacher
    {
        public string Name{get;set;}
        public int Experience{get;set;}
    }
 
    public static void Main()
    {
        //LinqPractice();
        AtmSimulator.Start();
    }

    private static void LinqPractice()
    {
        var teachers = new List<Teacher>(){
            new Teacher{Name = "Максим", Experience = 10},
            new Teacher{Name = "Виктор", Experience = 15},
            new Teacher{Name = "Павел", Experience = 9},
        };
 
        var students = new List<Student>(){
            new Student{Name = "Иван", TeacherName = "Виктор", MaxMark = 5},
            new Student{Name = "Евгений", TeacherName = "Виктор", MaxMark = 2},
            new Student{Name = "Марина", TeacherName = "Максим", MaxMark = 3},
            new Student{Name = "Виталий", TeacherName = "Максим", MaxMark = 4},
            new Student{Name = "Олег", TeacherName = "Виктор", MaxMark = 5},
            new Student{Name = "Георгий", TeacherName = "Павел", MaxMark = 4},
            new Student{Name = "Артур", TeacherName = "Максим", MaxMark = 3},
            new Student{Name = "Ирина", TeacherName = "Павел", MaxMark = 5},
            new Student{Name = "Ольга", TeacherName = "Максим", MaxMark = 4},
 
        };

        //1. Вывести всех учеников с максимальной оценкой выше 3
        var filteredStudents = students.Where(s => s.MaxMark > 3).ToList();
        filteredStudents.ForEach(s => Console.WriteLine($"{s.Name}, mark = {s.MaxMark}"));
        Console.WriteLine("\n\n");
        //2. Вывести всех учеников и их учителей в формате Имя_ученика - Имя_учителя (опыт)
        var newList = from student in students
            from teacher in teachers
            where teacher.Name == student.TeacherName
            let name = $"{teacher.Name} ({teacher.Experience})" 
            select new {student.Name, name, teacher.Experience};
        foreach (var x1 in newList)
        {
            Console.WriteLine($"{x1.Name} _ {x1.name}");
        }
        //3. Вывести средний, максимальный и минимальный баллы среди всех учеников

        Console.WriteLine("average = {0}",students.Average(s => s.MaxMark));
        Console.WriteLine("max = {0}",students.Max(s => s.MaxMark));
        Console.WriteLine("min = {0}",students.Min(s => s.MaxMark));
    }
}