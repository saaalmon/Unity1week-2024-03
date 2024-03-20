using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum WorkState
{
  WORKING,
  RESTING,
}

public class WorkerModel
{
  public IReactiveProperty<WorkState> State => _state;
  private readonly ReactiveProperty<WorkState> _state = new ReactiveProperty<WorkState>();

  private float _motivMax;
  private float _interval;

  private float _motiv;
  private float _timer;

  private WorkerData _data;
  public WorkerData Data => _data;

  public WorkerModel(WorkerData data)
  {
    _state.Value = WorkState.RESTING;

    _data = data;
    _motivMax = _data.MotivMax;
    _interval = _data.Interval;

    _motiv = _motivMax;
  }

  public void ChangeState(WorkState state)
  {
    _state.Value = state;
  }

  public void MotivAdd()
  {
    _motiv += Time.deltaTime;
    if (_motiv >= _motivMax) _motiv = _motivMax;
  }

  public void MotivSub()
  {
    _motiv -= Time.deltaTime;
    if (_motiv <= 0) _motiv = 0;
  }
}
