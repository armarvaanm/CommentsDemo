using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommentsDemo.Models
{
    public class CommentsViewModal
    {
        [Key]
        public int CommentId { get; set; }

        public string SearchString{ get; set; }

        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Comments { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CommentCreatedDate { get; set; }

        public List<CommentsViewModal> List { get; set; }

    }
}