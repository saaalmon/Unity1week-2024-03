using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UniRx;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
  public static SoundManager _instance;

  [SerializeField]
  private AudioMixer _audioMixer;
  [SerializeField]
  private AudioMixerGroup _bgmAudioMixerGroup;
  [SerializeField]
  private AudioMixerGroup _seAudioMixerGroup;
  [SerializeField]
  private SoundMasterData _bgmSoundMasterData;
  [SerializeField]
  private SoundMasterData _seSoundMasterData;

  [SerializeField]
  private int _bgmCount;
  [SerializeField]
  private int _seCount;

  [SerializeField]
  private float _bgmVolume;
  [SerializeField]
  private float _seVolume;

  public IReadOnlyReactiveProperty<float> BgmVol => _bgmVol;
  private FloatReactiveProperty _bgmVol = new FloatReactiveProperty(0);

  public IReadOnlyReactiveProperty<float> SeVol => _seVol;
  private FloatReactiveProperty _seVol = new FloatReactiveProperty(0);

  private List<AudioSource> _bgmAudioSourceList = new List<AudioSource>();
  private List<AudioSource> _seAudioSourceList = new List<AudioSource>();

  private Dictionary<string, AudioClip> _bgmSoundDictionary = new Dictionary<string, AudioClip>();
  private Dictionary<string, AudioClip> _seSoundDictionary = new Dictionary<string, AudioClip>();

  void Awake()
  {
    if (_instance == null)
    {
      _instance = this;
      DontDestroyOnLoad(gameObject);

      _bgmVol
      .Skip(1)
      .Subscribe(x =>
      {
        _audioMixer.SetFloat("BGMParam", x);
      })
      .AddTo(this);

      _seVol
      .Skip(1)
      .Subscribe(x =>
      {
        _audioMixer.SetFloat("SEParam", x);
      })
      .AddTo(this);
    }
    else
    {
      Destroy(gameObject);
    }

    for (var i = 0; i < _bgmCount; i++)
    {
      var audioSource = gameObject.AddComponent<AudioSource>();
      audioSource.outputAudioMixerGroup = _bgmAudioMixerGroup;

      _bgmAudioSourceList.Add(audioSource);
    }

    foreach (var s in _bgmSoundMasterData.SoundDatas)
    {
      _bgmSoundDictionary.Add(s.Audio.name, s.Audio);
    }

    for (var i = 0; i < _seCount; i++)
    {
      var audioSource = gameObject.AddComponent<AudioSource>();
      audioSource.outputAudioMixerGroup = _seAudioMixerGroup;

      _seAudioSourceList.Add(audioSource);
    }

    foreach (var s in _seSoundMasterData.SoundDatas)
    {
      _seSoundDictionary.Add(s.Audio.name, s.Audio);
    }
  }

  void Start()
  {
    SetBgmVolume(_bgmVolume);
    SetSeVolume(_seVolume);
  }

  private AudioSource GetUnusedBgmAudioSource()
  {
    foreach (var a in _bgmAudioSourceList)
    {
      if (!a.isPlaying) return a;
    }

    return null;
  }

  private AudioSource GetUnusedSeAudioSource()
  {
    foreach (var a in _seAudioSourceList)
    {
      if (!a.isPlaying) return a;
    }

    return null;
  }

  public void PlayBGM(string name, float pitch = 1, bool isloop = true)
  {
    if (_bgmSoundDictionary.TryGetValue(name, out var audio))
    {
      var audioSource = GetUnusedBgmAudioSource();
      if (audioSource == null) return;

      audioSource.clip = audio;
      audioSource.pitch = pitch;
      audioSource.loop = isloop;
      audioSource.Play();
      Debug.Log(audio);
    }
    else
    {
      Debug.Log("登録無し");
      return;
    }
  }

  public void PlaySE(string name, float pitch = 1)
  {
    if (_seSoundDictionary.TryGetValue(name, out var audio))
    {
      var audioSource = GetUnusedSeAudioSource();
      if (audioSource == null) return;

      audioSource.clip = audio;
      audioSource.pitch = pitch;

      audioSource.Play();
      Debug.Log(audio);
    }
    else
    {
      Debug.Log("登録無し");
      return;
    }
  }

  public void StopBGM()
  {
    foreach (var s in _bgmAudioSourceList)
    {
      s.Stop();
    }
  }

  public void StopSE()
  {
    foreach (var s in _seAudioSourceList)
    {
      s.Stop();
    }
  }

  public float SetBgmVolume(float volume)
  {
    return _bgmVol.Value = volume;
  }

  public float SetSeVolume(float volume)
  {
    return _seVol.Value = volume;
  }
}
