using Backend.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Backend.Interfaces
{
    public interface IDocument
    {
		Task<List<Document>> GetAllDocuments();

		Task<Document?> GetDocumentById(string id);


		Task AddDocument(Document item);
	

		Task<bool> RemoveDocument(string id);


		Task<bool> UpdateDocument(string id, Document document);


		Task<List<Document>> RemoveAllDocument(List<Document> list);

	}
}
