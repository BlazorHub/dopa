using System.Text.Json;

namespace Opa.WebAssembly.Serialization;

public class DefaultSerializer : IOpaSerializer
{
    private readonly JsonSerializerOptions? options;

    public DefaultSerializer()
    {
    }

    public DefaultSerializer(JsonSerializerOptions options)
    {
        this.options = options;
    }

    public TValue? Deserialize<TValue>(string json) =>
        JsonSerializer.Deserialize<TValue>(json, this.options);

    public string Serialize<TValue>(TValue value) =>
        JsonSerializer.Serialize(value, this.options);
}
