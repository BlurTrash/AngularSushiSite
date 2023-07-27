using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using MyShushiSiteApi.DbModels;
using MyShushiSiteApi.ViewModels;
using System.IO;
using System.Linq;
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
                //var imgUrl2 = Path.Combine(_hostingEnvironment.WebRootPath, c.ImageUrl);
                //var iconUrl2 = Path.Combine(_hostingEnvironment.WebRootPath, c.IconUrl);

                var scheme = HttpContext.Request.Scheme;
                var host = HttpContext.Request.Host.Value;
               
                var locationImg = $"{scheme}://{host}/{c.ImageUrl}";
                var locationIcon = $"{scheme}://{host}/{c.IconUrl}";

                //var imgPath = Path.Combine(_hostingEnvironment.ContentRootPath, c.ImageUrl);
                //var iconPath = Path.Combine(_hostingEnvironment.ContentRootPath, c.IconUrl);

                //var imgFileBytes = System.IO.File.ReadAllBytes(imgPath);
                //var iconFileBytes = System.IO.File.ReadAllBytes(iconPath);

                return new CategoryVM()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Url = c.Url,
                    ImageData = CreateFullImgUrl(c.ImageUrl),
                    IconData = CreateFullImgUrl(c.IconUrl),
                    FullUrl = c.FullUrl
                };

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
        public async Task<ActionResult<CategoryVM>> Post([FromForm] CategoryViewModel categoryViewModel)
        {
            //string FileName = file.FileName;
            // combining GUID to create unique name before saving in wwwroot
            //string uniqueFileName = Guid.NewGuid().ToString() + "_" + FileName;
            // getting full path inside wwwroot/images
            //var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", FileName);
            // copying file
            //file.CopyTo(new FileStream(imagePath, FileMode.Create));


            try
            {
                //string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, DIRECTORY_PATH);
                string path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
                string uploads = Path.Combine(path, DIRECTORY_PATH);

                string fullUrl = "/menu/" + categoryViewModel.Url;
                var category = new Category { Id = 0, IsActive = true,  Name = categoryViewModel.Name, Url = categoryViewModel.Url, FullUrl = fullUrl };

                var categoryImg = categoryViewModel.ImageData;
                var categoryIcon = categoryViewModel.IconData;

                string[] validFormat = new string[4] { "jpg", "png", "jpeg", "webp" };

                if (categoryImg != null)
                {
                    string imgPath = Path.Combine(uploads, categoryImg.FileName);
                    var fileImgFormat = categoryImg.FileName.Split(".").Last().ToLower();
                    if (!validFormat.Contains(fileImgFormat))
                    {
                        return BadRequest($"Неверный формат файла '{categoryImg.FileName}'. Допустимые форматы: .jpg, .png, .jpeg .webp!");
                    }
                    if (System.IO.File.Exists(imgPath))
                    {
                        return BadRequest($"Файл с именем '{categoryImg.FileName}' уже существует на сервере!");
                    }
                }

                if (categoryIcon != null)
                {
                    string iconPath = Path.Combine(uploads, categoryIcon.FileName);
                    var fileIconFormat = categoryIcon.FileName.Split(".").Last().ToLower();
                    if (!validFormat.Contains(fileIconFormat))
                    {
                        return BadRequest($"Неверный формат файла '{categoryIcon.FileName}'. Допустимые форматы: .jpg, .png, .jpeg .webp!");
                    }
                    if (System.IO.File.Exists(iconPath))
                    {
                        return BadRequest($"Файл с именем '{categoryIcon.FileName}' уже существует на сервере!");
                    }
                }

                if (categoryImg != null)
                {
                    if (categoryImg.Length > 0)
                    {
                        string imgPath = Path.Combine(uploads, categoryImg.FileName);

                        using (Stream fileStream = new FileStream(imgPath, FileMode.Create))
                        {
                            await categoryImg.CopyToAsync(fileStream);
                        }

                        var partPath = Path.Combine(DIRECTORY_PATH, categoryImg.FileName);
                        category.ImageUrl = partPath.Replace('\\', '/');
                    }
                }
                else return BadRequest($"Изображение не выбрано!");

                if (categoryIcon != null)
                {
                    if (categoryIcon.Length > 0)
                    {
                        string iconPath = Path.Combine(uploads, categoryIcon.FileName);

                        using (Stream fileStream = new FileStream(iconPath, FileMode.Create))
                        {
                            await categoryIcon.CopyToAsync(fileStream);
                        }

                        var partPath = Path.Combine(DIRECTORY_PATH, categoryIcon.FileName);
                        category.IconUrl = partPath.Replace('\\', '/');
                    }
                }
                else return BadRequest($"Иконка не выбрана!");

                _db.Categories.Add(category);
                _db.SaveChanges();

                var categoryVM = new CategoryVM
                {
                    Id = category.Id,
                    Name = category.Name,
                    Url = category.Url,
                    ImageData = CreateFullImgUrl(category.ImageUrl),
                    IconData = CreateFullImgUrl(category.IconUrl),
                    FullUrl = category.FullUrl
                };

                return Ok(categoryVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }


        /// <summary>
        /// Обновляет категорию
        /// </summary>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        //[ProducesDefaultResponseType]
        [HttpPut]
        public async Task<ActionResult<CategoryVM>> Put([FromForm] CategoryViewModel categoryViewModel)
        {
            try
            {
                //string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, DIRECTORY_PATH);
                string path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
                string uploads = Path.Combine(path, DIRECTORY_PATH);

                var category = _db.Categories.FirstOrDefault(c => c.Id == categoryViewModel.Id);

                if (category == null) { return NotFound($"Категория с id={categoryViewModel.Id} не найдена!"); }

                string fullUrl = "/menu/" + categoryViewModel.Url;
                category.Name = categoryViewModel.Name;
                category.Url = categoryViewModel.Url;
                category.FullUrl = fullUrl;

                var categoryImg = categoryViewModel.ImageData;
                var categoryIcon = categoryViewModel.IconData;

                string[] validFormat = new string[4] { "jpg", "png", "jpeg", "webp" };

                if (categoryImg != null)
                {
                    string imgPath = Path.Combine(uploads, categoryImg.FileName);
                    var fileImgFormat = categoryImg.FileName.Split(".").Last().ToLower();
                    if (!validFormat.Contains(fileImgFormat))
                    {
                        return BadRequest($"Неверный формат файла '{categoryImg.FileName}'. Допустимые форматы: .jpg, .png, .jpeg .webp!");
                    }
                    if (System.IO.File.Exists(imgPath))
                    {
                        return BadRequest($"Файл с именем '{categoryImg.FileName}' уже существует на сервере!");
                    }
                }

                if (categoryIcon != null)
                {
                    string iconPath = Path.Combine(uploads, categoryIcon.FileName);
                    var fileIconFormat = categoryIcon.FileName.Split(".").Last().ToLower();
                    if (!validFormat.Contains(fileIconFormat))
                    {
                        return BadRequest($"Неверный формат файла '{categoryIcon.FileName}'. Допустимые форматы: .jpg, .png, .jpeg .webp!");
                    }
                    if (System.IO.File.Exists(iconPath))
                    {
                        return BadRequest($"Файл с именем '{categoryIcon.FileName}' уже существует на сервере!");
                    }
                }

                if (categoryImg != null)
                {
                    if (categoryImg.Length > 0)
                    {
                        string imgPath = Path.Combine(uploads, categoryImg.FileName);

                        using (Stream fileStream = new FileStream(imgPath, FileMode.Create))
                        {
                            await categoryImg.CopyToAsync(fileStream);
                        }

                        string oldFilePath = Path.Combine(path, category.ImageUrl.Replace('/', '\\'));
                        DeleteOldFile(oldFilePath);

                        var partPath = Path.Combine(DIRECTORY_PATH, categoryImg.FileName);
                        category.ImageUrl = partPath.Replace('\\', '/');
                    }
                }

                if (categoryIcon != null)
                {
                    if (categoryIcon.Length > 0)
                    {
                        string iconPath = Path.Combine(uploads, categoryIcon.FileName);

                        using (Stream fileStream = new FileStream(iconPath, FileMode.Create))
                        {
                            await categoryIcon.CopyToAsync(fileStream);
                        }

                        string oldFilePath = Path.Combine(path, category.IconUrl.Replace('/', '\\'));
                        DeleteOldFile(oldFilePath);

                        var partPath = Path.Combine(DIRECTORY_PATH, categoryIcon.FileName);
                        category.IconUrl = partPath.Replace('\\', '/');
                    }
                }

                _db.Categories.Update(category);
                _db.SaveChanges();

                var categoryVM = new CategoryVM
                {
                    Id = category.Id,
                    Name = category.Name,
                    Url = category.Url,
                    ImageData = CreateFullImgUrl(category.ImageUrl),
                    IconData = CreateFullImgUrl(category.IconUrl),
                    FullUrl = category.FullUrl
                };

                return Ok(categoryVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryVM>> Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null) { return NotFound($"Категория с id={id} не найдена!"); }
            category.IsActive = false;

            _db.Categories.Update(category);
            _db.SaveChanges();

            string path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
            string oldImgPath = Path.Combine(path, category.ImageUrl.Replace('/', '\\'));
            string oldIconPath = Path.Combine(path, category.IconUrl.Replace('/', '\\'));
            DeleteOldFile(oldImgPath);
            DeleteOldFile(oldIconPath);

            var categoryVM = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Url = category.Url,
                ImageData = CreateFullImgUrl(category.ImageUrl),
                IconData = CreateFullImgUrl(category.IconUrl),
                FullUrl = category.FullUrl
            };

            //реализовать удалений всех элементов по категории

            return Ok(categoryVM);
        }


        /// <summary>
        /// Проверяет и удаляет файл по заданному пути
        /// </summary>
        private void DeleteOldFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        private string CreateFullImgUrl(string filePath)
        {
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host.Value;
            var locationImg = $"{scheme}://{host}/{filePath}";
            return locationImg;
        }
    }
}
