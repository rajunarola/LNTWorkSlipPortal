using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LNTSlipPortal_Repository.Data;

namespace LNTSlipPortal_Repository.ServiceContract
{
    public interface IDocument_Repository : IDisposable
    {
        IQueryable<Document> GetAllDocuments();
        Document GetDocumentByID(int DocumentId);
        Document InsertDocument(Document objDocument);
        Document UpdateDocument(Document objDocument);

    }
}
