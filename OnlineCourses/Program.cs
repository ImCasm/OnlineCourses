using System;
using Microsoft.EntityFrameworkCore;


namespace OnlineCourses
{
    public class Program
    {
        public static void Main(string[] args)
        {

            using (var db = new AppContext())
            {
                var courses = db.Course.AsNoTracking();
                foreach (var course in courses)
                {
                    Console.WriteLine("{0} - {1}", course.Title, course.Description);
                }
            }
        }
    }
}
