namespace Infrastructure.Persistence;

public interface IAppDbConnection
{
    bool CanConnect();
}