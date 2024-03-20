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

  private float _interval;
  private float _timer;

  private WorkerModel _model;
  public WorkerModel Model => _model;

  public void Init(WorkerModel model)
  {
    sr = GetComponent<SpriteRenderer>();

    _model = model;
    sr.sprite = _model.Data.Icon;
    _interval = _model.Data.Interval;

    _itemCollider
    .OnTriggerEnterAsObservable()
    .Where(x => x.TryGetComponent(out Item item))
    .ThrottleFirst(System.TimeSpan.FromSeconds(_interval))
    .Select(x => x.GetComponent<Item>())
    .Subscribe(x =>
    {
      Destroy(x.gameObject);
    })
    .AddTo(this);

    this.UpdateAsObservable()
    .Subscribe(_ =>
    {
      if (model.State.Value == WorkState.RESTING)
      {
        model.MotivAdd();
      }

      if (model.State.Value == WorkState.WORKING)
      {
        model.MotivSub();
      }
    })
    .AddTo(this);
  }
}
