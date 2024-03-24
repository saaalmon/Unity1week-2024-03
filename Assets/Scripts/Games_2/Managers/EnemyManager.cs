using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Game
{
  public class EnemyManager : MonoBehaviour
  {
    [SerializeField]
    private Enemy _prefab;
    [SerializeField]
    private Sprite[] _enemySprites;

    [SerializeField]
    private Transform _enemyPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
      GenerateEnemy();
    }

    public void GenerateEnemy()
    {
      var spriteRand = Random.Range(0, _enemySprites.Length);
      var enemy = Instantiate(_prefab, _enemyPos.position + new Vector3(-4, 0, 0), Quaternion.identity);
      enemy.Init(_enemySprites[spriteRand], _enemyPos.position);

      enemy.DestroySubject
      .Delay(System.TimeSpan.FromSeconds(1.5f))
      .Subscribe(_ =>
      {
        GenerateEnemy();
      })
      .AddTo(this);
    }
  }
}
