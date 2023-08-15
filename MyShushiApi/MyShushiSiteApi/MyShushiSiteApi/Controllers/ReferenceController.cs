using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShushiSiteApi.DbModels;
using MyShushiSiteApi.ViewModels;

namespace MyShushiSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {
        private MyDbContext _db;
        private IWebHostEnvironment _hostingEnvironment;

        public ReferenceController(MyDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _hostingEnvironment = environment;
        }

        /// <summary>
        /// Возвращает список способов оплаты
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KeyValueViewModel>>> GetAllPaymentMethod()
        {
            try
            {
                var payments = _db.Payments.ToList();
                var viewModels = payments.Select(p => new KeyValueViewModel
                {
                    Key = p.Id,
                    Value = p.Name
                });

                return Ok(viewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Возвращает список способов доставки
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KeyValueViewModel>>> GetAllDeliveryMethod()
        {
            try
            {
                var delivery = _db.DeliveryMethods.ToList();
                var viewModels = delivery.Select(d => new KeyValueViewModel
                {
                    Key = d.Id,
                    Value = d.Name
                });

                return Ok(viewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Возвращает список статусов заказов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KeyValueViewModel>>> GetAllOderStatuses()
        {
            try
            {
                var statuses = _db.OrderStatuses.ToList();
                var viewModels = statuses.Select(s => new KeyValueViewModel
                {
                    Key = s.Id,
                    Value = s.Name
                });

                return Ok(viewModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
        }
    }
}
