using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum WorkState
{
  WAITING,
  WORKING,
  RESTING,
}

public class WorkerModel
{
  public IReactiveProperty<WorkState> State => _state;
  private readonly ReactiveProperty<WorkState> _state = new ReactiveProperty<WorkState>();

  public IReactiveProperty<float> Motiv => _motiv;
  private readonly FloatReactiveProperty _motiv = new FloatReactiveProperty();

  public IReactiveProperty<float> Timer => _timer;
  private readonly FloatReactiveProperty _timer = new FloatReactiveProperty();

  public IReactiveProperty<float> Interval => _interval;
  private readonly FloatReactiveProperty _interval = new FloatReactiveProperty();

  private float _intervalDef;
  public float IntervalDef => _intervalDef;

  private float _motivMax;
  public float MotivMax => _motivMax;

  private WorkerData _data;
  public WorkerData Data => _data;

  public WorkerModel(WorkerData data)
  {
    _state.Value = WorkState.RESTING;

    _data = data;
    _motivMax = _data.MotivMax;
    _intervalDef = _data.Interval;

    _motiv.Value = _motivMax;
    _interval.Value = _intervalDef;
  }

  public void ChangeState(WorkState state)
  {
    _state.Value = state;
  }

  public void MotivAdd()
  {
    _motiv.Value += Time.deltaTime;
    if (_motiv.Value >= _motivMax) _motiv.Value = _motivMax;
  }

  public void MotivSub()
  {
    _motiv.Value -= Time.deltaTime;
    if (_motiv.Value <= 0) _motiv.Value = 0;
  }

  public void WorkInterval()
  {
    _timer.Value += Time.deltaTime;
  }

  public void WorkCapability()
  {
    var ratio = (float)_motiv.Value / _motivMax;

    if (ratio > 0.75f) _interval.Value = _intervalDef * 0.5f;
    else if (ratio > 0.5f) _interval.Value = _intervalDef * 0.8f;
    else _interval.Value = _intervalDef;

    Debug.Log(_interval.Value);
  }
}
