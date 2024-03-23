using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;

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

    public ISubject<int> ResultSubject => _resultSubject;
    private readonly Subject<int> _resultSubject = new Subject<int>();

    // Awake is called before the first frame update
    async public UniTask Awake()
    {
      _timeManager.Init();
      _hpManager.Init();
      _comboManager.Init();
      _enemyManager.Init();
      _scoreManager.Init();

      var updateObservable = this.UpdateAsObservable()
      .Subscribe(_ =>
      {
        _timeManager.Sub(Time.deltaTime);
      })
      .AddTo(this);

      await UniTask.WaitUntil(() => _timeManager.Timer.Value <= 0 || _hpManager.Hp.Value <= 0);

      updateObservable.Dispose();
      _resultSubject.OnNext(100);
      Debug.Log("Finish");
    }

    // Update is called once per frame
    void Update()
    {

    }
  }
}
