using HappyBlog.Data;
using HappyBlog.Models;
using HappyBlog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HappyBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;

        public PostController(AppDbContext context, IWebHostEnvironment webHostEnvironment) //Data base used - Depandancy injection - we make constructor
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }



   



        [HttpGet]

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var PostViewModel = new PostViewModel();
            PostViewModel.Categories = _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            return View(PostViewModel);
        }


        private readonly String[] _allowedExtension = { ".jpg", ".jpeg", ".png", };

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                if (postViewModel.FeatureImages == null)
                {
                    ModelState.AddModelError("FeatureImages", "Please select Only .jpg, .jpeg, .png files.");

                    postViewModel.Categories = _context.Categories
                        .Select(c => new SelectListItem
                        {
                            Text = c.Name,
                            Value = c.Id.ToString()
                        }).ToList();

                    return View(postViewModel);
                }


                var intputFileExtension = Path.GetExtension(postViewModel.FeatureImages.FileName).ToLower();
                bool isAllowed = _allowedExtension.Contains(intputFileExtension);

                if (!isAllowed)
                {
                    ModelState.AddModelError("FeatureImages", "Only .jpg, .jpeg, .png files are allowed.");
                    return View(postViewModel);


                }

                // Process the uploaded file (e.g., save it to a directory)
                postViewModel.post.FeatureImagePath = await UploadFiletoFolder(postViewModel.FeatureImages);//method to upload file to folder 
                                                                                                            // Save the post data to the database
                await _context.Posts.AddAsync(postViewModel.post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to a suitable page after creation
            }

            // 🔴 IMPORTANT: Reload categories when ModelState is invalid
            postViewModel.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();



            return View(postViewModel);

        }

        private readonly IWebHostEnvironment _webHostEnvironment;
        private async Task<string> UploadFiletoFolder(IFormFile file)
        {
            var inputFileExtension = Path.GetExtension(file.FileName);

            var fileName = Guid.NewGuid().ToString() + inputFileExtension;//unique file name

            var wwwRootPath = _webHostEnvironment.WebRootPath;//wwwroot folder path SEE LINE 13.

            var imagesFolderPath = Path.Combine(wwwRootPath, "images");//images folder path

            if (!Directory.Exists(imagesFolderPath)) //if images folder not exist create it
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var filePath = Path.Combine(imagesFolderPath, fileName);//full path to save file

            try
            {
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);

                }

            }

            catch (Exception ex)
            {

                return "Error: " + ex.Message;
            }

            return "/images/" + fileName; //return relative path to save in database
        }


        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            var PostQuery = _context.Posts.Include(p => p.Category).AsQueryable(); //Eager loading Category data


            if (categoryId.HasValue)
            {
                PostQuery = PostQuery.Where(p => p.CategoryId == categoryId);
            }
            var posts = PostQuery.ToList();


            ViewBag.Categories = _context.Categories.ToList();

            return View(posts);

        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _context.Posts.Include(p => p.Category).Include(p => p.Comments).FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


        [Authorize]
        public JsonResult AddComment([FromBody] Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Json
            (new
            {
                username = comment.UserName,
                commentDate = comment.CommentDate.ToString("MMMM dd ,yyyy"),
                Content = comment.Content

            }

            );
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (postFromDb == null)
            {
                return NotFound();
            }

            EditViewModel editViewModel = new EditViewModel()
            {
                post = postFromDb,
                Categories = _context.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList()
            };

            return View(editViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }

            var postFromDb = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == editViewModel.post.Id);

            if (postFromDb == null)
            {
                return NotFound();
            }

            //feature image update logic

            if (editViewModel.FeatureImages != null)
            {
                var inputFileExtension = Path.GetExtension(editViewModel.FeatureImages.FileName).ToLower();
                bool isAllowed = _allowedExtension.Contains(inputFileExtension);
                if (!isAllowed)
                {
                    ModelState.AddModelError("FeatureImages", "Only .jpg, .jpeg, .png files are allowed.");
                    return View(editViewModel);
                }
                var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Path.GetFileName(postFromDb.FeatureImagePath));

                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }

                editViewModel.post.FeatureImagePath = await UploadFiletoFolder(editViewModel.FeatureImages);

            }

            else
            {
                editViewModel.post.FeatureImagePath = postFromDb.FeatureImagePath;
            }

            _context.Posts.Update(editViewModel.post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (postFromDb == null)
            {
                return NotFound();
            }


            return View(postFromDb);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);


            if (string.IsNullOrEmpty(postFromDb.FeatureImagePath))
            {
                var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Path.GetFileName(postFromDb.FeatureImagePath));
                if (System.IO.File.Exists(existingFilePath))
                {

                    System.IO.File.Delete(existingFilePath);

                }

            }
            _context.Posts.Remove(postFromDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");



        }


    }
}
