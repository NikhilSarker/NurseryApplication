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
    public class TreeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TreeController()

        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44307/api/");
        }
        // GET: Tree/List
        public ActionResult List()
        {
            //Objective: Communicate with our tree data api to retrieve a list of trees
            //Curl https://localhost:44307/api/treedata/listtrees
            //HttpClient client = new HttpClient();
            string url = "treedata/listtrees";
            //string url = "https://localhost:44307/api/treedata/listtrees";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<TreeDto> trees = response.Content.ReadAsAsync<IEnumerable<TreeDto>>().Result;
            //Debug.WriteLine("Number of trees received : ");
            //Debug.WriteLine(trees.Count());


            return View(trees);
        }

        // GET: Tree/Details/5
        public ActionResult Details(int id)
        {
            //Objective: Communicate with our tree data api to retrieve one tree
            //Curl https://localhost:44307/api/treedata/findtree/{id}


            string url = "treedata/findtree/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            TreeDto selectedtree = response.Content.ReadAsAsync<TreeDto>().Result;
            //Debug.WriteLine("Tree received : ");
            //Debug.WriteLine(selectedtree.TreeName);


            return View(selectedtree);
        }

        public ActionResult Error()
        {
            return View();
        }
        // GET: Tree/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Tree/Create
        [HttpPost]
        public ActionResult Create(Tree tree)
        {
            //Debug.WriteLine("The inputted tre name is : ");
            //Debug.WriteLine(tree.TreeName);
            //Objective: Add a new tree into our system using the api
            //Curl -H "Content-type:application.json" -d @tree.json https://localhost:44307/api/treedata/addtree

            string url = "treedata/addtree";

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(tree);
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

        // GET: Tree/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "treedata/findtree/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            TreeDto selectedtree = response.Content.ReadAsAsync<TreeDto>().Result;
            //Debug.WriteLine("Tree received : ");
            //Debug.WriteLine(selectedtree.TreeName);


            return View(selectedtree);
        }

        // POST: Tree/Update/5
        [HttpPost]
        public ActionResult Update(int id, Tree tree)
        {
            string url = "treedata/updatetree/" + id;

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(tree);
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

        // GET: Tree/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "treedata/findtree/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            TreeDto selectedtree = response.Content.ReadAsAsync<TreeDto>().Result;

            return View(selectedtree);
        }

        // POST: Tree/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Tree tree)
        {
            string url = "treedata/deletetree/" + id;


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
