using BillingSoftware.Core.DataTransferObjects.UpdateDtos;
using BillingSoftware.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts.Repository
{
    public interface IDocumentInformationsRepository : IRepository<DocumentInformations>
    {
        Task AddPosition(Guid documentInformationId, Position position);
        Task AddPositions(Guid documentInformationId, ICollection<Position> positions);
        Task DeletePosition(Guid documentInformationId, Guid positionId);
        public Task UpdateWithDto(UpdateDocumentInformationDto dto);
    }
}
