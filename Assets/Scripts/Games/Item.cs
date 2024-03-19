using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  private Rigidbody rb;

  [SerializeField]
  private float _speed;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Init()
  {
    rb = GetComponent<Rigidbody>();

    rb.velocity = Vector3.left * _speed;

    Destroy(gameObject, 5.0f);
  }
}
