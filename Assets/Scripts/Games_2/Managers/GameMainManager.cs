using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

    public ISubject<int> ResultSubject => _resultSubject;
    private readonly Subject<int> _resultSubject = new Subject<int>();

    // Awake is called before the first frame update
    async public UniTask Awake()
    {
      _TitleCanvas.gameObject.SetActive(true);

      await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

      _TitleCanvas.gameObject.SetActive(false);

      _timeManager.Init();
      _hpManager.Init();
      _comboManager.Init();
      _enemyManager.Init();
      _scoreManager.Init();
      _playerManager.Init();

      var updateObservable = this.UpdateAsObservable()
      .Subscribe(_ =>
      {
        _timeManager.Sub(Time.deltaTime);
      })
      .AddTo(this);

      await UniTask.WaitUntil(() => _timeManager.Timer.Value <= 0 || _hpManager.Hp.Value <= 0);
      _playerManager.gameObject.SetActive(false);
      _enemyManager.gameObject.SetActive(false);

      updateObservable.Dispose();
      _resultSubject.OnNext(_scoreManager.Score.Value);
      Debug.Log("Finish");

      await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

      var seq = DOTween.Sequence()
      .Append(_fadeManager.FadeOut())
      .OnComplete(() => SceneManager.LoadScene("GameScene_2"))
      .Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
  }
}
