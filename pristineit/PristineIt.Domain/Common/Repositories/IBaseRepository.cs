namespace PristineIt.Domain.Common.Repositories;

public interface IBaseRepository
{
    Task<bool> SaveChanges();
}
