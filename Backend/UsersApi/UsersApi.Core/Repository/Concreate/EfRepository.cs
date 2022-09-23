using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Users.Core.Repository.Concreate
{
    public class EfRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class, new() where TContext : DbContext, new()
    {
        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                //AsNoTrackingWithIdentityResoluti() change tracker takip mekanizmasını devre dışı bıraktığı için performans arttırır. 
                return await context.Set<TEntity>().AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(filter);
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                if (filter == null)
                {
                    return await context.Set<TEntity>().AsNoTrackingWithIdentityResolution().ToListAsync();
                }
                else
                {
                    return await context.Set<TEntity>().Where(filter).AsNoTrackingWithIdentityResolution().ToListAsync();
                }
            }
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Remove(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            //Hatalı eksik update yapıyor
            //using (var context = new TContext())
            //{
            //    context.Update(entity);                
            //    await context.SaveChangesAsync();
            //}
            //return entity;

            using (var context = new TContext())
            {
                var uptadedEntity = context.Entry(entity);
                uptadedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
            
        }
        public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entity)
        {
            using (var context = new TContext())
            {
                Boolean response = false;
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await context.AddRangeAsync(entity);
                        response = true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        response = false;
                    }
                    finally
                    {
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                }
                return response;
            }
        }
        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entity)
        {
            using (var context = new TContext())
            {
                Boolean response = false;
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        context.RemoveRange(entity);
                        response = true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        response = false;
                    }
                    finally
                    {
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                }
                return response;
            }
        }
        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entity)
        {
            using (var context = new TContext())
            {
                Boolean response = false;
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        context.UpdateRange(entity);
                        response = true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        response = false;
                    }
                    finally
                    {
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                }
                return response;
            }
        }
    }
}
