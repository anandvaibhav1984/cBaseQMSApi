using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace cBaseQms.Repository.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        void Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        void Delete(T entity);

        //DELETE BY ID
        void Delete(int Id);

        void Delete(Expression<Func<T, bool>> where);

        // Get an entity by int id
        T GetById(int id);

        // Get an entity using delegate
        T Get(Expression<Func<T, bool>> where);

        // Gets all entities of type T
        IEnumerable<T> GetAll();

        // Gets entities using delegate
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        void Update(T entity, params Expression<Func<T, object>>[] properties);

        /// <summary>
        /// Get Data From Database
        /// <para>Use it when to retive data through a stored procedure</para>
        /// </summary>
        IEnumerable<T> ExecuteQuery(string spQuery, object[] parameters);

        /// <summary>
        /// Get Single Data From Database
        /// <para>Use it when to retive single data through a stored procedure</para>
        /// </summary>
        T ExecuteQuerySingle(string spQuery, object[] parameters);

        /// <summary>
        /// Insert/Update/Delete Data To Database
        /// <para>Use it when to Insert/Update/Delete data through a stored procedure</para>
        /// </summary>
        int ExecuteCommand(string spQuery, object[] parameters);
    }
}