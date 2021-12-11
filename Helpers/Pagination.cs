using Microsoft.AspNetCore.Mvc;
using WeGout.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace WeGout.Helpers
{
    public class Paging<T>
    {
        public List<T> PagingData { get; set; } = new List<T>();
        public int TotalItemCount { get; set; } = 0;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
    public class Paging
    {
        public int TotalItemCount { get; set; } = 0;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Paging(bool isAll)
        {
            if (isAll)
                this.PageSize = 0;
        }
        public Paging()
        {

        }


    }

    public static class ToPaging
    {
        /// <summary>
        /// IQueryable tipindeki LINQ listelerini, verilen parametrelere göre sıralayıp, önceki kayıtları atlayıp, istenilen kadar kaydı alıp cevap döner.
        /// PageSize olarak 0 paging'i devre dış bırakıp tüm listeyi döner.
        /// ÖNEMLİ: TotalItemCount parametresinin 0 verilmesi durumda, bu değer veritabanından sorgulanıp doldurulur. 
        ///         0'dan büyük olması durumunda ise sorgulanmaz. DataGrid vb. nesnelerde bunun kullanımında, ilk sorgudan sonra dolu göndererek optimizasyon yapınız.
        /// ÖNEMLİ: Bu methodun cevap döndüğü veri IQueryable tipinde değil, List tipindedir. Bu yüzden cevap üzerinden tekrar kriter yazarak sorgulama yapılması halinde veritabanından değil, ram üzerinden sorgulama yapılmış olur.
        /// </summary>
        public static async Task<Paging<U>> ToPagingAsync<T, U>(this IQueryable<T> source, Paging pagingParameters, IMapper _mapper)
        {
            Paging<U> result = new Paging<U>();



            if (pagingParameters.PageSize == 0)
            {
                result.PagingData = (await source.ToListAsync()).Select(l => _mapper.Map<U>(l)).ToList();
                result.PageIndex = 0;
                result.PageSize = result.PagingData.Count;
                result.TotalItemCount = result.PagingData.Count;
            }
            else
            {
                if (pagingParameters.TotalItemCount == 0)
                {
                    pagingParameters.TotalItemCount = source.Count();
                }

                if (pagingParameters.PageIndex > 0)
                {
                    source = source.Skip(pagingParameters.PageIndex * pagingParameters.PageSize);
                }
                try
                {
                    result.PagingData = (await source.Take(pagingParameters.PageSize)
                                        .ToListAsync()).Select(l => _mapper.Map<U>(l)).ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
                result.PageIndex = pagingParameters.PageIndex;
                result.PageSize = pagingParameters.PageSize;
                result.TotalItemCount = pagingParameters.TotalItemCount;

            }

            return result;
        }
        /// <summary>
        /// IQueryable tipindeki LINQ listelerini, verilen parametrelere göre sıralayıp, önceki kayıtları atlayıp, istenilen kadar kaydı alıp cevap döner.
        /// PageSize olarak 0 paging'i devre dış bırakıp tüm listeyi döner.
        /// ÖNEMLİ: TotalItemCount parametresinin 0 verilmesi durumda, bu değer veritabanından sorgulanıp doldurulur. 
        ///         0'dan büyük olması durumunda ise sorgulanmaz. DataGrid vb. nesnelerde bunun kullanımında, ilk sorgudan sonra dolu göndererek optimizasyon yapınız.
        /// ÖNEMLİ: Bu methodun cevap döndüğü veri IQueryable tipinde değil, List tipindedir. Bu yüzden cevap üzerinden tekrar kriter yazarak sorgulama yapılması halinde veritabanından değil, ram üzerinden sorgulama yapılmış olur.
        /// </summary>
        public static async Task<Paging<T>> ToPagingAsync<T>(this IQueryable<T> source, Paging pagingParameters)
        {
            Paging<T> result = new Paging<T>();



            if (pagingParameters.PageSize == 0)
            {
                result.PagingData = await source.ToListAsync();
                result.PageIndex = 0;
                result.PageSize = result.PagingData.Count;
                result.TotalItemCount = result.PagingData.Count;
            }
            else
            {
                if (pagingParameters.TotalItemCount == 0)
                {
                    pagingParameters.TotalItemCount = source.Count();
                }

                if (pagingParameters.PageIndex > 0)
                {
                    source = source.Skip(pagingParameters.PageIndex * pagingParameters.PageSize);
                }
                try
                {
                    result.PagingData = await source.Take(pagingParameters.PageSize)
                                        .ToListAsync();
                }
                catch (Exception e)
                {
                    throw e;
                }
                result.PageIndex = pagingParameters.PageIndex;
                result.PageSize = pagingParameters.PageSize;
                result.TotalItemCount = pagingParameters.TotalItemCount;

            }

            return result;
        }

    }


}
