using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Exceptions;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BillingSoftware.Persistence.Repository
{
    public class DocumentInformationsRepository : Repository<DocumentInformations>, IDocumentInformationsRepository
    {
        public DocumentInformationsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddPosition(Guid documentInformationId, Position position)
        {
            var documentInformation = await GetByIdAsync(documentInformationId);
            if (documentInformation == null)
            {
                throw new EntityNotFoundException("Document information does not exist.");
            }

            var tempPosition = await _context.Positions.FindAsync(position.Id);
            if (tempPosition == null)
            {
                tempPosition.DocumentInformationId = documentInformationId;
                await _context.Positions.AddAsync(tempPosition);
            }
        }

        public async Task AddPositions(Guid documentInformationId, ICollection<Position> positions)
        {
            foreach (var item in positions)
            {
                await AddPosition(documentInformationId, item);
            }
        }

        public async Task DeletePosition(Guid documentInformationId, Guid positionId)
        {
            var documentInformation = await GetByIdAsync(documentInformationId);
            if (documentInformation == null)
            {
                throw new EntityNotFoundException("Document information does not exist.");
            }

            var tempPosition = await _context.Positions.FindAsync(positionId);
            if (tempPosition == null)
            {
                throw new EntityNotFoundException("Position does not exist.");
            }

            _context.Positions.Remove(tempPosition);
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
