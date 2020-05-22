using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
  public interface  IRepository<T>
    {
        IEnumerable<T> FindAll();
        T FindById(int id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Exists(int id);
    }
}
