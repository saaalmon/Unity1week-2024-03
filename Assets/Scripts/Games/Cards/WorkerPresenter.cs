using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WorkerPresenter : MonoBehaviour
{
  public void OnCreateWorker(WorkerModel model, WorkerView view)
  {
    model.Motiv
    .Subscribe(x =>
    {
      view.SetMotiv(x, model.MotivMax);
    })
    .AddTo(view);
  }
}
