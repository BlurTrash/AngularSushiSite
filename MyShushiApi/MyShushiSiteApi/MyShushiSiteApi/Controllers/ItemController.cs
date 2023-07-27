using Microsoft.AspNetCore.Mvc;
using MyShushiSiteApi.DbModels;
using MyShushiSiteApi.ViewModels;
using System.IO;
using System.Linq;

namespace MyShushiSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : Controller
    {
        const string DIRECTORY_PATH = @"FileStorage\ItemsImg\";
        private MyDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;

        public ItemController(MyDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _hostingEnvironment = environment;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemVM>> Get(int id)
        {
            var item = _db.Items.FirstOrDefault(i => i.IsActive == true && i.Id == id);

            if (item == null)
            {
                return NotFound($"Товар не найден!");
            }

            var itemVM =  new ItemVM()
            {
                Id = item.Id,
                Name = item.Name,
                UrlName = item.UrlName,
                ImageUrl = CreateFullImgUrl(item.ImageUrl),
                CategoryId = (int)item.CategoryId,
                Desription = item.Desription,
                Ingredients = item.Ingredients,
                Price = item.Price,
                Weight = item.Weight,
                Count = item.Count,
                FullUrl = item.FullUrl
            };

            return Ok(itemVM);
        }

        [HttpGet("{countNewestItems}")]
        public async Task<ActionResult<IEnumerable<ItemVM>>> GetNewestItems(int countNewestItems)
        {
            var newestItems = _db.Items.OrderByDescending(i => i.CreateDate).Take(countNewestItems).ToList();

            var viewModelItems = newestItems.AsEnumerable().Select(c =>
            {
                return new ItemVM()
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlName = c.UrlName,
                    ImageUrl = CreateFullImgUrl(c.ImageUrl),
                    CategoryId = (int)c.CategoryId,
                    Desription = c.Desription,
                    Ingredients = c.Ingredients,
                    Price = c.Price,
                    Weight = c.Weight,
                    Count = c.Count,
                    FullUrl = c.FullUrl
                };
            }).ToList();

            return Ok(viewModelItems);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<IEnumerable<ItemVM>>> GetItemsByCategoryId(int categoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);
            var items = _db.Items.Where(i => i.IsActive == true && i.CategoryId == categoryId).ToList();

            if (category == null)
            {
                return NotFound($"Категория не найдена!");
            }

            var viewModelItems = items.AsEnumerable().Select(c =>
            {
                //var scheme = HttpContext.Request.Scheme;
                //var host = HttpContext.Request.Host.Value;
                //var locationImg = $"{scheme}://{host}/{c.ImageUrl}";

                return new ItemVM()
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlName = c.UrlName,
                    ImageUrl = CreateFullImgUrl(c.ImageUrl),
                    CategoryId = (int)c.CategoryId,
                    Desription = c.Desription,
                    Ingredients = c.Ingredients,
                    Price = c.Price,
                    Weight = c.Weight,
                    Count = c.Count,
                    FullUrl = c.FullUrl
                };
            }).ToList();

            return Ok(viewModelItems);
        }


        /// <summary>
        /// Создает: Новый элемент меню по выбранной категории
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ItemVM>> Post([FromForm] ItemViewModel itemViewModel)
        {
            try
            {
                string path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
                string uploads = Path.Combine(path, DIRECTORY_PATH);

                var category = _db.Categories.FirstOrDefault(c => c.Id == itemViewModel.CategoryId);
                if (category == null)
                {
                    return NotFound($"Категория не найдена!");
                }

                //string fullUrl = category.FullUrl + "/" + itemViewModel.UrlName;

                var item = new Item
                {
                    Id = itemViewModel.Id,
                    CategoryId = itemViewModel.CategoryId,
                    Desription = itemViewModel.Desription,
                    Ingredients = itemViewModel.Ingredients,
                    Name = itemViewModel.Name,
                    UrlName = itemViewModel.UrlName,
                    Price = itemViewModel.Price,
                    Weight = itemViewModel.Weight,
                    Count = itemViewModel.Count,
                    IsActive = true
                    //FullUrl = fullUrl
                };

                var itemImg = itemViewModel.ImageData;

                string[] validFormat = new string[4] { "jpg", "png", "jpeg", "webp" };

                if (itemImg != null)
                {
                    string imgPath = Path.Combine(uploads, itemImg.FileName);
                    var fileImgFormat = itemImg.FileName.Split(".").Last().ToLower();
                    if (!validFormat.Contains(fileImgFormat))
                    {
                        return BadRequest($"Неверный формат файла '{itemImg.FileName}'. Допустимые форматы: .jpg, .png, .jpeg .webp!");
                    }
                    if (System.IO.File.Exists(imgPath))
                    {
                        return BadRequest($"Файл с именем '{itemImg.FileName}' уже существует на сервере!");
                    }

                    if (itemImg.Length > 0)
                    {
                        using (Stream fileStream = new FileStream(imgPath, FileMode.Create))
                        {
                            await itemImg.CopyToAsync(fileStream);
                        }

                        var partPath = Path.Combine(DIRECTORY_PATH, itemImg.FileName);

                        item.ImageUrl = partPath.Replace('\\', '/');
                    }
                }
                else return BadRequest($"Изображение не выбрано!");

                _db.Items.Add(item);
                _db.SaveChanges();

                string fullUrl = category.FullUrl + "/" + item.Id.ToString();

                item.FullUrl = fullUrl;

                _db.Items.Update(item);
                _db.SaveChanges();

                var itemVM = new ItemVM
                {
                    Id = item.Id,
                    CategoryId = (int)item.CategoryId,
                    Desription = item.Desription,
                    Ingredients = item.Ingredients,
                    Name = item.Name,
                    UrlName = item.UrlName,
                    Price = item.Price,
                    Weight = item.Weight,
                    Count = item.Count,
                    ImageUrl = CreateFullImgUrl(item.ImageUrl),
                    FullUrl = item.FullUrl
                };

                return Ok(itemVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Обновляет продукт 
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<ItemVM>> Put([FromForm] ItemViewModel itemViewModel)
        {
            try
            {
                string path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
                string uploads = Path.Combine(path, DIRECTORY_PATH);

                var category = _db.Categories.FirstOrDefault(c => c.Id == itemViewModel.CategoryId);
                if (category == null)
                {
                    return NotFound($"Категория не найдена!");
                }
                string fullUrl = category.FullUrl + "/" + itemViewModel.UrlName;

                var item = _db.Items.FirstOrDefault(c => c.Id == itemViewModel.Id);
                if (item == null) { return NotFound($"Продукт с id={itemViewModel.Id} не найден!"); }

                item.Name = itemViewModel.Name;
                item.Desription = itemViewModel.Desription;
                item.Ingredients = itemViewModel.Ingredients;
                item.Weight = itemViewModel.Weight;
                item.Count = itemViewModel.Count;
                item.Price = itemViewModel.Price;
                item.UrlName = itemViewModel.UrlName;
                item.FullUrl = fullUrl;

                var itemImg = itemViewModel.ImageData;

                string[] validFormat = new string[4] { "jpg", "png", "jpeg", "webp" };

                if (itemImg != null)
                {
                    string imgPath = Path.Combine(uploads, itemImg.FileName);
                    var fileImgFormat = itemImg.FileName.Split(".").Last().ToLower();
                    if (!validFormat.Contains(fileImgFormat))
                    {
                        return BadRequest($"Неверный формат файла '{itemImg.FileName}'. Допустимые форматы: .jpg, .png, .jpeg .webp!");
                    }
                    if (System.IO.File.Exists(imgPath))
                    {
                        return BadRequest($"Файл с именем '{itemImg.FileName}' уже существует на сервере!");
                    }

                    if (itemImg.Length > 0)
                    {
                        using (Stream fileStream = new FileStream(imgPath, FileMode.Create))
                        {
                            await itemImg.CopyToAsync(fileStream);
                        }

                        string oldFilePath = Path.Combine(path, item.ImageUrl.Replace('/', '\\'));
                        DeleteOldFile(oldFilePath);

                        var partPath = Path.Combine(DIRECTORY_PATH, itemImg.FileName);
                        item.ImageUrl = partPath.Replace('\\', '/');
                    }
                }

                _db.Items.Update(item);
                _db.SaveChanges();

                var itemVM = new ItemVM
                {
                    Id = item.Id,
                    CategoryId = (int)item.CategoryId,
                    Desription = item.Desription,
                    Ingredients = item.Ingredients,
                    Name = item.Name,
                    UrlName = item.UrlName,
                    Price = item.Price,
                    Weight = item.Weight,
                    Count = item.Count,
                    ImageUrl = CreateFullImgUrl(item.ImageUrl),
                    FullUrl = item.FullUrl
                };

                return Ok(itemVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemVM>> Delete(int id)
        {
            try
            {
                var item = _db.Items.FirstOrDefault(c => c.Id == id);
                if (item == null) { return NotFound($"Продукт с id={id} не найден!"); }
                item.IsActive = false;

                _db.Items.Update(item);
                _db.SaveChanges();

                string path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
                string oldFilePath = Path.Combine(path, item.ImageUrl.Replace('/', '\\'));
                DeleteOldFile(oldFilePath);

                var itemVM = new ItemVM
                {
                    Id = item.Id,
                    CategoryId = (int)item.CategoryId,
                    Desription = item.Desription,
                    Ingredients = item.Ingredients,
                    Name = item.Name,
                    UrlName = item.UrlName,
                    Price = item.Price,
                    Weight = item.Weight,
                    Count = item.Count,
                    ImageUrl = CreateFullImgUrl(item.ImageUrl),
                    FullUrl = item.FullUrl
                };

                return Ok(itemVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
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
