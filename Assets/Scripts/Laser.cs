using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int _speed;

    void Update()
    {
       transform.Translate(Vector3.up * _speed * Time.deltaTime);

       if (transform.position.y > 7.5f)
        {
            Destroy(this.gameObject);
        }
       
    }
}
