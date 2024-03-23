using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
  public class Player : MonoBehaviour, IHitable
  {
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
      HpManager._instance?.Hp
      .Where(x => x <= 0)
      .Subscribe(x =>
      {
        Destroy(gameObject);
      })
      .AddTo(this);
    }

    public void GenerateFukidashi()
    {
      var fukidashi = Instantiate(_prefab, transform.position, Quaternion.identity);
      fukidashi.Init(Vector3.left, _fukiSpeed);
    }

    public void Hit()
    {
      HpManager._instance?.Sub();
    }
  }
}
