using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence.Repository
{
    internal class BSFileRepository : Repository<BSFile>, IBSFileRepository
    {
        public BSFileRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public Task<BSFile[]> GetAllAsync()
        {
            return _context.BSFiles.ToArrayAsync();
        }
    }
}
