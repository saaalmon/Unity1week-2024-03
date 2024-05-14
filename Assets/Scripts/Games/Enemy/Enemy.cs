using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using Cinemachine;
using DG.Tweening;


namespace Game
{
  public class Enemy : MonoBehaviour, IHitable
  {
    private SpriteRenderer sp;
    private CinemachineImpulseSource imp;

    [SerializeField]
    private Fukidashi _prefab;
    [SerializeField]
    private float _fukiSpeed;
    [SerializeField]
    private int _count;
    [SerializeField]
    private ParticleSystem _hitParticle;

    public ISubject<Unit> DestroySubject => _destroySubject;
    private readonly Subject<Unit> _destroySubject = new Subject<Unit>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Sprite sprite, Vector3 point)
    {
      sp = GetComponent<SpriteRenderer>();
      imp = GetComponent<CinemachineImpulseSource>();

      sp.sprite = sprite;

      transform.DOMove(point, 0.2f);

      FukidashiInterval().Forget();
    }

    public void Hit()
    {
      HitStop().Forget();

      ComboManager._instance?.Add();
      ScoreManager._instance?.Add(1000);
      _destroySubject.OnNext(Unit.Default);
      Instantiate(_hitParticle, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }

    async public UniTask HitStop()
    {
      imp.GenerateImpulse();
      SoundManager._instance?.PlaySE("Hit_Enemy");

      Time.timeScale = 0.0f;
      await UniTask.Delay(System.TimeSpan.FromSeconds(0.2f), ignoreTimeScale: true);
      Time.timeScale = 1.0f;
    }

    async public UniTask FukidashiInterval()
    {
      var token = this.GetCancellationTokenOnDestroy();

      for (var i = 0; i < _count; i++)
      {
        await UniTask.Delay(System.TimeSpan.FromSeconds(0.3f), cancellationToken: token);

        GenerateFukidashi(i);
      }

      await UniTask.Delay(System.TimeSpan.FromSeconds(3.0f), cancellationToken: token);

      _destroySubject.OnNext(Unit.Default);

      transform.DOMove(transform.position + new Vector3(-4, 0, 0), 0.2f)
      .OnComplete(() => Destroy(gameObject));

    }

    public void GenerateFukidashi(int index)
    {
      var randPos = new Vector3(1, 0, -index * 0.2f) + Vector3.up * Random.Range(-1f, 1f);
      var fukidashi = Instantiate(_prefab, transform.position + randPos, Quaternion.identity);
      fukidashi.Init(Vector3.right, _fukiSpeed);
    }
  }
}
