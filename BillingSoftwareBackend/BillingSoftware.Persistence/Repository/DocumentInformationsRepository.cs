using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using CommonBase.Exceptions;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BillingSoftware.Core.DataTransferObjects.UpdateDtos;

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

        public async Task UpdateWithDto(UpdateDocumentInformationDto dto)
        {
            DocumentInformations entity = null;
            if (dto.Id != null && Guid.Empty != dto.Id)
            {
                var guidString = dto.Id.ToString();
                var guid = Guid.Parse(guidString);
                entity = await GetByIdAsync(guid);
                if (entity != null)
                {
                    PositionRepository positionRepository = new PositionRepository(_context);
                    foreach (var item in entity.Positions)
                    {
                        if(!dto.Positions.Select(i => i.Id).ToList().Contains(item.Id))
                        {
                            await positionRepository.Remove(item.Id);
                        }
                    }
                    
                    foreach (var item in dto.Positions)
                    {
                        await positionRepository.UpdateWithDto(item, guid);
                    }

                    entity = await GetByIdAsync(guid);
                    dto.CopyProperties(entity);
                    await Update(entity);
                }
            }
            else
            {
                throw new Exception("Id cannot be null");
            }
        }
    }
}
