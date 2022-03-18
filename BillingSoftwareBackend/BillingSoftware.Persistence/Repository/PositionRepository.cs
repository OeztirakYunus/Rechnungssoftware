using BillingSoftware.Core.Contracts.Repository;
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
    }
}
