using System.Text.Json.Serialization;
using Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit;

namespace Infrastructure.JsonSourceGenerators;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Serialization)]
    [JsonSerializable(typeof(MaterialAudit))]
internal partial class EntityAuditJsonSourceGenerator : JsonSerializerContext
{
}