using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.DataTransferObjects.UpdateDtos;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public async Task<Position[]> GetAllAsync()
        {
            return await _context.Positions
                .IncludeAllRecursively()
                .ToArrayAsync();
        }

        public override async Task<Position> GetByIdAsync(Guid id)
        {
            return await _context.Positions
                .IncludeAllRecursively()
                .SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdateWithDto(UpdatePositionDto dto, Guid documentInformationId)
        {
            Position entity = null;
            if(dto.Id != null)
            {
                var guidString = dto.Id.ToString();
                var guid = Guid.Parse(guidString);
                entity = await GetByIdAsync(guid);
            }
            if(entity == null)
            {
                entity = new Position();
                entity.ProductId = dto.ProductId;
                entity.Discount = dto.Discount;
                entity.Quantity = dto.Quantity;
                entity.DocumentInformationId = documentInformationId;
                entity.TypeOfDiscount = dto.TypeOfDiscount;
                await AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                dto.CopyProperties(entity);
                await Update(entity);
            }
        }
    }
}
