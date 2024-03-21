using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  private Rigidbody rb;

  [SerializeField]
  private float _speed;

  public void Init()
  {
    rb = GetComponent<Rigidbody>();

    rb.velocity = Vector3.right * _speed;

    Destroy(gameObject, 5.0f);
  }
}
