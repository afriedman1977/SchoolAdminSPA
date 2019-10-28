using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dal;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolAdminSPA.Controllers
{
   // [Route("api/[controller]")]
    public class StudentController : Controller
    {
        // GET: api/values
        [HttpGet]
       // [Route("/GetStudents")]
        public IEnumerable<Student> GetStudents()
        {
            using(var ctx = new SchoolDBContext())
            {
                return ctx.Students.ToList();
            }
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        public Student GetStudent(int id)
        {
            using(var ctx = new SchoolDBContext())
            {
                return ctx.Students.FirstOrDefault(s => s.StudentId == id);
            }
        }

        // POST api/values
        [HttpPost]
        public void AddStudent([FromBody]Student student)
        {
            using(var ctx = new SchoolDBContext())
            {
                ctx.Students.Add(student);
                ctx.SaveChanges();
            }
        }

        // PUT api/values/5
        [HttpPost]
        public void EditStudent([FromBody]Student student)
        {
            using (var ctx = new SchoolDBContext())
            {
                ctx.Students.Attach(student);
                ctx.Entry(student).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpPost]
        public void DeleteStudent([FromBody]int id)
        {
            using (var ctx = new SchoolDBContext())
            {
                var student = ctx.Students.FirstOrDefault(s => s.StudentId == id);
                ctx.Entry(student).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }
    }
}
