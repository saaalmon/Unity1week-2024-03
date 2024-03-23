using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
  [SerializeField]
  private Image[] _keyCodeIcons;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetKeyCode(KeyCode[] keyCodes)
  {
    for (var i = 0; i < keyCodes.Length; i++)
    {
        
    }
  }
}
