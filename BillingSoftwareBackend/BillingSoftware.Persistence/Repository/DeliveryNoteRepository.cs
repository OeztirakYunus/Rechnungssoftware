using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.DataTransferObjects.UpdateDtos;
using BillingSoftware.Core.Entities;
using CommonBase.Exceptions;
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

        public async Task UpdateWithDto(UpdateDeliveryNoteDto dto)
        {
            var entity = await GetByIdAsync(dto.Id);
            if (entity != null)
            {
                DocumentInformationsRepository documentInformationsRepository = new DocumentInformationsRepository(_context);
                dto.DocumentInformations.Id = dto.DocumentInformationsId;
                await documentInformationsRepository.UpdateWithDto(dto.DocumentInformations);
                dto.CopyProperties(entity);
                await Update(entity);
            }
            else
            {
                throw new EntityNotFoundException("DeliveryNote not found");
            }
        }
    }
}
