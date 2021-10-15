﻿using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Contracts.Repository;
using BillingSoftware.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed;

        public UnitOfWork() : this(new ApplicationDbContext())
        {
        }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CompanyRepository = new CompanyRepository(_context);
            AddressRepository = new AddressRepository(_context);
            UserRepository = new UserRepository(_context);
            ContactRepository = new ContactRepository(_context);
            DeliveryNoteRepository = new DeliveryNoteRepository(_context);
            DocumentInformationsRepository = new DocumentInformationsRepository(_context);
            InvoiceRepository = new InvoiceRepository(_context);
            OfferRepository = new OfferRepository(_context);
            OrderConfirmationRepository = new OrderConfirmationRepository(_context);
            PositionRepository = new PositionRepository(_context);
            ProductRepository = new ProductRepository(_context);
        }

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

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task DeleteDatabaseAsync() => await _context.Database.EnsureDeletedAsync();
        public async Task MigrateDatabaseAsync() => await _context.Database.MigrateAsync();
        public async Task CreateDatabaseAsync() => await _context.Database.EnsureCreatedAsync();

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
