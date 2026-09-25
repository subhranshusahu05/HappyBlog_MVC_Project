using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBlog.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "UserName is required")]
        [MaxLength(100, ErrorMessage = "UserName  cannot exceed 100 characters")]
        public string UserName { get; set; }



        [DataType(DataType.Date)]// Specify that only the date part is relevant
        public DateTime CommentDate { get; set; }




        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [ForeignKey("Post")]

        public int  PostId { get; set; }
        
        public Post Post {  get; set; }

    }
}
