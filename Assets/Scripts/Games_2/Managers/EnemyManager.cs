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

    private Enemy _enemy;

    public ISubject<Unit> EnemyDestroySubject => _enemyDestroySubject;
    private readonly Subject<Unit> _enemyDestroySubject = new Subject<Unit>();

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

    }

    public void GenerateEnemy()
    {
      var spriteRand = Random.Range(0, _enemySprites.Length);
      _enemy = Instantiate(_prefab, _enemyPos.position + new Vector3(-4, 0, 0), Quaternion.identity);
      _enemy.Init(_enemySprites[spriteRand], _enemyPos.position);

      _enemy.DestroySubject
      .Subscribe(_ =>
      {
        _enemy = null;
        _enemyDestroySubject.OnNext(Unit.Default);
      })
      .AddTo(this);
    }

    public void DestroyEnemy()
    {
      if (_enemy != null) Destroy(_enemy.gameObject);
    }
  }
}
