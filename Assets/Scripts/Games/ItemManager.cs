using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
  [SerializeField]
  private Item _prefab;
  [SerializeField]
  private Transform _point;

  [SerializeField]
  private float _intervalMax;
  [SerializeField]
  private float _intervalMin;
  [SerializeField]
  private float _intervalFever;

  [SerializeField]
  private float _itemSpeed;
  [SerializeField]
  private float _itemSpeedFever;

  [SerializeField]
  private float _feverTime;

  private float _timerFever;

  private float _timer;
  private float _interval;

  public void Init()
  {
    _interval = Random.Range(_intervalMin, _intervalMax);
  }

  public void Interval()
  {
    _timer += Time.deltaTime;

    if (_timer >= _interval)
    {
      _timer = 0;
      Generate(_itemSpeed);
      Init();
    }
  }

  public void FeverInternal()
  {
    _timer += Time.deltaTime;

    if (_timer >= _intervalFever)
    {
      _timer = 0;
      Generate(_itemSpeedFever);
      Init();
    }
  }

  public void Generate(float speed)
  {
    var item = Instantiate(_prefab, _point.position, Quaternion.identity);
    item.Init(speed);
  }
}
