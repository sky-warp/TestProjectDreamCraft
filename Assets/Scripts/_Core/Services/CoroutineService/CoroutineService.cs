using System;
using System.Collections;
using UnityEngine;

public class CoroutineService : IDisposable
{
    private readonly MonoBehaviour _mono;

    public CoroutineService(MonoBehaviour mono)
    {
        _mono = mono;
    }

    public void StartCoroutine(IEnumerator enumerator) =>
        _mono.StartCoroutine(enumerator);
    
    public void StopCoroutine(IEnumerator enumerator) =>
        _mono.StopCoroutine(enumerator);

    public void Dispose()
    {
        _mono.StopAllCoroutines();
    }
}