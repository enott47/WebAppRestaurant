using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurantDemo.Repositories;
using WebAppRestaurantDemo.Models;
using WebAppRestaurantDemo.ViewModel;

namespace WebAppRestaurantDemo.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDBEntities objrestaurantDBEntities;
        public HomeController()
        {
            objrestaurantDBEntities = new RestaurantDBEntities();
        }


        // GET: Home
        public ActionResult Index()


        {
            CustomerRepository ObjCustomerRepository = new CustomerRepository();
            ItemRepository itemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();


            var objMyltipleModels =  new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (ObjCustomerRepository.GetAllCustomers() , itemRepository.GetAllItems(), objPaymentTypeRepository.GetAllPaymentType());


            return View(objMyltipleModels);
        }

        [HttpGet]
        public JsonResult getItemUnitPrice(int? itemId)
        {
            decimal Unitprice = objrestaurantDBEntities.Items.SingleOrDefault(model => model.ItemId == itemId).ItemPrice;
            return Json(Unitprice, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            objOrderRepository.AddOrder(objOrderViewModel);
            return Json(data: " Your Order has been Successfully Placed.", JsonRequestBehavior.AllowGet);
        }
    }


}