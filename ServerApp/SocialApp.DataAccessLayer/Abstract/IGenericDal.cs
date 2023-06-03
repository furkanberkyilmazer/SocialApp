using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {

        //IEnumerable(List), generic olamayan bir koleksiyon üzerinde iterasyon(liste içerisinde dönmemizi) yapmamızı sağlar.IEnumerable, generic olamayan bir koleksiyon üzerinde iterasyon(liste içerisinde dönmemizi) yapmamızı sağlar.

        //IQueryable, belli bir uzak veri kaynağından(web service, database…) verileri sorgulamak için işlevsellik sağlar.
        Task<T> GetByIdAsync(int id);


        // productRepository.Where(x=>x.id>5)  -> .ToListAsync() demeden araya orderby vs eklenebilir.
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //.Tolist demeden veritabanını gitmez özellikle IQueryable kullandım sorgu dondükten sonra işlem yapmak için


        IQueryable<T> GetAll();

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);


        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);  //update ve delete in asyn olmasına gerek yok zaten uzun süren bir işlem değil.

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
