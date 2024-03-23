using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class WorkerCardPresenter : MonoBehaviour
{
  public void OnCreateCard(WorkerManager manager, WorkerModel model, WorkerCardView view)
  {
    view.WorkerCardButton.OnSelectAsObservable()
    .Subscribe(_ =>
    {
      view.transform.DOScale(1.2f, 0.2f);
    })
    .AddTo(this);

    view.WorkerCardButton.OnDeselectAsObservable()
    .Subscribe(_ =>
    {
      view.transform.DOScale(1.0f, 0.2f);
    })
    .AddTo(this);

    view.WorkerCardButton.OnUpdateSelectedAsObservable()
    .Where(_ => Input.GetMouseButtonDown(0))
    .Subscribe(_ =>
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out RaycastHit hit))
      {
        if (hit.collider.TryGetComponent(out WorkerPoint workerPoint))
        {
          manager.GenerateWorker(model, workerPoint);
        }
      }
    })
    .AddTo(this);

    model.Motiv
    .Subscribe(x =>
    {
      view.SetMotiv(x, model.MotivMax);
    })
    .AddTo(view);

    model.State
    .Subscribe(x =>
    {
      if (x == WorkState.RESTING)
      {
        view.WorkerCardButton.interactable = true;
        view.SetMotivActive(true);
      }
      else if (x == WorkState.WAITING || x == WorkState.WORKING)
      {
        view.WorkerCardButton.interactable = false;
        view.SetMotivActive(false);
      }
    })
    .AddTo(this);
  }
}
