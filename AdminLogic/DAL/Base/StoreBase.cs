using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using AdminLogic.DAL;
using X.PagedList;

namespace AdminLogic
{
    public abstract class StoreBase<T> : IStore<T>
    {

        private readonly Lazy<string[]> _pocoPropNames;
        private string[] PocoPropNames => _pocoPropNames.Value;

        public bool IsValidColumnName(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                return false;

            columnName = columnName.Trim();
            var result = PocoPropNames.Any(a => StringComparer.OrdinalIgnoreCase.Equals((string) a, columnName));
            return result;
        }

        protected StoreBase()
        {
            _pocoPropNames = new Lazy<string[]>(() => Util.GeneralExtensions.GetFirstGenericTypePropNames(this.GetType()));
        }

        public DbConnection GetOpenDbConnection()
        {
            var con = new SqlConnection(GetDbConnectionString());
            con.Open();
            return con;
        }

        protected virtual string GetDbConnectionString()
        {
            var config = ConfigurationManager.ConnectionStrings["Uploader"];
            return config.ConnectionString;
        }

        public abstract int Update(T model);
        public abstract int Delete(T model);
        public abstract int Insert(T model);
        public abstract IPagedList<T> ListAll(int currentPage = 1, int pageSize = 20, string orderBy = null, ListSortDirection sortDirection = ListSortDirection.Descending);
    }
}