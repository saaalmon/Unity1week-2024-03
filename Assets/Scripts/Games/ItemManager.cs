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
  private float _interval;

  private float _timer;

  public void Interval()
  {
    _timer += Time.deltaTime;

    if (_timer >= _interval)
    {
      _timer = 0;
      Generate();
    }
  }

  public void Generate()
  {
    var item = Instantiate(_prefab, _point.position, Quaternion.identity);
    item.Init();
  }
}
