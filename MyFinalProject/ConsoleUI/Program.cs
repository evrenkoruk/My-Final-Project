using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {

        //SOLID 
        //Open Closed Principle: Yeni bir özellik eklendi ve mevcut yapıdan hiçbir değiştirilmedi.
        //Örneğin EntityFramework eklendi ve Busines katmanında sadece bağlanılacak DataAccess sınıfı değiştirildi.

        static void Main(string[] args)
        {
             ProductTest();

             

        }

        private static void ProductTest()
        {
            //ProductManager productManager = new ProductManager(new EfProductDal());

            //var result = productManager.GetProductDetails();

            //if(result.Success == true)
            //{
            //    foreach (var item in result.Data)
            //    {
            //        Console.WriteLine(item.ProductName + "/" + item.CategoryName + " /" + item.ProductId);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}

          
        }
    }
}
