namespace GadgetLand.Application.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
