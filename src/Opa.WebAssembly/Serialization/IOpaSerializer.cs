namespace Opa.WebAssembly.Serialization;

public interface IOpaSerializer
{
    string Serialize<TValue>(TValue value);

    TValue? Deserialize<TValue>(string json);
}
