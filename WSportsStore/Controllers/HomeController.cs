using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSportsStore.Models;
using WSportsStore.Models.ViewModels;

namespace WSportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;
        public HomeController(IStoreRepository repo) => repository = repo;

        //public IActionResult Index() => View(repository.Products);

        //public ViewResult Index(int productPage = 1)
        //     => View(repository.Products
        //     .OrderBy(p => p.ProductID)
        //     .Skip((productPage - 1) * PageSize)
        //     .Take(PageSize));

        //        public ViewResult Index(int productPage = 1)
        //            => View(new ProductsListViewModel
        //            {
        //                Products = repository.Products
        //            .OrderBy(p => p.ProductID)
        //            .Skip((productPage - 1) * PageSize)
        //            .Take(PageSize),
        //                PagingInfo = new PagingInfo
        //                {
        //                    CurrentPage = productPage,
        //                    ItemsPerPage = PageSize,
        //                    TotalItems = repository.Products.Count()
        //                }
        //});

        public ViewResult Index(string category, int productPage = 1)
                => View(new ProductsListViewModel
                {
                    Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = category == null ?
                            repository.Products.Count() :
                            repository.Products.Where(e =>
                            e.Category == category).Count()
                    },
                    CurrentCategory = category
                });


    }
}
