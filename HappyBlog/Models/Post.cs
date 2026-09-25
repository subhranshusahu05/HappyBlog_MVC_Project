using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Title is required")]
        [MaxLength(200,ErrorMessage ="Title cannot exceeds 200 character")]
        public string Title { get; set; }



        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }


        [Required(ErrorMessage = "Author is required")]
        [MaxLength(100, ErrorMessage = "AuthorName cannot exceeds 100 character")]
        public string Author { get; set; }


        [ValidateNever]
        public string FeatureImagePath { get; set; }


        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }= DateTime.Now;


        //foreign key relation one to many relation 

        [ForeignKey("Category")]// Foreign key attribute
        public int CategoryId { get; set; }// Foreign key to Category
        [ValidateNever]

        public Category Category { get; set; } // Navigation property




        [ValidateNever]

        public ICollection<Comment> Comments { get; set; } // Navigation property to related comments


    }
}
