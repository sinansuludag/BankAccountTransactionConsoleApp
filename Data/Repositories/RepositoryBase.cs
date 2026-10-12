using Data.Interfaces;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
    {
        private List<T> _entities = new();
        public bool Add(T entity)
        {
            var ent = _entities.FirstOrDefault(x => x.Id == entity.Id);
            if (ent == null)
            {
                _entities.Add(entity);
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        { 
           var entity= _entities.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                _entities.Remove(entity);
                return true;
            }
           return false;
        }

        public List<T> GetAll()
        {
            return [.. _entities];
        }

        public T? GetById(int id)
        {
            return _entities.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(T entity)
        {
            var index = _entities.FindIndex(x => x.Id == entity.Id);

            if (index != -1)
            {
                _entities[index] = entity;
                return true;
            }

            return false;
        }
    }
}
