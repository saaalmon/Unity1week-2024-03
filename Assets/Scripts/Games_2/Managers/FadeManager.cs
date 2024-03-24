using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game
{
  public class FadeManager : MonoBehaviour
  {
    [SerializeField]
    private Material _material;
    [SerializeField]
    private float _timer = 0.4f;
    private readonly int _progressId = Shader.PropertyToID("_Progress");

    private void Awake()
    {
      if (_material != null) FadeIn();
    }

    public Tweener FadeIn()
    {
      return DOTween.To(() => 0f, (x) =>
      {
        _material.SetFloat(_progressId, x);
      }, 1f, _timer)
      .OnStart(() => gameObject.SetActive(true))
      .OnComplete(() => gameObject.SetActive(false));
    }

    public Tweener FadeOut()
    {
      return DOTween.To(() => 1f, (x) =>
      {
        _material.SetFloat(_progressId, x);
      }, 0f, _timer)
      .OnStart(() => gameObject.SetActive(true));
    }
  }
}
