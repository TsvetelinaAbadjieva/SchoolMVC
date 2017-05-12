using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;


namespace SchoolMVC.Controllers
{
    public class HomeController : Controller
    {

        SchoolDataContext Context;

        public HomeController()
        {
            Context = new SchoolDataContext();
        }
        //Извежда списък на всички учители
        [HttpGet]
        public ActionResult GetTeachers(string search, int? page)
        {
            if (search == null) search = "";
            
            IList<Teacher> teachers = new List<Teacher>();
            List<ShowAllTeachersDetailedResult> result = Context.ShowAllTeachersDetailed().ToList();
            foreach (var m in result)
            {   
                if (m.FirstName.ToLower().Contains(search.ToLower()) || m.MiddleName.ToLower().Contains(search.ToLower()) || m.LastName.ToLower().Contains(search.ToLower()) ||m.Address.ToLower().Contains(search.ToLower())||m.DateOfBirth.ToString().Contains(search.ToLower())||(m.Subject != null && m.Subject.ToLower().Contains(search.ToLower()))||(m.Subject2!= null && m.Subject2.ToLower().Contains(search.ToLower()))||m.UserName.ToLower().Contains(search.ToLower())||search=="")
                {  
                    
                    Teacher teacher = new Teacher();
                    teacher.Id = m.Id;
                    teacher.FirstName = m.FirstName;
                    teacher.MiddleName = m.MiddleName;
                    teacher.LastName = m.LastName;
                    teacher.Position = m.Position;
                    teacher.DateOfBirth = m.DateOfBirth;
                    teacher.Address = m.Address;
                    teacher.UserName = m.UserName;
                    teacher.Subject = m.Subject;
                    teacher.Subject2 = m.Subject2;
                    teacher.IsDeleted = false;

                    teachers.Add(teacher);
                    
                }

            }
            return View(teachers.ToPagedList<Teacher>(page??1,10));
        }
        //Извежда списък на всички ученици
        public ActionResult GetStudents(string search, int? page)
        {
            if (search == null) search = "";
            IList<Student> students = new List<Student>();
            List<ShowAllStudentsDetailedResult> result = Context.ShowAllStudentsDetailed().ToList();
            foreach (var m in result)
            {
                if (m.FirstName.ToLower().Contains(search.ToLower()) || m.MiddleName.ToLower().Contains(search.ToLower()) || m.LastName.ToLower().Contains(search.ToLower()) || m.Address.ToLower().Contains(search.ToLower()) || m.DateOfBirth.ToString().Contains(search.ToLower()) || (m.MotherName != null && m.MotherName.ToLower().Contains(search.ToLower())) || (m.FatherName != null && m.FatherName.ToLower().Contains(search.ToLower())) ||(m.UserName!=null && m.UserName.ToLower().Contains(search.ToLower())) || search == "")
                {
                    Student student = new Student();
                    student.Id = m.Id;
                    student.Number = m.Number;
                    student.Degree = Convert.ToInt32(m.Degree);
                    student.Letter = m.Letter;
                    student.FirstName = m.FirstName;
                    student.MiddleName = m.MiddleName;
                    student.LastName = m.LastName;
                    student.DateOfBirth = m.DateOfBirth;
                    student.Address = m.Address;
                    student.UserName = m.UserName;
                    student.MotherName = m.MotherName;
                    student.FatherAddress = m.FatherAddress;
                    student.FatherName = m.FatherName;
                    student.FatherAddress = m.FatherAddress;
                    student.IsDeleted = false;

                    students.Add(student);
                }

            }
            return View(students.ToPagedList<Student>(page ?? 1, 10));
        }
        //Подготвя за редакция учител-избира го от списъка по ИД
        [HttpGet]
        [ActionName("EditTeacher")]
        public ActionResult EditTeacher(int id)
        {
            Teacher teacher = new Teacher();
            List<FindTeacherByIdResult> list = Context.FindTeacherById(id).ToList();
            foreach(var m in list)
            {
                teacher.Id = m.Id;
                teacher.FirstName = m.FirstName;
                teacher.MiddleName = m.MiddleName;
                teacher.LastName = m.LastName;
                teacher.Address = m.Address;
                teacher.Position = m.Position;
                teacher.DateOfBirth = m.DateOfBirth;
                teacher.UserName = m.UserName;
                teacher.Subject = m.Subject;
                teacher.Subject2 = m.Subject2;
                teacher.IsDeleted = false;
            }
            return View(teacher);
        }
        //Редактира учител
        [HttpPost]
        [ActionName("EditTeacher")]
        public ActionResult EditSelectedTeacher()
        {
            Teacher model = new Teacher();
            if (ModelState.IsValid)
            {

                UpdateModel(model);
                Context.UpdateTeacher(model.Id, model.FirstName, model.MiddleName, model.LastName, model.Address, model.Position, Convert.ToDateTime(model.DateOfBirth), model.Subject, model.Subject2, model.UserName);
                Context.SubmitChanges();
                return RedirectToAction("GetTeachers");
            }
            else return View();
        }
    
        //Изтрива избран ред
       [HttpPost]
       [ActionName("DeletePerson")]
        public ActionResult DeletePerson(int id)
        {
            
            Context.DeletePersonById(id);
            Context.SubmitChanges();
            
            return RedirectToAction("GetTeachers");
        }

    
        //Детайли за учител
        public ActionResult DetailsTeacher(int id)
        {
            Teacher teacher = new Teacher();
            List<FindTeacherByIdResult> list = Context.FindTeacherById(id).ToList();
            foreach (var m in list)
            {
                teacher.Id = m.Id;
                teacher.FirstName = m.FirstName;
                teacher.MiddleName = m.MiddleName;
                teacher.LastName = m.LastName;
                teacher.Address = m.Address;
                teacher.DateOfBirth = m.DateOfBirth;
                teacher.UserName = m.UserName;
                teacher.Subject = m.Subject;
                teacher.Subject2 = m.Subject2;
            }
            return View(teacher);
        }

        [HttpGet]
        [ActionName("CreateTeacher")]
        public ActionResult CreateTeacher()
        {
           
             return View();
        }//ctreate only the form

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [ActionName("CreateTeacher")]
        public ActionResult CreateNewTeacher()
        {
            Teacher model = new Teacher();
            if (ModelState.IsValid)
            {

                UpdateModel(model);
                string autoPassword = Guid.NewGuid().ToString();
                var count = Convert.ToInt32(Context.IFExistsPerson(model.FirstName, model.MiddleName, model.LastName, Convert.ToDateTime(model.DateOfBirth)));
                if (count == 0)
                {
                    Context.InsertTeacher(model.FirstName, model.MiddleName, model.LastName, model.Address, Convert.ToDateTime(model.DateOfBirth), model.Position, model.Subject, model.Subject2, model.UserName, autoPassword);
                    Context.SubmitChanges();
                    return RedirectToAction("GetTeachers");
                }
                else
                {
                    //ViewBag.Error = "Записът вече съществува в базата данни!";
                    //  string str = "Записът вече съществува в базата данни!";
                    return RedirectToAction("GetTeachers");
                }
            }
            else  return View();
        }

        public ActionResult DisplayErrorMessage(string str)
        {
           
            return View(str);
        }
        public ActionResult DisplayConfirmationMessage(string message)
        {

            return View(message);
        }

        
        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";

            return View();
        }
        [HttpPost]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}