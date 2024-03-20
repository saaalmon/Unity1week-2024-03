using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class WorkerManager : MonoBehaviour
{
  [SerializeField]
  private WorkerData[] _datas;
  [SerializeField]
  private Worker _worker;

  public IReactiveCollection<WorkerModel> WorkerModelList => _workerModelList;
  private readonly ReactiveCollection<WorkerModel> _workerModelList = new ReactiveCollection<WorkerModel>();

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
    var wokerModel = new WorkerModel(_datas[rand]);
    _workerModelList.Add(wokerModel);
  }

  public void GenerateWorker(WorkerModel model, WorkerPoint point)
  {
    var worker = Instantiate(_worker, point.transform.position, Quaternion.identity);
    worker.Init(model);
    point.WorkerChange(worker);
  }
}
