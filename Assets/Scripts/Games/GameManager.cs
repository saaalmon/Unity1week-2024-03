using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
  public enum GameState
  {
    NORMAL,
    IDLE,
    FEVER,
  }

  [SerializeField]
  private TimeManager _timeManager;
  [SerializeField]
  private MoneyManager _moneyManager;
  [SerializeField]
  private ItemManager _itemManager;

  [SerializeField]
  private CanvasGroup _resultCanvas;

  private GameState _state;

  [SerializeField]
  private float _feverTime;

  private float _timerFever;

  // Start is called before the first frame update
  async UniTask Start()
  {
    _state = GameState.NORMAL;

    _itemManager.Init();
    _timeManager.Init();
    _moneyManager.Init();

    var updateObservable = this.UpdateAsObservable()
            .Subscribe(_ =>
            {
              _timeManager.Sub(Time.deltaTime);

              if (_state == GameState.FEVER)
              {
                _itemManager.FeverInternal();

                _timerFever += Time.deltaTime;
                if (_timerFever >= _feverTime)
                {
                  _timerFever = 0;
                  ChangeState(GameState.NORMAL).Forget();
                }
              }
              else if (_state == GameState.NORMAL)
              {
                _itemManager.Interval();
              }
              else if (_state == GameState.IDLE)
              {

              }
            })
            .AddTo(this);

    this.UpdateAsObservable()
    .Where(_ => Input.GetKeyDown(KeyCode.X))
    .Subscribe(_ =>
    {
      ChangeState(GameState.FEVER).Forget();
    })
    .AddTo(this);

    await UniTask.WaitUntil(() => _timeManager.Timer.Value <= 0);

    updateObservable.Dispose();

    _resultCanvas.gameObject.SetActive(true);
    Debug.Log("終了");
  }

  async public UniTask ChangeState(GameState state)
  {
    _state = GameState.IDLE;
    await UniTask.Delay(System.TimeSpan.FromSeconds(3.0f));
    _state = state;
  }
}
