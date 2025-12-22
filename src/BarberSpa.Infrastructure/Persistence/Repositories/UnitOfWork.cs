using BarberSpa.Domain.Ports.Out;
using BarberSpa.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace BarberSpa.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            // Inicializamos los repositorios pasando el contexto
            Users = new UserRepository(_context);
            Barbers = new BarberRepository(_context);
            Services = new ServiceRepository(_context);
            Appointments = new AppointmentRepository(_context);
        }

        public IUserRepository Users { get; }
        public IBarberRepository Barbers { get; }
        public IServiceRepository Services { get; }
        public IAppointmentRepository Appointments { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}