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

  // Start is called before the first frame update
  void Start()
  {

  }

  public void Interval()
  {
    _timer += Time.deltaTime;

    if (_timer >= _interval)
    {
      _timer = 0;
      Generate();
    }

    // Observable
    // .Interval(System.TimeSpan.FromSeconds(_interval))
    // .Subscribe(_ =>
    // {
    //   Generate();
    // })
    // .AddTo(this);
  }

  public void Generate()
  {
    var item = Instantiate(_prefab, _point.position, Quaternion.identity);
    item.Init();
  }
}
