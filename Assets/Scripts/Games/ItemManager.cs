using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

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
    Observable
    .Interval(TimeSpan.FromSeconds(_interval))
    .Subscribe(_ =>
    {
      Generate();
    })
    .AddTo(this);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Generate()
  {
    var item = Instantiate(_prefab, _point.position, Quaternion.identity);
    item.Init();
  }
}
