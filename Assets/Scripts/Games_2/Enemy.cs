using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Game
{
  public class Enemy : MonoBehaviour, IHitable
  {
    private SpriteRenderer sp;

    [SerializeField]
    private Fukidashi _prefab;
    [SerializeField]
    private float _fukiSpeed;
    [SerializeField]
    private int _count;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Sprite sprite)
    {
      sp = GetComponent<SpriteRenderer>();

      sp.sprite = sprite;

      FukidashiInterval().Forget();
    }

    public void Hit()
    {
      Destroy(gameObject);
    }

    async public UniTask FukidashiInterval()
    {
      for (var i = 0; i < _count; i++)
      {
        await UniTask.Delay(System.TimeSpan.FromSeconds(0.1f));

        GenerateFukidashi();
      }
    }

    public void GenerateFukidashi()
    {
      var randPos = new Vector3(2, 0, 0) + Vector3.up * Random.Range(-1f, 1f);
      var fukidashi = Instantiate(_prefab, transform.position + randPos, Quaternion.identity);
      fukidashi.Init(Vector3.right, _fukiSpeed);
    }
  }
}
