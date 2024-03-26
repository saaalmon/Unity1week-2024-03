using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cinemachine;
using Cysharp.Threading.Tasks;

namespace Game
{
  public class Player : MonoBehaviour, IHitable
  {
    private Animator _anim;

    [SerializeField]
    private HpManager _hpManager;

    [SerializeField]
    private CinemachineImpulseSource _fukiImp;
    [SerializeField]
    private CinemachineImpulseSource _hitImp;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private Fukidashi _prefab;
    [SerializeField]
    private float _fukiSpeed;

    [SerializeField]
    private ParticleSystem _hitParticle;

    // Start is called before the first frame update
    void Start()
    {
      Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
      _anim = GetComponent<Animator>();

      _hpManager.Hp
      .Skip(1)
      .Where(x => x <= 0)
      .Subscribe(x =>
      {
        _hitImp.GenerateImpulse();

        HitStop().Forget();
      })
      .AddTo(this);
    }

    public void GenerateFukidashi()
    {
      var fukidashi = Instantiate(_prefab, transform.position, Quaternion.identity);
      fukidashi.Init(Vector3.left, _fukiSpeed);

      _anim.SetTrigger("IsShout");
      SoundManager._instance?.PlaySE("Shout");

      _fukiImp.GenerateImpulse();
    }

    public void Hit()
    {
      _anim.SetTrigger("IsHit");

      HpManager._instance?.Sub();
      ComboManager._instance?.Set();
    }

    async public UniTask HitStop()
    {
      Instantiate(_hitParticle, transform.position, Quaternion.identity);

      Time.timeScale = 0.0f;
      await UniTask.Delay(System.TimeSpan.FromSeconds(0.2f), ignoreTimeScale: true);
      Time.timeScale = 1.0f;
    }
  }
}
