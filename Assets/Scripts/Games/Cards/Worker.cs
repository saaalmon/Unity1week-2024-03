using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Worker : MonoBehaviour
{
  private SpriteRenderer sr;

  [SerializeField]
  private BoxCollider _itemCollider;

  private WorkerModel _model;
  public WorkerModel Model => _model;

  public void Init(WorkerModel model)
  {
    sr = GetComponent<SpriteRenderer>();

    _model = model;
    sr.sprite = _model.Data.Icon;

    _itemCollider
    .OnTriggerEnterAsObservable()
    .Where(_ => _model.State.Value == WorkState.WAITING)
    .Where(x => x.TryGetComponent(out Item item))
    .Select(x => x.GetComponent<Item>())
    .Subscribe(x =>
    {
      Destroy(x.gameObject);
      _model.ChangeState(WorkState.WORKING);
    })
    .AddTo(this);

    _model.Motiv
    .Subscribe(x =>
    {
      _model.WorkCapability();
    })
    .AddTo(this);

    _model.Motiv
    .Where(x => x <= 0)
    .Subscribe(_ =>
    {
      _model.ChangeState(WorkState.RESTING);
      Destroy(this.gameObject);
    })
    .AddTo(this);

    _model.Timer
    .Subscribe(x =>
    {
      if (x >= _model.Interval.Value)
      {
        _model.Timer.Value = 0;
        _model.ChangeState(WorkState.WAITING);
      }
    })
    .AddTo(this);
  }
}
