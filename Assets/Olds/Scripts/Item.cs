using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  private Rigidbody rb;

  [SerializeField]
  private int _money;

  public int Money => _money;

  public void Init(float speed)
  {
    rb = GetComponent<Rigidbody>();

    rb.velocity = Vector3.right * speed;

    Destroy(gameObject, 5.0f);
  }
}
