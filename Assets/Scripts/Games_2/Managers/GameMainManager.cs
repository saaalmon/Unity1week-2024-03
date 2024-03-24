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
    private ComboManager _comboManager;
    [SerializeField]
    private EnemyManager _enemyManager;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private FadeManager _fadeManager;

    [SerializeField]
    private CanvasGroup _TitleCanvas;

    public ISubject<int> ResultScoreSubject => _resultScoreSubject;
    private readonly Subject<int> _resultScoreSubject = new Subject<int>();

    // Awake is called before the first frame update
    async public UniTask Awake()
    {
      CinemachineImpulseManager.Instance.IgnoreTimeScale = true;
      _TitleCanvas.gameObject.SetActive(true);

      _timeManager.Init();
      await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

      _TitleCanvas.gameObject.SetActive(false);

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

      await UniTask.WaitUntil(() => _timeManager.Timer.Value <= 0 || _hpManager.Hp.Value <= 0);

      cts.Cancel();

      _playerManager.ClearInputKeyCode();
      _enemyManager.DestroyEnemy();

      updateObservable.Dispose();
      enemyObservable.Dispose();

      Debug.Log("Finish");

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f));

      // _playerManager.gameObject.SetActive(false);
      // _enemyManager.gameObject.SetActive(false);

      _resultScoreSubject.OnNext(_scoreManager.Score.Value);

      await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

      var seq = DOTween.Sequence()
      .Append(_fadeManager.FadeOut())
      .OnComplete(() => SceneManager.LoadScene("GameScene_2"))
      .Play();
    }

    async private UniTask StartUpEnemy(CancellationTokenSource cts)
    {
      _playerManager.ClearInputKeyCode();

      await UniTask.Delay(System.TimeSpan.FromSeconds(1.0f), cancellationToken: cts.Token);

      _enemyManager.GenerateEnemy();
      _playerManager.FeverJadge();
    }
  }
}
