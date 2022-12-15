using rediscachewithasp.Models;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rediscachewithasp.Controllers
{
    public class HomeController : Controller
    {
        private readonly RedisEndpoint _redisEndpoint;
        public HomeController()
        {
            //var host = ConfigurationManager.AppSettings["localhost"].ToString();
            //var port = Convert.ToInt32(ConfigurationManager.AppSettings["6379"]);
            _redisEndpoint = new RedisEndpoint("Localhost", 6379);
        }

   

        public ActionResult SetStrings(user value)
        {
            var email = value.email;
            if (email != null)
            {
                using (var redisClient = new RedisClient(_redisEndpoint))
                {
                    redisClient.SetValue("key", email);
                }
                return RedirectToAction("GetString");

            }
            else { return View(); }


        }

        public ActionResult GetString()
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
               ViewBag.data= redisClient.GetValue("key");
            }
            return View();
        }

    }
}