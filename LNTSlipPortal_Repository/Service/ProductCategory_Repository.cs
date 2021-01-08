using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.ServiceContract;
using System.Data.Entity;
using LNTSlipPortal_Repository.Data;
using System.Data;
using System.Data.SqlClient;
using LNTSlipPortal_Repository.DataServices;

namespace LNTSlipPortal_Repository.Service
{
    public class ProductCategory_Repository : IProductCategory_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public ProductCategory_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public ProductCategory_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<ProductCategory> GetAllProductCategorys()
        {
            try
            {
                return context.ProductCategories.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllProductCategorys,Repository");
                throw;
            }

        }

        public ProductCategory  GetProductCategoryByID(int ProductCategoryId)
        {
            try
            {
                var objProductCategory=( from e in context.ProductCategories
                                 where e.ProductCatId == ProductCategoryId
                                  select e).AsQueryable();
                return objProductCategory.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetProductCategorybyId,Repository");
                throw;
            }
        }
       
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
