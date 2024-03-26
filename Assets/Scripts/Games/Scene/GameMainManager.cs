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
using unityroom.Api;

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
    private ComboManager _comboManager;
    [SerializeField]
    private EnemyManager _enemyManager;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private FadeManager _fadeManager;

    [SerializeField]
    private CanvasGroup _TitleCanvas;
    [SerializeField]
    private CanvasGroup _GameCanvas;

    public static GameMainManager _instance;

    public ISubject<int> ResultScoreSubject => _resultScoreSubject;
    private readonly Subject<int> _resultScoreSubject = new Subject<int>();

    public ISubject<Unit> ResultSubject => _resultSubject;
    private readonly Subject<Unit> _resultSubject = new Subject<Unit>();

    public ISubject<string> StressSubject => _stressSubject;
    private readonly Subject<string> _stressSubject = new Subject<string>();

    public ISubject<int> CountDownSubject => _countDownSubject;
    private readonly Subject<int> _countDownSubject = new Subject<int>();

    // Awake is called before the first frame update
    async public UniTask Awake()
    {
      _instance = this;

      CinemachineImpulseManager.Instance.IgnoreTimeScale = true;
      _TitleCanvas.gameObject.SetActive(true);

      SoundManager._instance?.StopBGM();
      SoundManager._instance?.PlayBGM("BGM_Title");

      _timeManager.Init();
      await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

      SoundManager._instance?.StopBGM();

      _TitleCanvas.gameObject.SetActive(false);
      _GameCanvas.gameObject.SetActive(true);

      StressSubject.OnNext("お断りします！！");
      SoundManager._instance?.PlaySE("Shout");

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));

      SoundManager._instance?.PlayBGM("BGM_Game");

      _hpManager.Init();
      _comboManager.Init();
      _enemyManager.Init();
      _scoreManager.Init();
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

      SoundManager._instance?.StopBGM();

      cts.Cancel();

      ResultSubject.OnNext(Unit.Default);
      StressSubject.OnNext("終了！");
      SoundManager._instance?.PlaySE("Shout");

      _enemyManager.DestroyEnemy();
      _playerManager.ClearInputKeyCode();

      updateObservable.Dispose();
      enemyObservable.Dispose();
      textObservable.Dispose();

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f));

      _GameCanvas.gameObject.SetActive(false);

      SoundManager._instance?.PlayBGM("BGM_Result");

      // _playerManager.gameObject.SetActive(false);
      // _enemyManager.gameObject.SetActive(false);

      _resultScoreSubject.OnNext(_scoreManager.Score.Value);
      UnityroomApiClient.Instance.SendScore(1, _scoreManager.Score.Value, ScoreboardWriteMode.Always);

      await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

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

    async public UniTask CountDown()
    {
      CountDownSubject.OnNext(3);
      SoundManager._instance?.PlaySE("CountDown", 1.2f);

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f));

      CountDownSubject.OnNext(2);
      SoundManager._instance?.PlaySE("CountDown", 1.4f);

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f));

      CountDownSubject.OnNext(1);
      SoundManager._instance?.PlaySE("CountDown", 1.6f);
    }
  }
}
