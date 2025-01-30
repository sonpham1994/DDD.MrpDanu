namespace Tests.Integration;

[CollectionDefinition(CollectionEndpointName.MaterialEndpoint)] // use the same instance across classes
public class MaterialEndpointCollection : ICollectionFixture<EndpointFactory>;

[CollectionDefinition(CollectionEndpointName.TransactionalPartnerEndpoint)] // use the same instance across classes
public class TransactionalPartnerEndpointCollection : ICollectionFixture<EndpointFactory>;