using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShushiSiteApi.DbModels;
using MyShushiSiteApi.ViewModels;
using System.IO;
using System.Net.Mime;

namespace MyShushiSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        const string DIRECTORY_PATH = @"FileStorage\CategoriesImg\";
        private MyDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;

        public CategoryController(MyDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _hostingEnvironment = environment;
        }

        /// <summary>
        /// Возвращает: Cписок всех категорий
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryVM>>> GetAll()
        {
            var categories = _db.Categories.Where(c => c.IsActive == true).ToList();

            if (categories == null)
            {
                return NotFound($"Категории не найдены!");
            }

            var viewModelCategories = categories.AsEnumerable().Select(c =>
            {
                var imgPath = Path.Combine(_hostingEnvironment.ContentRootPath, c.ImageUrl);
                var iconPath = Path.Combine(_hostingEnvironment.ContentRootPath, c.IconUrl);

                var imgFileBytes = System.IO.File.ReadAllBytes(imgPath);
                var iconFileBytes = System.IO.File.ReadAllBytes(iconPath);

                //FormFile fromImgFile = null;
                //FormFile fromIconFile = null;

                //using (var ms = new MemoryStream(imgFileBytes))
                //{
                //    var extension = Path.GetExtension(imgPath).Trim('.');

                //    fromImgFile = new FormFile(ms, 0, ms.Length, Path.GetFileNameWithoutExtension(imgPath), Path.GetFileName(imgPath))
                //    {
                //        Headers = new HeaderDictionary(),
                //        ContentType = $"image/{extension}"
                //    };

                //    System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                //    {
                //        FileName = Path.GetFileName(imgPath)
                //    };
                //    fromImgFile.ContentDisposition = cd.ToString();
                //}

                //using (var ms = new MemoryStream(iconFileBytes))
                //{
                //    var extension = Path.GetExtension(iconPath).Trim('.');

                //    fromIconFile = new FormFile(ms, 0, ms.Length, Path.GetFileNameWithoutExtension(iconPath), Path.GetFileName(iconPath))
                //    {
                //        Headers = new HeaderDictionary(),
                //        ContentType = $"image/{extension}"
                //    };

                //    System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                //    {
                //        FileName = Path.GetFileName(iconPath)
                //    };
                //    fromIconFile.ContentDisposition = cd.ToString();
                //}

                //return new CategoryViewModel()
                //{
                //    Id = c.Id,
                //    Name = c.Name,
                //    Url = c.Url,
                //    ImageData = fromImgFile,
                //    IconData = fromIconFile
                //};


                return new CategoryVM()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Url = c.Url,
                    ImageData = imgFileBytes,
                    IconData = iconFileBytes
                };
            }).ToList();

            return Ok(viewModelCategories);
        }

        /// <summary>
        /// Создает: Новую категорию
        /// </summary>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        //[ProducesDefaultResponseType]
        [HttpPost]
        public async Task<ActionResult<CategoryViewModel>> Post([FromForm] CategoryViewModel categoryViewModel)
        {
            try
            {
                string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, DIRECTORY_PATH);

                var category = new Category { Id = 0, IsActive = true,  Name = categoryViewModel.Name, Url = categoryViewModel.Url };

                var categoryImg = categoryViewModel.ImageData;
                var categoryIcon = categoryViewModel.IconData;

                if (categoryImg.Length > 0)
                {
                    string imgPath = Path.Combine(uploads, categoryImg.FileName);

                    if (System.IO.File.Exists(imgPath))
                    {
                        return BadRequest($"Файл с именем '{categoryImg.FileName}' уже существует на сервере!");
                    }

                    using (Stream fileStream = new FileStream(imgPath, FileMode.Create))
                    {
                        await categoryImg.CopyToAsync(fileStream);
                    }

                    category.ImageUrl = Path.Combine(DIRECTORY_PATH, categoryImg.FileName);
                }

                if (categoryIcon.Length > 0)
                {
                    string iconPath = Path.Combine(uploads, categoryIcon.FileName);

                    if (System.IO.File.Exists(iconPath))
                    {
                        return BadRequest($"Файл с именем '{categoryIcon.FileName}' уже существует на сервере!");
                    }

                    using (Stream fileStream = new FileStream(iconPath, FileMode.Create))
                    {
                        await categoryIcon.CopyToAsync(fileStream);
                    }

                    category.IconUrl = Path.Combine(DIRECTORY_PATH, categoryIcon.FileName);
                }

                _db.Categories.Add(category);
                _db.SaveChanges();

                categoryViewModel.Id = category.Id;

                return Ok(categoryViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }
    }
}
