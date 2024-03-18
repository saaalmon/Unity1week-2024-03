using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    SoundManager._instance.PlaySE("Sound_1");
  }

  // Update is called once per frame
  void Update()
  {

  }
}
