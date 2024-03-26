using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cinemachine;
using System.Threading;

namespace Game
{
  public class GameMainManager : MonoBehaviour
  {
    [SerializeField]
    private PlayerManager _playerManager;
    [SerializeField]
    private TimeManager _timeManager;
    [SerializeField]
    private HpManager _hpManager;
    [SerializeField]
    private EnemyManager _enemyManager;
    [SerializeField]
    private FadeManager _fadeManager;

    [SerializeField]
    private CanvasGroup _titleCanvas;
    [SerializeField]
    private CanvasGroup _gameMainCanvas;
    [SerializeField]
    private CanvasGroup _resultCanvas;

    public static GameMainManager _instance;

    public ISubject<Unit> ResultSubject => _resultSubject;
    private readonly Subject<Unit> _resultSubject = new Subject<Unit>();

    public ISubject<string> StressSubject => _stressSubject;
    private readonly Subject<string> _stressSubject = new Subject<string>();

    public ISubject<int> CountDownSubject => _countDownSubject;
    private readonly Subject<int> _countDownSubject = new Subject<int>();

    // Awake is called before the first frame update
    async public UniTask Awake()
    {
      //タイトルシーン初期化
      _instance = this;
      CinemachineImpulseManager.Instance.IgnoreTimeScale = true;

      SoundManager._instance?.StopBGM();
      SoundManager._instance?.PlayBGM("BGM_Title");

      _titleCanvas.gameObject.SetActive(true);

      _timeManager.Init();
      _hpManager.Init();

      await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

      //ゲームシーン初期化
      SoundManager._instance?.StopBGM();

      _titleCanvas.gameObject.SetActive(false);
      _gameMainCanvas.gameObject.SetActive(true);

      StressSubject.OnNext("お断りします！！");
      SoundManager._instance?.PlaySE("Shout");

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));

      //ゲームシーン
      SoundManager._instance?.PlayBGM("BGM_Game");

      _enemyManager.Init();
      _playerManager.Init();

      var cts = new CancellationTokenSource();
      StartUpEnemy(cts).Forget();

      var updateObservable = this.UpdateAsObservable()
      .Subscribe(_ =>
      {
        _timeManager.Sub(Time.deltaTime);
      })
      .AddTo(this);

      var enemyObservable = _enemyManager.EnemyDestroySubject
      .Subscribe(_ =>
      {
        StartUpEnemy(cts).Forget();
      })
      .AddTo(this);

      var textObservable = _timeManager.Timer
      .Where(x => x <= 3.0f)
      .Take(1)
      .Subscribe(_ =>
      {
        CountDown().Forget();
      })
      .AddTo(this);

      await UniTask.WaitUntil(() => _timeManager.Timer.Value <= 0 || _hpManager.Hp.Value <= 0);

      //リザルトシーン初期化
      SoundManager._instance?.StopBGM();

      cts.Cancel();

      _stressSubject.OnNext("終了！");
      SoundManager._instance?.PlaySE("Shout");

      _enemyManager.DestroyEnemy();
      _playerManager.ClearInputKeyCode();

      updateObservable.Dispose();
      enemyObservable.Dispose();
      textObservable.Dispose();

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));

      //リザルトシーン
      SoundManager._instance?.PlayBGM("BGM_Result");

      _gameMainCanvas.gameObject.SetActive(false);
      _resultCanvas.gameObject.SetActive(true);

      _resultSubject.OnNext(Unit.Default);

      await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

      //タイトルへ
      var seq = DOTween.Sequence()
      .Append(_fadeManager.FadeOut())
      .OnComplete(() => SceneManager.LoadScene("GameScene"))
      .Play();
    }

    async private UniTask StartUpEnemy(CancellationTokenSource cts)
    {
      _playerManager.ClearInputKeyCode();

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f), cancellationToken: cts.Token);

      _enemyManager.GenerateEnemy();
      _playerManager.FeverJadge();
    }

    async private UniTask CountDown()
    {
      _countDownSubject.OnNext(3);
      SoundManager._instance?.PlaySE("CountDown", 1.2f);

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f));

      _countDownSubject.OnNext(2);
      SoundManager._instance?.PlaySE("CountDown", 1.4f);

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f));

      _countDownSubject.OnNext(1);
      SoundManager._instance?.PlaySE("CountDown", 1.6f);
    }
  }
}
