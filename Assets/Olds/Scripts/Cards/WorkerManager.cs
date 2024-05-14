using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class WorkerManager : MonoBehaviour
{
  [SerializeField]
  private WorkerData[] _datas;
  [SerializeField]
  private Worker _worker;

  public IReactiveCollection<WorkerModel> WorkerModelList => _workerModelList;
  private readonly ReactiveCollection<WorkerModel> _workerModelList = new ReactiveCollection<WorkerModel>();

  public ISubject<Worker> WorkerSubject => _workerSubject;
  private readonly Subject<Worker> _workerSubject = new Subject<Worker>();

  // Start is called before the first frame update
  void Start()
  {
    for (var i = 0; i < 5; i++)
    {
      GenerateModel();
    }
  }

  public void GenerateModel()
  {
    var rand = Random.Range(0, _datas.Length);
    var workerModel = new WorkerModel(_datas[rand]);
    _workerModelList.Add(workerModel);

    this.UpdateAsObservable()
    .Subscribe(_ =>
    {
      if (workerModel.State.Value == WorkState.RESTING)
      {
        workerModel.MotivAdd();
      }

      if (workerModel.State.Value == WorkState.WAITING)
      {
        workerModel.MotivSub();
      }

      if (workerModel.State.Value == WorkState.WORKING)
      {
        workerModel.MotivSub();
        workerModel.WorkInterval();
      }
    })
    .AddTo(this);
  }

  public void GenerateWorker(WorkerModel model, WorkerPoint point)
  {
    var worker = Instantiate(_worker, point.transform.position, Quaternion.identity);
    worker.Init(model);
    point.WorkerChange(worker);
    _workerSubject.OnNext(worker);
  }
}
