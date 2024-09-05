using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
  [SerializeField] private StateTableSO _stateTable = default;
  [SerializeField, ReadOnly] private string currentStateName;

#if UNITY_EDITOR
  [Space]
  [SerializeField]
  // internal Debugging.StateMachineDebugger _debugger = default;
#endif

  private readonly Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();
  public State _currentState;

  private void Awake()
  {
    // #if UNITY_EDITOR
    //     _debugger.Awake(this);
    // #endif
  }
  private void Start()
  {
    _currentState = _stateTable.GetInitialState(this);

    currentStateName = _currentState.OriginSO.name;
  }

  public new bool TryGetComponent<T>(out T component) where T : Component
  {
    var type = typeof(T);
    if (!_cachedComponents.TryGetValue(type, out var value))
    {
      if (base.TryGetComponent<T>(out component))
      {
        _cachedComponents.Add(type, component);
        return true;
      }

      component = GetComponentInChildren<T>();
      if (component != null)
      {
        _cachedComponents.Add(type, component);
        return true;
      }

      return false;
    }

    component = (T)value;
    return true;
  }

  public T GetOrAddComponent<T>() where T : Component
  {
    if (!TryGetComponent<T>(out var component))
    {
      component = gameObject.AddComponent<T>();
      _cachedComponents.Add(typeof(T), component);
    }

    return component;
  }

  public new T GetComponent<T>() where T : Component
  {
    return TryGetComponent(out T component)
      ? component : throw new InvalidOperationException($"{typeof(T).Name} not found in {name}.");
  }

  private void Update()
  {
    if (_currentState.TryGetTransition(out var transitionState))
      Transition(transitionState);

    _currentState.OnUpdate();
  }

  private void Transition(State transitionState)
  {
    _currentState.OnStateExit();
    _currentState = transitionState;
    _currentState.OnStateEnter();

    currentStateName = _currentState.OriginSO.name;
  }
}
