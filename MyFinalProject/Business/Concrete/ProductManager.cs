﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.CSS;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstarct;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        IProductDal _productDal;
        ICategoryService _categoryService;
     

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
           
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            IResult result = BusinessRules.Run(
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),
                CheckIfCategoryLimitExceed()
                );

            if(result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        
        }

        public IDataResult<List<Product>> GetAll()
        {

            if(DateTime.Now.Hour == 22.00)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new DataResult<List<Product>>(_productDal.GetAll(),true,Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get( p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(x => x.UnitPrice >= min &&  x.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());

        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
         
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {

            var result = _productDal.GetAll(x => x.CategoryId == categoryId).Count;

            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);

            }
            return new SuccessResult();

        }

        private IResult CheckIfProductNameExists(string productName)
        {

            var result = _productDal.GetAll(p => p.ProductName == productName).Any();//Any : Var mı?

            if(result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceed()
        {
            var result = _categoryService.GetAll();

            if(result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceed);
            }

            return new SuccessResult();

       
        }


    }
}
