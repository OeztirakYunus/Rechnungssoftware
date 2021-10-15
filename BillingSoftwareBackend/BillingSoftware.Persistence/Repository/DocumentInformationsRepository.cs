using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class DocumentInformationsRepository : Repository<DocumentInformations>, IDocumentInformationsRepository
    {
        public DocumentInformationsRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public Task<DocumentInformations[]> GetAllAsync()
        {
            return _context.DocumentInformations.ToArrayAsync();
        }
    }
}
