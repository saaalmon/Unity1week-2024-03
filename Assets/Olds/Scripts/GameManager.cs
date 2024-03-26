using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public enum GameState
  {
    NORMAL,
    IDLE,
    FEVER,
    RESULT,
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

  public ISubject<int> ResultSubject => _resultSubject;
  private readonly Subject<int> _resultSubject = new Subject<int>();

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

              if (_timeManager.Timer.Value <= 0)
              {
                _state = GameState.RESULT;
              }

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

    await UniTask.WaitUntil(() => _state == GameState.RESULT);

    updateObservable.Dispose();
    _resultSubject.OnNext(_moneyManager.Money.Value);

    _resultCanvas.gameObject.SetActive(true);

    this.UpdateAsObservable()
    .Where(_ => Input.GetKeyDown(KeyCode.Space))
    .Subscribe(_ =>
    {
      SceneManager.LoadScene("TitleScene");
    })
    .AddTo(this);

    // this.UpdateAsObservable()
    // .Where(_ => Input.GetKeyDown(KeyCode.X))
    // .Subscribe(_ =>
    // {
    //   ChangeState(GameState.FEVER).Forget();
    // })
    // .AddTo(this);

    Debug.Log("終了");
  }

  async public UniTask ChangeState(GameState state)
  {
    _state = GameState.IDLE;
    await UniTask.Delay(System.TimeSpan.FromSeconds(3.0f));
    _state = state;
  }
}
