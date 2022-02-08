using System.Diagnostics.CodeAnalysis;
using Wasmtime;

namespace CShopa.Wasmtime;

internal sealed class OpaRuntime : OpaRuntimeBase
{
    private readonly Store store;

    [NotNull]
    private Instance? instance;

    [NotNull]
    private Memory? memory;

    public OpaRuntime(Store store, Memory memory, Linker linker, Module module)
    {
        this.store = store;
        this.memory = memory;

        instance = linker.Instantiate(store, module);
    }

    public override string ReadValueAt(int address) =>
        memory.ReadNullTerminatedString(store, address);

    public override void WriteValueAt(int address, string json) =>
        memory.WriteString(store, address, json);

    public override void Invoke(string function, params object[] rest)
    {
        var run = instance.GetFunction(store, function);

        if (run is Function)
        {
            run.Invoke(store, rest);
        }
        else
        {
            throw new InvalidOperationException($"Could not invoke '{function}'.");
        }
    }

    [return: MaybeNull] // the abstract class declares T? return type, but the override forces T..?
    public override T Invoke<T>(string function, params object[] rest)
    {
        var run = instance.GetFunction(store, function);

        if (run is Function)
        {
            return (T?)run.Invoke(store, rest);
        }

        throw new InvalidOperationException($"Could not invoke '{function}'");
    }

    ~OpaRuntime()
    {
        Dispose(false);
    }

    protected override void DisposeManaged()
    {
        this.store.Dispose();
    }

    protected override void DisposeUnmanaged()
    {
        this.instance = null;
        this.memory = null;
    }
}