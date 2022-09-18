using CoreCRUD.Infrastructure.EntityTypeConfiguration.Context;
using CoreCRUD.Infrastructure.EntityTypeConfiguration.Repositories.Interface;
using CoreCRUD.Models.Entities.Abstract;
using CoreCRUD.Models.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoreCRUD.Infrastructure.EntityTypeConfiguration.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Category entity)
        {
            entity.DeleteDate = DateTime.Now;
            entity.Status = Status.Passive;
            _context.SaveChanges();

            //destroy
            //_context.Remove(entity);

        }

        public Category GetByDefault(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.FirstOrDefault(expression);
        }

        public List<Category> GetByDefaults(Expression<Func<Category, bool>> expression)
        {
            return _context.Categories.Where(expression).ToList();
        }

        public void Update(Category entity)
        {
            entity.Status = Status.Modified;
            entity.UpdateDate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
