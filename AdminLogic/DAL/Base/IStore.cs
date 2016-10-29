using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace AdminLogic.DAL
{
    public interface IStore<T>
    {
        int Update(T model);

        int Delete(T model);

        int Insert(T model);

        IPagedList<T> ListAll(int currentPage = 1, int pageSize = 20, string orderBy = null, ListSortDirection sortDirection = ListSortDirection.Descending);
    }
}
