using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game
{
  public class PlayerManager : MonoBehaviour
  {
    [SerializeField]
    private Player _player;
    [SerializeField]
    private KeyCode[] _inputKeyCode;
    [SerializeField]
    private int _count;

    public List<KeyCode> InputKeyCodeList => _inputKeyCodeList;
    private List<KeyCode> _inputKeyCodeList = new List<KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
      GenerateKeyCode();
    }

    // Update is called once per frame
    void Update()
    {
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
              if (_inputKeyCodeList.Count <= 0)
              {
                _player.GenerateFukidashi();
                GenerateKeyCode();
                Debug.Log("Complite!");
              }

              break;
            }
          }
        }
      }
    }

    public void GenerateKeyCode()
    {
      for (var i = 0; i < _count; i++)
      {
        _inputKeyCodeList.Add(SetInputKeyCode());
      }
    }

    private KeyCode SetInputKeyCode()
    {
      var rand = UnityEngine.Random.Range(0, _inputKeyCode.Length);
      var key = _inputKeyCode[rand];
      return key;
    }
  }
}
