using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Worker : MonoBehaviour
{
  [SerializeField]
  private SpriteRenderer sr;

  [SerializeField]
  private BoxCollider _itemCollider;
  [SerializeField]
  private ParticleSystem _workParticle;

  private WorkerModel _model;
  public WorkerModel Model => _model;

  private Item _workItem;

  public void Init(WorkerModel model)
  {
    _model = model;
    sr.sprite = _model.Data.Icon;

    _itemCollider
    .OnTriggerEnterAsObservable()
    .Where(_ => _model.State.Value == WorkState.WAITING)
    .Where(x => x.TryGetComponent(out Item item))
    .Select(x => x.GetComponent<Item>())
    .Subscribe(x =>
    {
      _workItem = x;
      Destroy(x.gameObject);
      _model.ChangeState(WorkState.WORKING);
      Instantiate(_workParticle, transform.position, Quaternion.identity);

      // var seq = DOTween.Sequence()
      // .Append(transform.DOScaleY(0.5f, _model.Interval.Value / 2))
      // .Append(transform.DOScaleY(1.0f, _model.Interval.Value / 2))
      // .Play();
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

        MoneyManager._instance?.Add(_workItem.Money);
        _workItem = null;
        _model.ChangeState(WorkState.WAITING);
      }
    })
    .AddTo(this);
  }
}
