using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PushNotifyApp.Models;
using PushNotifyApp.Hubs;

namespace PushNotifyApp.Controllers
{
    public class HomeController : Controller
    {
        static List<Book> books;
        static HomeController()
        {
            books = new List<Book>();
            books.Add(new Book { Author="Л.Толстой", Name = "Война и мир", Year= 1867, Price=2000 });
            books.Add(new Book { Author="И.Тургенев", Name = "Отцы и дети", Year=1862, Price=3000 });
        }
        public ActionResult Index()
        {
            return View(books);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            books.Add(book);
            SendMessage("Добавлен новый объект");
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        private void SendMessage(string message)
        {
            // Получаем контекст хаба
            var context = 
                Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            
            // отправляем сообщение
            context.Clients.All.displayMessage(message);
        }
    }
}