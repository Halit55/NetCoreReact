using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Repository.Abstract;
using Users.Entities.Entities.Abstract;

namespace Users.Core.Repository.Concreate
{
    public class AdoRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class, new() where TContext : DbContext, new()
    {
        //connectionstring DataAccessLayer dan gönderilmeli
        SqlConnection connection = new SqlConnection("Server=.\\SQLEXPRESS;Database=Users;Trusted_Connection=True;MultipleActiveResultSets=true");

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();
            var tablename = typeof(TEntity).Name;

            if (tablename.EndsWith("y"))
            {
                tablename = tablename.Remove(tablename.Length - 1, 1) + "ies";
            }
            else
            {
                tablename = tablename + "s";
            }

            string column = string.Empty;
            string value = string.Empty;

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object key = propertyInfo.GetCustomAttributes(true).Length;
                if (Convert.ToInt32(key) == 0 && propertyInfo.GetValue(entity, null) != null)
                {
                    column += propertyInfo.Name + ",";
                    value += "'" + propertyInfo.GetValue(entity, null).ToString() + "',";
                }
            }

            column = column.Substring(0, column.Length - 1);
            value = value.Substring(0, value.Length - 1);
            string query = @"INSERT INTO " + tablename + " (" + column + ") VALUES (" + value + ")";
            using (var command = connection.CreateCommand())
            {
                await connection.OpenAsync();
                command.CommandText = query;
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
                return entity;
            }
        }
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();
            var tablename = typeof(TEntity).Name;
            if (tablename.EndsWith("y"))
            {
                tablename = tablename.Remove(tablename.Length - 1, 1) + "ies";
            }
            else
            {
                tablename = tablename + "s";
            }
            string keyvalue = string.Empty;
            string key = string.Empty;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object _key = propertyInfo.GetCustomAttributes(true).Length;
                if (Convert.ToInt32(_key) > 0)
                {
                    key += propertyInfo.Name;
                    keyvalue += propertyInfo.GetValue(entity, null).ToString();
                }
            }

            string query = @"DELETE FROM " + tablename + " WHERE " + key + "=" + keyvalue;

            using (var command = connection.CreateCommand())
            {
                await connection.OpenAsync();
                command.CommandText = query;
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
                return entity;
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();
            var tablename = typeof(TEntity).Name;
            if (tablename.EndsWith("y"))
            {
                tablename = tablename.Remove(tablename.Length - 1, 1) + "ies";
            }
            else
            {
                tablename = tablename + "s";
            }

            string column = string.Empty;
            string value = string.Empty;
            string key = string.Empty;
            string keyvalue = string.Empty;

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                column += propertyInfo.Name + ",";
                value += "'" + column.GetType().GetProperty(column).GetValue(entity, null) + "',";
                object _key = propertyInfo.GetCustomAttributes(true).Length;
                if (Convert.ToInt32(_key) > 0)
                {
                    key += propertyInfo.Name;
                    keyvalue += propertyInfo.GetValue(entity, null).ToString();
                }
            }

            string query = @"UPDATE " + tablename + " SET (" + column + ") VALUES (" + value + ") WHERE " + key + " = " + keyvalue + '"';

            using (var command = connection.CreateCommand())
            {
                await connection.OpenAsync();
                command.CommandText = query;
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
                return entity;
            }
        }
        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();
            var tablename = typeof(TEntity).Name;

            if (tablename.EndsWith("y"))
            {
                tablename = tablename.Remove(tablename.Length - 1, 1) + "ies";
            }
            else
            {
                tablename = tablename + "s";
            }

            string query = string.Empty;

            string key = string.Empty;
            string keyvalue = string.Empty;
            TEntity entity = new TEntity();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object _key = propertyInfo.GetCustomAttributes(true).Length;
                if (Convert.ToInt32(_key) > 0)
                {
                    key += propertyInfo.Name;
                    keyvalue += propertyInfo.GetValue(entity, null).ToString();
                }
            }

            query = @"SELECT * FROM " + tablename + " WHERE " + key + "=" + keyvalue;


            using (var command = connection.CreateCommand())
            {
                await connection.OpenAsync();
                command.CommandText = query;
                SqlDataReader sdr = await command.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    int i = 0;
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        propertyInfo.SetValue(entity, sdr[i]);
                        i++;
                    }
                }
                await sdr.CloseAsync();
                await connection.CloseAsync();
                return entity;
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();
            var tablename = typeof(TEntity).Name;
            if (tablename.EndsWith("y"))
            {
                tablename = tablename.Remove(tablename.Length - 1, 1) + "ies";
            }
            else
            {
                tablename = tablename + "s";
            }

            string query = string.Empty;
            string column = string.Empty;
            string value = string.Empty;

            if (filter == null)
            {
                query = @"SELECT * FROM " + tablename;
            }
            else
            {
                query = @"SELECT * FROM " + tablename + " WHERE " + filter + "=" + filter;
            }

            List<TEntity> entity = new List<TEntity>();

            using (var command = connection.CreateCommand())
            {
                await connection.OpenAsync();
                command.CommandText = query;
                SqlDataReader sdr = await command.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    int i = 0;
                    TEntity _entity = new TEntity();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        propertyInfo.SetValue(_entity, sdr[i]);
                        i++;
                    }
                    entity.Add(_entity);
                }
                await sdr.CloseAsync();
                await connection.CloseAsync();
                return entity;
            }
        }

        //Daha sonra tamamlanacak
        public Task<bool> AddRangeAsync(IEnumerable<TEntity> entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entity)
        {
            throw new NotImplementedException();
        }

    }

}
