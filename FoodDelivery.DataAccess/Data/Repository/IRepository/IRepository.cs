using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FoodDelivery.DataAccess.IRepository
{
    interface IRepository<T> where T : class
    {
        //Get Object Id
        T Get(int id);

        //Get all objects Ienumerable
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        //Get the first or default result
        T GetFirstorDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        //Add
        void Add(T entity);

        //Remove by Id
        void Remove(int id);

        //Remove the object itself
        void Remove(T entity);

        //Remove list of objects
        void RemoveRange(IEnumerable<T> entity);
    }
}
