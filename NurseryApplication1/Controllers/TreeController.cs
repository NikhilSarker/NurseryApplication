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
            DetailsTree ViewModel = new DetailsTree();


            //Objective: Communicate with our tree data api to retrieve one tree
            //Curl https://localhost:44307/api/treedata/findtree/{id}


            string url = "treedata/findtree/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            TreeDto SelectedTree = response.Content.ReadAsAsync<TreeDto>().Result;
            //Debug.WriteLine("Tree received : ");
            //Debug.WriteLine(selectedtree.TreeName);

            ViewModel.SelectedTree = SelectedTree;



            url = "caretakerdata/ListCaretakersForTree/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<CaretakerDto> ResponsibleCaretakers = response.Content.ReadAsAsync<IEnumerable<CaretakerDto>>().Result;

            ViewModel.ResponsibleCaretakers = ResponsibleCaretakers;
            url = "caretakerdata/listcaretakersnotcaringfortree/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<CaretakerDto> AvailableCaretakers = response.Content.ReadAsAsync<IEnumerable<CaretakerDto>>().Result;

            ViewModel.AvailableCaretakers = AvailableCaretakers;



            return View(ViewModel);
        }

        //POST: Tree/Assiciate/{treeid}
        [HttpPost]
        public ActionResult Associate(int id, int CaretakerId)
        {

            string url = "treedata/associatetreewithcaretaker/" + id + "/"+ CaretakerId;


            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            return RedirectToAction("Details/" + id);

        }




        //GET: Tree/unAssiciate/{id}caretakerId=/{caretakerid}
        [HttpGet]
        public ActionResult UnAssociate(int id, int CaretakerId)
        {

            string url = "treedata/unassociatetreewithcaretaker/" + id + "/" + CaretakerId;


            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            return RedirectToAction("Details/" + id);

        }


        public ActionResult Error()
        {
            return View();
        }
        // GET: Tree/New
        public ActionResult New()
        {

            //Information about all categories in the system
            //Get api/categorydata/listCategories

            string url = "categorydata/listcategories";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<CategoryDto> categoriesOptions = response.Content.ReadAsAsync<IEnumerable<CategoryDto>>().Result;

            return View(categoriesOptions);
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
            UpdateTree ViewModel = new UpdateTree();


            string url = "treedata/findtree/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The respose code is ");
            //Debug.WriteLine(response.StatusCode);

            TreeDto SelectedTree = response.Content.ReadAsAsync<TreeDto>().Result;
            //Debug.WriteLine("Tree received : ");
            //Debug.WriteLine(selectedtree.TreeName);
            ViewModel.SelectedTree = SelectedTree;

            url = "categorydata/listcategories/";
            response = client.GetAsync(url).Result;

            IEnumerable<CategoryDto> CategoriesOptions = response.Content.ReadAsAsync<IEnumerable<CategoryDto>>().Result;
            ViewModel.CategoriesOptions = CategoriesOptions;
            return View(ViewModel);
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
