using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenSequencer
{
    private readonly MonoBehaviour _mono;
    private readonly Queue<IEnumerator> _coroutines = new();
    private Action _onComplete;

    public TweenSequencer(MonoBehaviour mono)
    {
        _mono = mono;
    }

    /// <summary>
    /// Adds a coroutine step to the sequence.
    /// </summary>
    public TweenSequencer Append(IEnumerator coroutine)
    {
        _coroutines.Enqueue(coroutine);
        return this;
    }

    /// <summary>
    /// Sets a callback when all coroutines in the sequence have completed.
    /// </summary>
    public TweenSequencer OnComplete(Action callback)
    {
        _onComplete = callback;
        return this;
    }

    /// <summary>
    /// Starts playing the sequence.
    /// </summary>
    public void Play()
    {
        _mono.StartCoroutine(RunSequence());
    }

    private IEnumerator RunSequence()
    {
        while (_coroutines.Count > 0)
        {
            yield return _mono.StartCoroutine(_coroutines.Dequeue());
        }

        _onComplete?.Invoke();
    }
}
