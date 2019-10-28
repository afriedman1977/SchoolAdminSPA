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
    public class TestController : Controller
    {
        // GET: api/values
        [HttpGet]
        public Student GetStudent(int id)
        {
            using (var ctx = new SchoolDBContext())
            {
                return ctx.Students.Include(s => s.Tests).FirstOrDefault(s => s.StudentId == id);
            }
        }

        // GET api/values/5
        [HttpPost]
        public void AddTest([FromBody]Test test)
        {
            using (var ctx = new SchoolDBContext())
            {
                ctx.Tests.Add(test);
                ctx.SaveChanges();
            }
        }

        [HttpPost]
        public void EditTest([FromBody]Test test)
        {
            using (var ctx = new SchoolDBContext())
            {
                ctx.Tests.Attach(test);
                ctx.Entry(test).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        [HttpPost]
        public void DeleteTest([FromBody]int id)
        {
            using (var ctx = new SchoolDBContext())
            {
                var test = ctx.Tests.FirstOrDefault(s => s.TestId == id);
                ctx.Entry(test).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }
    }
}
