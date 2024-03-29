﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.CSS;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstarct;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependecyResolves.Autofac
{


    //Startup kısmında yaptığımız çözümleme ortamını ayarlıyoruz burada
    public class AutofacBusinessModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
           
            builder.RegisterType<ProductManager>().As<IProductService>(); //Karşılığı :   services.AddSingleton<IProductService,ProductManager>();
            //Birisi senden IProductDal istediğinde EfProductDal new();
            builder.RegisterType<EfProductDal>().As<IProductDal>();
            builder.RegisterType<FileLogger>().As<ILogger>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();



            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }



    }
    
}
