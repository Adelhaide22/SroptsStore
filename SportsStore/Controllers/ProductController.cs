using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int productPage = 1)
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

        public ViewResult SortedList(string category, string order, int productPage = 1)
        {
            var products = repository.Products
                  .Where(p => category == null || p.Category == category);
            if (order == "asc")
            {
                products = products.OrderBy(p => p.Price);
            }
            if (order == "desc")
            {
                products = products.OrderByDescending(p => p.Price);
            }
            products = products.Skip((productPage - 1) * PageSize)
                  .Take(PageSize);

            return View("List", new ProductsListViewModel
            {
                Products = products,
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

        public ViewResult ListInRange(string category, decimal lowerBound, decimal upperBound, int productPage = 1)
            => View("List", new ProductsListViewModel
            {
                Products = repository.GetFromRange(new Range() { LowerBound = lowerBound, UpperBound = upperBound})
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
