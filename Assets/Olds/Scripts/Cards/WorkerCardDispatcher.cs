using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WorkerCardDispatcher : MonoBehaviour
{
  [SerializeField]
  private WorkerManager _manager;
  [SerializeField]
  private WorkerCardPresenter _presenter;
  [SerializeField]
  private WorkerCardView _view;

  [SerializeField]
  private Transform _parent;

  // Start is called before the first frame update
  void Start()
  {
    foreach (var c in _manager.WorkerModelList)
    {
      Dispatch(c);
    }

    _manager.WorkerModelList.ObserveAdd()
    .Subscribe(x =>
    {
      Dispatch(x.Value);
    })
    .AddTo(this);
  }

  public void Dispatch(WorkerModel model)
  {
    var view = Instantiate(_view, _parent);
    view.Init(model.Data);
    _presenter.OnCreateCard(_manager, model, view);
  }
}
