﻿using System.Web.Mvc;
using MrCMS.Web.Apps.Ecommerce.Entities.Shipping;
using MrCMS.Web.Apps.Ecommerce.Services.Shipping;
using MrCMS.Web.Apps.Ecommerce.Services.Tax;
using MrCMS.Website.Controllers;
using MrCMS.Helpers;

namespace MrCMS.Web.Apps.Ecommerce.Areas.Admin.Controllers
{
    public class ShippingMethodController : MrCMSAppAdminController<EcommerceApp>
    {
        private readonly IShippingMethodManager _shippingMethodManager;
        private readonly ITaxRateManager _taxRateManager;

        public ShippingMethodController(IShippingMethodManager shippingMethodManager, ITaxRateManager taxRateManager)
        {
            _shippingMethodManager = shippingMethodManager;
            _taxRateManager = taxRateManager;
        }

        public ViewResult Index()
        {
            var options = _shippingMethodManager.GetAll();
            return View(options);
        }

        [HttpGet]
        public PartialViewResult Add()
        {
            ViewData["tax-rates"] = _taxRateManager.GetAll()
                                                   .BuildSelectItemList(rate => rate.Name,
                                                                        rate => rate.Id.ToString(),
                                                                        emptyItem: null);
            return PartialView();
        }

        [HttpPost]
        public RedirectToRouteResult Add(ShippingMethod option)
        {
            _shippingMethodManager.Add(option);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Edit(ShippingMethod option)
        {
            ViewData["tax-rates"] = _taxRateManager.GetAll()
                                                   .BuildSelectItemList(rate => rate.Name,
                                                                        rate => rate.Id.ToString(),
                                                                        rate => rate == option.TaxRate,
                                                                        emptyItem: null);
            return PartialView(option);
        }

        [ActionName("Edit")]
        [HttpPost]
        public RedirectToRouteResult Edit_POST(ShippingMethod option)
        {
            _shippingMethodManager.Update(option);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Delete(ShippingMethod option)
        {
            return PartialView(option);
        }

        [ActionName("Delete")]
        [HttpPost]
        public RedirectToRouteResult Delete_POST(ShippingMethod option)
        {
            _shippingMethodManager.Delete(option);
            return RedirectToAction("Index");
        }
    }
}