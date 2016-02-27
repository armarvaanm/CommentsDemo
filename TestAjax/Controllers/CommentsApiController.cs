using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CommentsDemo.Models;

namespace CommentsDemo.Controllers
{
    public class CommentsApiController : ApiController
    {
        private DataDb db = new DataDb();

        // GET: api/CommentsApi
        public IQueryable<CommentsModal> GetComments()
        {
            return db.Comments;
        }

        // GET: api/CommentsApi/5
        [ResponseType(typeof(CommentsModal))]
        public async Task<IHttpActionResult> GetCommentsModal(int id)
        {
            CommentsModal commentsModal = await db.Comments.FindAsync(id);
            if (commentsModal == null)
            {
                return NotFound();
            }

            return Ok(commentsModal);
        }

        // PUT: api/CommentsApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCommentsModal(int id, CommentsModal commentsModal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commentsModal.CommentId)
            {
                return BadRequest();
            }

            db.Entry(commentsModal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsModalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CommentsApi
        [ResponseType(typeof(CommentsModal))]
        public async Task<IHttpActionResult> PostCommentsModal(CommentsModal commentsModal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            commentsModal.CommentCreatedDate = DateTime.Now;
            
            db.Comments.Add(commentsModal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = commentsModal.CommentId }, commentsModal);
        }
        // DELETE: api/CommentsApi/5
        [ResponseType(typeof(CommentsModal))]
        public async Task<IHttpActionResult> DeleteCommentsModal(int id)
        {
            CommentsModal commentsModal = await db.Comments.FindAsync(id);
            if (commentsModal == null)
            {
                return NotFound();
            }

            db.Comments.Remove(commentsModal);

            db.Entry(commentsModal).State = EntityState.Deleted;
            await db.SaveChangesAsync();

            return Ok(commentsModal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool CommentsModalExists(int id)
        {
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}