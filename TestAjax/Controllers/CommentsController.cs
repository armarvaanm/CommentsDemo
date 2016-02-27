using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommentsDemo.Models;
using log4net;
using System.Threading.Tasks;

namespace CommentsDemo.Controllers
{
    public class CommentsController : Controller
    {

        private static log4net.ILog Log { get; set; }

        ILog log = log4net.LogManager.GetLogger(typeof(CommentsController));

        CommentsApiController _serv = null;

        public CommentsController()
        {
         
        }

        // GET: Comments
        public ActionResult Index()

        {
            _serv = new CommentsApiController();
            var model = new CommentsViewModal();

            model.Comments = string.Empty;
            model.List = AutoMapper.Mapper.Map < List<CommentsViewModal>> (_serv.GetComments().Where(x => x.Email == User.Identity.Name.Trim()).ToList());

            return View(model);
        }

        public ActionResult GetComments()
        {
            _serv = new CommentsApiController();
            var model = new CommentsViewModal();

            model.Comments = string.Empty;
            model.List = AutoMapper.Mapper.Map<List<CommentsViewModal>>(_serv.GetComments().Where(x=>x.Email==User.Identity.Name.Trim()).ToList());

            return PartialView("_List", model.List);
        }

        // GET: Comments/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        public async Task<ActionResult> Create(CommentsViewModal collection)
        {
            try
            {
                _serv = new CommentsApiController();

                collection.Email = User.Identity.Name;
                await _serv.PostCommentsModal(AutoMapper.Mapper.Map<CommentsViewModal, CommentsModal>(collection));

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return View();
            }
        }

        // GET: People/Delete/5
        public  ActionResult Delete(int? id)
        {
            
            return PartialView();
        }
        
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string url = Url.Action("GetComments", "Comments");
            try
            {

                _serv = new CommentsApiController();
                await _serv.DeleteCommentsModal(id);


            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            return Json(new { success = true, url = url });

        }

        public ActionResult SearchByComment(string SearchString)
        {
            _serv = new CommentsApiController();
            var model = new CommentsViewModal();

            model.Comments = string.Empty;
            model.List = AutoMapper.Mapper.Map<List<CommentsViewModal>>(_serv.GetComments().Where(x =>(x.Email == User.Identity.Name.Trim()) &&( x.Comments.Contains(SearchString))).ToList());

            return PartialView("_List", model.List);
        }

        public ActionResult SampleException()
        {
            try
            {
                int x, y, z;
                x = 5; y = 0;
                z = x / y;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            return Json(new{ data= "Exception Handled"});
        }
    }
}
