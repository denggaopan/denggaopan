using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Denggaopan.OpenApi
{
    public class PagingList<T>
    {
        /// <summary>
        /// 赋值当前页面数据数组
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        public PagingList(List<T> entities, int pageNumber, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            TotalPages = (totalCount + pageSize - 1) / pageSize;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Entities = entities;
        }

        /// <summary>
        /// 将源数据数组分页
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        public PagingList(List<T> source, int pageNumber, int pageSize)
        {
            var total = source.Count();
            TotalCount = total;
            TotalPages = (total + pageSize - 1) / pageSize;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Entities = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }


        public List<T> Entities { get; }
        /// <summary>
        /// 页码（从1开始）
        /// </summary>
        public int PageNumber { get; }
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; }
        /// <summary>
        /// Total count
        /// </summary>
        public int TotalCount { get; }
        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; }

        public List<int> CurrentPages
        {
            get
            {
                var arr = new List<int>();
                var n = PageNumber / PageSize;
                for (var i = n + 1; i <= n + 10; i++)
                {
                    if (i <= TotalPages)
                    {
                        arr.Add(i);
                    }
                    else
                    {
                        break;
                    }
                }
                return arr;
            }
        }
        /// <summary>
        /// Has previous page
        /// </summary>
        public bool HasPreviousPage
        {
            get { return (PageNumber > 1); }
        }
        /// <summary>
        /// Has next page
        /// </summary>
        public bool HasNextPage
        {
            get { return (PageNumber < TotalPages); }
        }
    }
}
