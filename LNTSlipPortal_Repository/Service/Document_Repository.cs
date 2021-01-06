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
    public class Document_Repository : IDocument_Repository, IDisposable
    {
        private LNTSlipPortalEntities context;
        public Document_Repository(LNTSlipPortalEntities _context)
        {
            context = _context;
        }
        public Document_Repository()
        {
            context = new LNTSlipPortalEntities();
        }
        public IQueryable<Document> GetAllDocuments()
        {
            try
            {
                return context.Documents.AsQueryable();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetAllDocuments,Repository");
                throw;
            }

        }

        public Document  GetDocumentByID(int DocumentId)
        {
            try
            {
                var objDocument=( from e in context.Documents
                                 where e.DocumentId == DocumentId
                                  select e).AsQueryable();
                return objDocument.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.SetLog("GetDocumentbyId,Repository");
                throw;
            }
        }

        public Document InsertDocument(Document objDocument)
        {
            try
            {
                context.Documents.Add(objDocument);
                context.SaveChanges();

                return objDocument;
            }
            catch (Exception ex)
            {
                ex.SetLog("InsertEmployee,Repository");
                throw;
            }
        }
        public Document UpdateDocument(Document objDocument)
        {
            try
            {
                context.Entry(objDocument).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return objDocument;
            }
            catch (Exception ex)
            {
                ex.SetLog("Update,Repository");
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
