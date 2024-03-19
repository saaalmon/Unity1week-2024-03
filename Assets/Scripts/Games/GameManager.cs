using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private TimeManager _timeManager;
  [SerializeField]
  private ItemManager _itemManager;

  [SerializeField]
  private CanvasGroup _resultCanvas;

  // Start is called before the first frame update
  async UniTask Start()
  {
    _timeManager.Init();

    var timer = this.UpdateAsObservable()
            .Subscribe(_ =>
            {
              _timeManager.Sub(Time.deltaTime);
              _itemManager.Interval();
            })
            .AddTo(this);

    await UniTask.WaitUntil(() => _timeManager.Timer.Value <= 0);

    timer.Dispose();

    _resultCanvas.gameObject.SetActive(true);
    Debug.Log("終了");
  }
}
