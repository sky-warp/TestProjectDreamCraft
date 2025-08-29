using System;
using System.Collections.Generic;

public class DisposableService
{
    private readonly List<IDisposable> _disposables = new();

    public void Add(IDisposable disposable)
    {
        if (_disposables.Contains(disposable) == false)
        {
            _disposables.Add(disposable);
        }
    }

    public void DisposeAll()
    {
        for (int i = 0; i < _disposables.Count; i++)
        {
            _disposables[i].Dispose();
        }
    }
}