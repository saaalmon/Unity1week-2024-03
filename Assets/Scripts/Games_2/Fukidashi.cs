using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using TMPro;

namespace Game
{
  public class Fukidashi : MonoBehaviour
  {
    private Rigidbody rb;
    private BoxCollider coll;

    [SerializeField]
    private FukidashiData[] _datas;

    [SerializeField]
    private Image _fukiBack;
    [SerializeField]
    private TextMeshProUGUI _fukiText;

    public void Init(Vector3 dir, float speed)
    {
      rb = GetComponent<Rigidbody>();
      coll = GetComponent<BoxCollider>();

      rb.velocity = dir * speed;

      var data = _datas[Random.Range(0, _datas.Length)];
      _fukiBack.sprite = data.Fukidashi;
      _fukiText.text = data.Text;

      Destroy(gameObject, 5.0f);

      coll.OnTriggerEnterAsObservable()
      .Skip(1)
      .Subscribe(x =>
      {
        if (x.TryGetComponent(out IHitable hitable))
        {
          Destroy(gameObject);
          hitable.Hit();
        }
      })
      .AddTo(this);
    }
  }
}
