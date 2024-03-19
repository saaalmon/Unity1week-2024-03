using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Worker : MonoBehaviour
{
  [SerializeField]
  private BoxCollider _itemCollider;

  [SerializeField]
  private float _workInterval;

  // Start is called before the first frame update
  void Start()
  {
    _itemCollider
    .OnTriggerEnterAsObservable()
    .Where(x => x.TryGetComponent(out Item item))
    .ThrottleFirst(System.TimeSpan.FromSeconds(_workInterval))
    .Select(x => x.GetComponent<Item>())
    .Subscribe(x =>
    {
      Destroy(x.gameObject);
    })
    .AddTo(this);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
