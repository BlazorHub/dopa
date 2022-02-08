﻿namespace CShopa;

public interface IOpaPolicy : IDisposable
{
    T? Evaluate<T>(object input);

    T? Evaluate<T>(object input, out string responseJson);

    string EvaluateJson(string json);

    void SetData<T>(T input);

    void SetDataJson(string json);

    bool AddBuiltin<TResult>(string name, Func<TResult> callback);

    bool AddBuiltin<TArg, TResult>(string name, Func<TArg, TResult> callback);

    bool AddBuiltin<TArg1, TArg2, TResult>(string name, Func<TArg1, TArg2, TResult> callback);

    bool AddBuiltin<TArg1, TArg2, TArg3, TResult>(string name, Func<TArg1, TArg2, TArg3, TResult> callback);

    bool AddBuiltin<TArg1, TArg2, TArg3, TArg4, TResult>(string name, Func<TArg1, TArg2, TArg3, TArg4, TResult> callback);
}