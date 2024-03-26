using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
  public class PlayerManager : MonoBehaviour
  {
    [SerializeField]
    private ComboManager _comboManager;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private KeyCode[] _inputKeyCode;
    [SerializeField]
    private int _count;
    [SerializeField]
    private Sprite[] _keycodeImageSprites;
    [SerializeField]
    private Image _keycodeImage;
    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private Light _defaultLight;
    [SerializeField]
    private Light _feverLight;
    [SerializeField]
    private ParticleSystem _feverParticle;

    public IReadOnlyReactiveCollection<KeyCode> InputKeyCodeList => _inputKeyCodeList;
    private ReactiveCollection<KeyCode> _inputKeyCodeList = new ReactiveCollection<KeyCode>();

    private List<Image> _keyImageList = new List<Image>();

    public bool IsFever => _isFever;
    private bool _isFever = false;

    [SerializeField]
    private int _feverComboCountMax;
    [SerializeField]
    private int _feverCountMax;

    private int _feverComboCount;
    private int _feverCount;

    private float _timer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
      _timer += Time.deltaTime;

      if (Input.anyKeyDown)
      {
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
          if (Input.GetKeyDown(code))
          {
            if (code == (KeyCode)_inputKeyCodeList[0])
            {
              Debug.Log(code.ToString());
              _inputKeyCodeList.RemoveAt(0);
              Destroy(_keyImageList[0].gameObject);
              _keyImageList.RemoveAt(0);

              SoundManager._instance?.PlaySE("Input_Correct");

              if (_keyImageList.Count > 0) _keyImageList[0].transform.localScale = Vector3.one * 2f;

              if (_timer <= 0.3f) ScoreManager._instance?.Add(500);
              else if (_timer <= 0.5f) ScoreManager._instance?.Add(400);
              else if (_timer <= 0.8f) ScoreManager._instance?.Add(300);
              else if (_timer <= 1.0f) ScoreManager._instance?.Add(200);
              else ScoreManager._instance?.Add(100);

              _timer = 0f;
            }

            if (_inputKeyCodeList.Count <= 0)
            {
              _player.GenerateFukidashi();
            }
            break;
          }
        }
      }
    }

    public void Init()
    {

    }

    public void FeverJadge()
    {
      if (!_isFever && _feverComboCount >= _feverComboCountMax)
      {
        _isFever = true;
        _feverComboCount = 0;
        _feverComboCountMax *= 2;

        _feverCount = _feverCountMax;

        SoundManager._instance?.StopBGM();
        SoundManager._instance?.PlayBGM("BGM_Fever");

        _feverParticle.Play(true);

        var seq = DOTween.Sequence()
        .Append(DOTween.To(() => _defaultLight.intensity,
                            (x) => _defaultLight.intensity = x,
                            0, 0.4f))
        .Join(DOTween.To(() => _feverLight.intensity,
                            (x) => _feverLight.intensity = x,
                            150, 0.4f))
        .Play();
      }

      Debug.Log(_isFever);
      if (_isFever && _feverCount <= 0)
      {
        _isFever = false;

        _feverParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        SoundManager._instance?.StopBGM();
        SoundManager._instance?.PlayBGM("BGM_Game");

        var seq = DOTween.Sequence()
        .Append(DOTween.To(() => _defaultLight.intensity,
                            (x) => _defaultLight.intensity = x,
                            2, 0.4f))
        .Join(DOTween.To(() => _feverLight.intensity,
                            (x) => _feverLight.intensity = x,
                            0, 0.4f))
        .Play();
      }
      else if (_isFever) _feverCount--;
      else if (!_isFever) _feverComboCount++;

      if (_isFever) GenerateFeverKeyCode();
      else GenerateKeyCode();
    }

    public void GenerateKeyCode()
    {
      for (var i = 0; i < _count; i++)
      {
        var keycode = SetInputKeyCode();
        _inputKeyCodeList.Add(keycode);
        var keyImage = Instantiate(_keycodeImage, _parent.transform);
        _keyImageList.Add(keyImage);
        _keyImageList[0].transform.localScale = Vector3.one * 2f;

        if (keycode == KeyCode.UpArrow) keyImage.sprite = _keycodeImageSprites[0];
        if (keycode == KeyCode.DownArrow) keyImage.sprite = _keycodeImageSprites[1];
        if (keycode == KeyCode.LeftArrow) keyImage.sprite = _keycodeImageSprites[2];
        if (keycode == KeyCode.RightArrow) keyImage.sprite = _keycodeImageSprites[3];
      }

      _timer = 0f;
    }

    public void GenerateFeverKeyCode()
    {
      var rand = UnityEngine.Random.Range(0, _inputKeyCode.Length);

      for (var i = 0; i < _count; i++)
      {
        var keycode = _inputKeyCode[rand];
        _inputKeyCodeList.Add(keycode);
        var keyImage = Instantiate(_keycodeImage, _parent.transform);
        _keyImageList.Add(keyImage);
        _keyImageList[0].transform.localScale = Vector3.one * 2f;

        if (keycode == KeyCode.UpArrow) keyImage.sprite = _keycodeImageSprites[0];
        if (keycode == KeyCode.DownArrow) keyImage.sprite = _keycodeImageSprites[1];
        if (keycode == KeyCode.LeftArrow) keyImage.sprite = _keycodeImageSprites[2];
        if (keycode == KeyCode.RightArrow) keyImage.sprite = _keycodeImageSprites[3];
      }
      _timer = 0f;
    }

    private KeyCode SetInputKeyCode()
    {
      var rand = UnityEngine.Random.Range(0, _inputKeyCode.Length);
      var key = _inputKeyCode[rand];
      return key;
    }

    public void ClearInputKeyCode()
    {
      foreach (var k in _keyImageList)
      {
        Destroy(k.gameObject);
      }

      _keyImageList.Clear();
      _inputKeyCodeList.Clear();
    }
  }
}
