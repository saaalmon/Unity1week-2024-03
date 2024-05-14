using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WorkerDispatcher : MonoBehaviour
{
  [SerializeField]
  private WorkerManager _manager;
  [SerializeField]
  private WorkerPresenter _presenter;
  [SerializeField]
  private WorkerView _view;

  // Start is called before the first frame update
  void Start()
  {
    _manager.WorkerSubject
    .Subscribe(x =>
    {
      Dispatch(x);
    })
    .AddTo(this);
  }

  public void Dispatch(Worker worker)
  {
    var view = Instantiate(_view, worker.transform, true);
    view.transform.position = worker.transform.position;
    _presenter.OnCreateWorker(worker.Model, view);
  }
}
