using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //generic constraint
    //class:referans tipi olablir.
    //IEntity:Kendisi ve implement olduğu sınıflar olabilir
    //new():soyut bir nesne olamaz, somut bir nesne olmalıdır.
    public interface IEntityRepository<T>where T:class,IEntity,new()
    {

        //Filtreleme işlemleri için Expression yapısını kullanırız
        List<T> GetAll(Expression<Func<T,bool>> filter=null);

        T Get(Expression<Func<T, bool>> filter);
        //-------------------------------------------
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
