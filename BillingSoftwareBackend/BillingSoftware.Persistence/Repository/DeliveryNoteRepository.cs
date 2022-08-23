using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class DeliveryNoteRepository : Repository<DeliveryNote>, IDeliveryNoteRepository
    {
        public DeliveryNoteRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public async Task<DeliveryNote[]> GetAllAsync()
        {
            return await _context.DeliveryNotes
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<DeliveryNote> GetByIdAsync(Guid id)
        {
            return await _context.DeliveryNotes
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }

        public override async Task Update(DeliveryNote entity)
        {
            var docInfoRep = new Repository<DocumentInformations>(_context);
            var positionRepo = new Repository<Position>(_context);

            var docInfo = await docInfoRep.GetByIdAsync(entity.DocumentInformationsId);
            if (docInfo != null)
            {
                entity.DocumentInformations.CopyProperties(docInfo);

                var positions = new List<Position>();
                foreach (var item in docInfo.Positions)
                {
                    var position = await positionRepo.GetByIdAsync(item.Id);
                    if (position != null)
                    {
                        item.CopyProperties(position);
                        positions.Add(position);
                        await positionRepo.Update(position);
                        await _context.SaveChangesAsync();
                    }
                }
                docInfo.Positions = positions;

                await docInfoRep.Update(docInfo);
                await _context.SaveChangesAsync();

                entity.DocumentInformations = docInfo;
            }

            await base.Update(entity);
        }
    }
}
