using NurseryApplication1.Models;
using NurseryApplication1.Models.ViewModels;
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
    public class CategoryController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CategoryController()

        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44307/api/");
        }
        // GET: Category/List
        public ActionResult List()
        {
            //Objective: Communicate with our tree data api to retrieve a list of trees
            //Curl https://localhost:44307/api/categorydata/listcategoriess
            //HttpClient client = new HttpClient();
            string url = "categorydata/listcategories";
            //string url = "https://localhost:44307/api/categorydata/listcategories";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<CategoryDto> categories = response.Content.ReadAsAsync<IEnumerable<CategoryDto>>().Result;
            //Debug.WriteLine("Number of trees received : ");
            //Debug.WriteLine(trees.Count());


            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            DetailsCategory ViewModel = new DetailsCategory();

            //Objective: Communicate with our tree data api to retrieve one tree
            //Curl https://localhost:44307/api/categorydata/findcategory/{id}


            string url = "categorydata/findcategory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            CategoryDto Selectedcategory = response.Content.ReadAsAsync<CategoryDto>().Result;
            //Debug.WriteLine("Tree received : ");
            //Debug.WriteLine(selectedtree.TreeName);

            ViewModel.SelectedCategory = Selectedcategory;

            url = "treedata/listtreesforcategory/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<TreeDto> RelatedTrees = response.Content.ReadAsAsync<IEnumerable<TreeDto>>().Result;

            ViewModel.RelatedTrees = RelatedTrees;


            return View(ViewModel);
        }
        public ActionResult Error()
        {
            return View();
        }
        // GET: Category/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            //Debug.WriteLine("The inputted tre name is : ");
            //Debug.WriteLine(tree.TreeName);
            //Objective: Add a new tree into our system using the api
            //Curl -H "Content-type:application.json" -d @tree.json https://localhost:44307/api/caregorydata/addcategory

            string url = "categorydata/addcategory";

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(category);
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

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "categorydata/findcategory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            CategoryDto selectedcategory = response.Content.ReadAsAsync<CategoryDto>().Result;
            //Debug.WriteLine("Tree received : ");
            //Debug.WriteLine(selectedtree.TreeName);


            return View(selectedcategory);
        }

        // POST: Category/Update/5
        [HttpPost]
        public ActionResult Update(int id, Category category)
        {
            string url = "categorydata/updatecategory/" + id;

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(category);
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

        // GET: Category/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "categorydata/findcategory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CategoryDto selectedcategory = response.Content.ReadAsAsync<CategoryDto>().Result;

            return View(selectedcategory);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Category category)
        {
            string url = "categorydata/deletecategory/" + id;


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
