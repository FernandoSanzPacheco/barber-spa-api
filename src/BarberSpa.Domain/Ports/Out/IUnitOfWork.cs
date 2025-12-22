using System;
using System.Threading.Tasks;

namespace BarberSpa.Domain.Ports.Out
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IBarberRepository Barbers { get; }
        IServiceRepository Services { get; }
        IAppointmentRepository Appointments { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}