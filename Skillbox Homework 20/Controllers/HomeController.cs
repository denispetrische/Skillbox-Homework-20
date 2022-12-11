using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skillbox_Homework_20.Database;

namespace Skillbox_Homework_20.Controllers
{
    public class HomeController : Controller
    {
        private myContext dbcontext;

        public HomeController(myContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        /// <summary>
        /// Defaul view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(dbcontext.Information.ToList());
        }

        /// <summary>
        /// It triggers after you click on the index
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Show(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            return View(dbcontext.Information.Where(x => x.Id == id).ToList());
        }

        [HttpPost]
        /// <summary>
        /// It adds information to the db
        /// </summary>
        /// <returns></returns>
        public IActionResult AddForm([Bind] Information newInfo)
        {
            try
            {
                dbcontext.Information.Add(newInfo);
                dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch 
            {
                return RedirectToAction("EmptyFields");
            }
        }        

        [HttpGet]
        /// <summary>
        /// It returns a form for adding new information
        /// </summary>
        /// <returns></returns>
        public IActionResult AddForm()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// It deletes the string with the specified id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            try
            {
                var objectInfo = dbcontext.Information.Where(x => x.Id == id).ToList().FirstOrDefault();
                dbcontext.Information.Remove(objectInfo);
                dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch 
            {
                return RedirectToAction("IncorrectIndex");
            }
        }

        [HttpGet]
        /// <summary>
        /// Returns a form where you can select an index
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// Use this form to prevent errors when index is not exist
        /// </summary>
        /// <returns></returns>
        public IActionResult IncorrectIndex()
        {
            return View();
        }

        /// <summary>
        /// Use this form to prevent errors when some fields are empty
        /// </summary>
        /// <returns></returns>
        public IActionResult EmptyFields()
        {
            return View();
        }

        [HttpGet]
        /// <summary>
        /// Basic form for updating
        /// </summary>
        /// <returns></returns>
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// Updating method
        /// </summary>
        /// <returns></returns>
        public IActionResult Update(int? Id, 
                                    string SecondName, 
                                    string FirstName, 
                                    string Patronymic,
                                    string PhoneNumber,
                                    string Adress,
                                    string Description)
        {
            try
            {
                var entity = dbcontext.Information.Where(x => x.Id == Id).ToList().FirstOrDefault();

                if (SecondName != "")
                {
                    entity.SecondName = SecondName;
                }

                if (FirstName != "")
                {
                    entity.FirstName = FirstName;
                }

                if (Patronymic != "")
                {
                    entity.Patronymic = Patronymic;
                }

                if (PhoneNumber != "")
                {
                    entity.PhoneNumber = PhoneNumber;
                }

                if (Adress != "")
                {
                    entity.Adress = Adress;
                }

                if (Description != "")
                {
                    entity.Description = Description;
                }

                dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch 
            {
                return RedirectToAction("EmptyFields");
            }
        }
    }
}
