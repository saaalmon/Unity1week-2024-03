using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using DG.Tweening;

namespace Game
{
  public class Fukidashi : MonoBehaviour, IHitable
  {
    private Rigidbody rb;
    private BoxCollider coll;
    private CinemachineImpulseSource imp;

    [SerializeField]
    private SpriteRenderer sp;
    [SerializeField]
    private FukidashiData[] _datas;

    [SerializeField]
    private TextMeshProUGUI _fukiText;
    [SerializeField]
    private ParticleSystem _hitParticle;

    public void Init(Vector3 dir, float speed)
    {
      rb = GetComponent<Rigidbody>();
      coll = GetComponent<BoxCollider>();
      imp = GetComponent<CinemachineImpulseSource>();

      GameMainManager._instance?.ResultSubject
      .Subscribe(_ =>
      {
        Instantiate(_hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
      })
      .AddTo(this);

      var seq = DOTween.Sequence()
      .OnStart(() =>
      {
        transform.localScale = Vector3.zero;
        _fukiText.transform.localScale = Vector3.zero;
      })
      .Append(transform.DOScale(Vector3.one, 0.2f))
      .Join(_fukiText.transform.DOScale(Vector3.one * 2.0f, 0.4f))
      .Append(_fukiText.transform.DOScale(Vector3.one, 0.2f))
      .Play();

      rb.velocity = dir * speed;

      var data = _datas[Random.Range(0, _datas.Length)];
      sp.sprite = data.Fukidashi;
      _fukiText.text = data.Text;

      Destroy(gameObject, 3.0f);

      coll.OnTriggerEnterAsObservable()
      .Subscribe(x =>
      {
        if (x.TryGetComponent(out IHitable hitable))
        {
          if (this.gameObject.layer == 8)
          {
            imp.GenerateImpulse();
            SoundManager._instance?.PlaySE("Hit_Fukidashi", Random.Range(0.5f, 1.5f));

            Instantiate(_hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
          }

          hitable.Hit();
        }
      })
      .AddTo(this);
    }

    public void Hit()
    {

    }
  }
}
