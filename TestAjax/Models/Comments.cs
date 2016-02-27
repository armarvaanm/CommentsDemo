using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommentsDemo.Models
{
    public class CommentsModal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public string Email{ get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Comments { get; set; }
        
        public DateTime CommentCreatedDate { get; set; }
    }


}