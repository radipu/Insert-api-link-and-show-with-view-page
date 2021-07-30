using ITGROWTEST_NEW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ITGROWTEST_NEW.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var newsArticles = GetArticles();
            return View(newsArticles);
        }

        protected DataContainer GetArticles()
        {
            const string apiKey = "x-api-key";
            const string clientKey = "7cb1cc4c775e4bb3b4765a0cae5c6c06";
            const string url = "https://newsapi.org/v2/everything?q=COVID&from=2021-07-13";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(apiKey, clientKey);

            string jsonResponse;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                jsonResponse = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }

            var finalResults = JsonConvert.DeserializeObject<DataContainer>(jsonResponse);
            return finalResults;
        }
    }
}
