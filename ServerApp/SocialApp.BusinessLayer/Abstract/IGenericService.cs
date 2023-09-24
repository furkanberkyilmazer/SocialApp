using System.Linq.Expressions;

namespace SocialApp.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        //void TInsert(T t);
        //void TDelete(T t);
        //void TUpdate(T t);
        //List<T> TGetList();
        //T TGetByID(int id);

        Task<T> GetByIdAsync(int id);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);


        Task<IEnumerable<T>> GetAllAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);


        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
