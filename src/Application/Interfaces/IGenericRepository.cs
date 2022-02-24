﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepository<T>: IDisposable where T : class
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task Update(T entity);
        Task Remove(T entity);
        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();
    }
}
