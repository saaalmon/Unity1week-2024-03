using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cinemachine;

namespace Game
{
  public class Player : MonoBehaviour, IHitable
  {
    private Animator _anim;

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

      HpManager._instance?.Hp
      .Where(x => x <= 0)
      .Subscribe(x =>
      {
        _hitImp.GenerateImpulse();
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
      HpManager._instance?.Sub();
      ComboManager._instance?.Set();
    }
  }
}
