using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
      GenerateEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateEnemy()
    {
      var spriteRand = Random.Range(0, _enemySprites.Length);
      var enemy = Instantiate(_prefab, _enemyPos.position, Quaternion.identity);
      enemy.Init(_enemySprites[spriteRand]);
    }
  }
}
