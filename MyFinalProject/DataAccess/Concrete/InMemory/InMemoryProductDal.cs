using DataAccess.Abstarct;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete
{
    public class InMemoryProductDal : IProductDal
    {

        List<Product> _products;

        public InMemoryProductDal()
        {
            //Without Oracle,Sql Server ,MongoDb,Postgres 

            var barcodes = System.IO.File.ReadAllLines(@"C:\Users\NurselAltın\Desktop\Nursel\projects\project\MyFinalProject\DataAccess\Utilities\NoktaUpdatedPicture.txt");
            _products = barcodes.Select(s => new Product
            {
                ProductName = s.Split(':')[0].Trim(),
             
            }).ToList();


        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            _products.Remove(_products.SingleOrDefault(p => p.ProductId == product.ProductId));
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryıd)
        {
            return _products.Where(x => x.CategoryId == categoryıd).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product pUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            pUpdate.ProductName = product.ProductName;
            pUpdate.CategoryId = product.CategoryId;
            pUpdate.UnitPrice = product.UnitPrice;
            pUpdate.UnitsInStock = product.UnitsInStock;

        }
    }
}
