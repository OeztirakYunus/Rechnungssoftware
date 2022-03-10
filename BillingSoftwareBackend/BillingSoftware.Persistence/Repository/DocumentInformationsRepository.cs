using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class DocumentInformationsRepository : Repository<DocumentInformations>, IDocumentInformationsRepository
    {
        public DocumentInformationsRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public async Task<DocumentInformations[]> GetAllAsync()
        {
            return await _context.DocumentInformations
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<DocumentInformations> GetByIdAsync(Guid id)
        {
            return await _context.DocumentInformations
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
