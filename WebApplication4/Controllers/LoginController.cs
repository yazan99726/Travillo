using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication4.Models;

namespace task1.Controllers
{
    public class LoginController : Controller
    {
        private readonly ModelContext context;
        public LoginController(ModelContext context1)
        {
           this.context = context1;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        //method =post
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            const string id = "id";

            //slect * from login where username =username and password =pasword
            var item = context.Logins.Where(x => x.Username == username && x.Password == password).SingleOrDefault();

            if (item != null)
            {
                switch (item.Rolid)
                {
                    case 1:                  
                        {
                            HttpContext.Session.SetInt32(id, (int)item.Adminid.Value);
                            return RedirectToAction("Index", "Home");
                        }
                    case 2:
                        {
                            HttpContext.Session.SetInt32(id, (int)item.Clientid.Value);
                            return RedirectToAction("Index", "HomePage");
                        }
                    case 3:
                        {
                            HttpContext.Session.SetInt32(id, (int)item.Accountid.Value);
                            return RedirectToAction("users", "Accountensts");
                        }                  

                }
            }
            else
            {
                return View();
            }
            /*
             singleordefult return on record from database
            firstordefult return first record from database
            lastordefult return last record from database
            tolist()this without where return all record
            *
               ex** select * from login where user name =10
                 .where.tolist()
               ex*** select * from login order by id desc
                 var item = context.UserLogins.orderbydescendin(x=>x.id).tolist()
            **
            last accept null 
            lastordefult accept null
            var item = context.UserLogins.find()//for delete
            */
            return View();
        }
    }
}