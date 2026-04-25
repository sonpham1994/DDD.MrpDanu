namespace MinimalApi.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder routes);
}