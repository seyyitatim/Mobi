using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mobi.DbContext;
using Mobi.Entities;
using Mobi.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mobi.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<AppUser> UserManager { get; set; }
        private SignInManager<AppUser> SignInManager { get; set; }
        public IWebHostEnvironment HostEnvironment { get; }
        private AppDbContext DbContext { get; set; }

        public AdminController(UserManager<AppUser> userManager, AppDbContext dbContext, SignInManager<AppUser> signInManager, IWebHostEnvironment hostEnvironment)
        {
            UserManager = userManager;
            DbContext = dbContext;
            SignInManager = signInManager;
            HostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }

        #region Category
        public async Task<IActionResult> Category()
        {
            var categoryList = await DbContext.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
            return View(categoryList);
        }

        public IActionResult CategoryAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await DbContext.Categories.AnyAsync(c => c.Name == model.Name))
                {
                    Category category = new Category()
                    {
                        Name = model.Name
                    };

                    await DbContext.Categories.AddAsync(category);
                    DbContext.SaveChanges();

                    return RedirectToAction("Category");
                }
                else
                {
                    ModelState.AddModelError("", $"{model.Name} kategori adı daha önce eklenmiştir.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CategoryEdit(int id)
        {
            if (id > 0)
            {
                var category = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                {
                    return RedirectToAction("Category");
                }
                CategoryEditViewModel model = new CategoryEditViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return View(model);

            }
            return RedirectToAction("Category");
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == model.Id);

                if (category != null)
                {
                    category.Name = model.Name;

                    DbContext.Categories.Update(category);
                    await DbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("Category");
        }

        public async Task<IActionResult> CategoryDelete(int id)
        {
            if (id > 0)
            {
                var category = await DbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

                if (category != null)
                {
                    DbContext.Remove(category);
                    await DbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("Category");
        }
        #endregion

        #region Product

        public async Task<IActionResult> Product()
        {
            var productList = await DbContext.Products.Include(p => p.Category).Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                OriginalImagePath = p.OriginalImagePath,
                CategoryName = p.Category.Name
            }).ToListAsync();
            return View(productList);
        }

        public async Task<IActionResult> ProductAdd()
        {
            var categoryList = await DbContext.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
            ViewBag.Category = categoryList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductAdd(ProductAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await DbContext.Products.AnyAsync(c => c.Name == model.Name))
                {
                    Product product = new Product()
                    {
                        Name = model.Name,
                        CategoryId = model.CategoryId
                    };

                    if (model.Image != null)
                    {
                        product.OriginalImagePath = await SaveImage(model.Image);
                    }

                    await DbContext.Products.AddAsync(product);
                    DbContext.SaveChanges();

                    return RedirectToAction("Product");
                }
                else
                {
                    ModelState.AddModelError("", $"{model.Name} ürün adı daha önce eklenmiştir.");
                }
            }
            var categoryList = await DbContext.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
            ViewBag.Category = categoryList;
            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int id)
        {
            if (id > 0)
            {
                var product = await DbContext.Products.FirstOrDefaultAsync(c => c.Id == id);

                if (product == null)
                {
                    return RedirectToAction("Product");
                }
                ProductEditViewModel model = new ProductEditViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    SmallImagePath = product.SmallImagePath,
                    OriginalImagePath = product.OriginalImagePath
                };

                var categoryList = await DbContext.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToListAsync();
                ViewBag.Category = categoryList;

                return View(model);

            }
            return RedirectToAction("Product");
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await DbContext.Products.FirstOrDefaultAsync(c => c.Id == model.Id);

                if (product != null)
                {
                    product.Name = model.Name;
                    product.CategoryId = model.CategoryId;

                    if (model.Image != null)
                    {
                        DeleteImage(product.OriginalImagePath);

                        product.OriginalImagePath = await SaveImage(model.Image);
                    }

                    DbContext.Products.Update(product);
                    await DbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("Product");
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            if (id > 0)
            {
                var product = await DbContext.Products.FirstOrDefaultAsync(c => c.Id == id);

                if (product != null)
                {
                    DeleteImage(product.OriginalImagePath);

                    DbContext.Remove(product);
                    await DbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("Product");
        }
        #endregion

        #region methods

        private async Task<string> SaveImage(IFormFile image)
        {
            var wwwRootPath = HostEnvironment.WebRootPath;
            var fileName = Path.GetFileNameWithoutExtension(image.FileName);
            var extension = Path.GetExtension(image.FileName);

            fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            string imagePath = Path.Combine(wwwRootPath, "image", "products", fileName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return fileName;
        }

        private void DeleteImage(string imagePath)
        {
            string path = Path.Combine(HostEnvironment.WebRootPath, "image", "products", imagePath);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        #endregion
    }
}
