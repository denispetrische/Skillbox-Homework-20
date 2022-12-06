using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skillbox_Homework_20.Database;

namespace Skillbox_Homework_20.Controllers
{
    public class HomeController : Controller
    {
        private myContext dbcontext;
        public IActionResult Index()
        {
            return View(dbcontext.Information.ToList());
        }

        public IActionResult Show(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            return View(dbcontext.Information.Where(x => x.Id == id).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Information newInfo)
        {
            dbcontext.Information.Add(newInfo);
            await dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public HomeController(myContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
    }
}
