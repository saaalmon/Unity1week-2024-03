using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPoint : MonoBehaviour
{
  private Worker _worker;

  public void WorkerChange(Worker worker)
  {
    if (_worker != null)
    {
      _worker.Model.ChangeState(WorkState.RESTING);
      Destroy(_worker.gameObject);
    }

    _worker = worker;
    _worker.Model.ChangeState(WorkState.WAITING);
  }
}
