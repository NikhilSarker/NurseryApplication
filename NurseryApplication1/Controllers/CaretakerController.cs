using NurseryApplication1.Migrations;
using NurseryApplication1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NurseryApplication1.Controllers
{
    public class CaretakerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CaretakerController()

        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44307/api/");
        }
        // GET: Caretaker/List
        public ActionResult List()
        {
            //Objective: Communicate with our tree data api to retrieve a list of trees
            //Curl https://localhost:44307/api/treedata/listtrees
            //HttpClient client = new HttpClient();
            string url = "caretakerdata/listcaretakers";
            //string url = "https://localhost:44307/api/caretakerdata/listcaretakers";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<CaretakerDto> caretakers = response.Content.ReadAsAsync<IEnumerable<CaretakerDto>>().Result;
            //Debug.WriteLine("Number of trees received : ");
            //Debug.WriteLine(trees.Count());


            return View(caretakers);
        }

        // GET: Caretaker/Details/5
        public ActionResult Details(int id)
        {
            //Objective: Communicate with our tree data api to retrieve one tree
            //Curl https://localhost:44307/api/caretakerdata/findcaretaker/{id}


            string url = "caretakerdata/findcaretaker/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            CaretakerDto selectedcaretaker = response.Content.ReadAsAsync<CaretakerDto>().Result;
            //Debug.WriteLine("Tree received : ");
            //Debug.WriteLine(selectedtree.TreeName);


            return View(selectedcaretaker);
        }

        public ActionResult Error()
        {
            return View();
        }
        // GET: Caretaker/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Caretaker/Create
        [HttpPost]
        public ActionResult Create(Caretaker caretaker)
        {
            //Debug.WriteLine("The inputted tre name is : ");
            //Debug.WriteLine(tree.TreeName);
            //Objective: Add a new tree into our system using the api
            //Curl -H "Content-type:application.json" -d @tree.json https://localhost:44350/api/treedata/addtree

            string url = "caretakerdata/addcaretaker";

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(caretaker);
            Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
            }
        }

        // GET: Caretaker/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "caretakerdata/findcaretaker/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            CaretakerDto selectedcaretaker = response.Content.ReadAsAsync<CaretakerDto>().Result;
            //Debug.WriteLine("Tree received : ");
            //Debug.WriteLine(selectedtree.TreeName);


            return View(selectedcaretaker);
        }

        // POST: Caretaker/Update/5
        [HttpPost]
        public ActionResult Update(int id, Caretaker caretaker)
        {
            string url = "caretakerdata/updatecaretaker/" + id;

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(caretaker);
            //Debug.WriteLine(jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(jsonpayload);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
            }
        }

        // GET: Caretaker/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "caretakerdata/findcaretaker/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CaretakerDto selectedcaretaker = response.Content.ReadAsAsync<CaretakerDto>().Result;

            return View(selectedcaretaker);
        }

        // POST: Caretaker/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Caretaker caretaker)
        {
            string url = "caretakerdata/deletecaretaker/" + id;


            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
            }
        }
    }
}
