using CoreCRUD.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreCRUD.Infrastructure.EntityTypeConfiguration.Repositories.Interface
{
    public interface ICategoryRepository
    {
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);

        //read
        //....GetByDefault( x => x.Name == StartsWith("F") ) ;
        Category GetByDefault(Expression<Func<Category, bool>> expression);
        List<Category> GetByDefaults(Expression<Func<Category, bool>> expression);
    }
}
