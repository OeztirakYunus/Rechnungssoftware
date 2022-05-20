using BillingSoftware.Core.Contracts.Repository;
using System;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public ICompanyRepository CompanyRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public IContactRepository ContactRepository { get; }
        public IDeliveryNoteRepository DeliveryNoteRepository { get; }
        public IDocumentInformationsRepository DocumentInformationsRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }
        public IOfferRepository OfferRepository { get; }
        public IOrderConfirmationRepository OrderConfirmationRepository { get; }
        public IPositionRepository PositionRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }
        public IBSFileRepository BSFileRepository { get; }
        public IBankInformationRepository BankInformationRepository { get; }

        Task<int> SaveChangesAsync();
        Task DeleteDatabaseAsync();
        Task MigrateDatabaseAsync();
        Task CreateDatabaseAsync();
    }

}
