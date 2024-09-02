using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
  private readonly Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();
  private StateSO _currentState = default;
  public StateSO[] States = default;

  private void Start()
  {
    _currentState = States[0];

    for (int i = 0; i < States.Length; i++)
      States[i].InitComponent(this);

    _currentState.OnStateEnter();
  }

  public new bool TryGetComponent<T>(out T component) where T : Component
  {
    var type = typeof(T);
    if (!_cachedComponents.TryGetValue(type, out var value))
    {
      if (base.TryGetComponent<T>(out component))
        _cachedComponents.Add(type, component);

      return component != null;
    }

    component = (T)value;
    return true;
  }

  public new T GetComponent<T>() where T : Component
  {
    return TryGetComponent(out T component)
      ? component : throw new InvalidOperationException($"{typeof(T).Name} not found in {name}.");
  }

  private void Update()
  {
    if (_currentState.GetTransition(out var transitionState))
      Transition(transitionState);

    _currentState.OnUpdate();
  }

  private void Transition(StateSO transitionState)
  {
    _currentState.OnStateExit();
    _currentState = transitionState;
    _currentState.OnStateEnter();
  }
}
