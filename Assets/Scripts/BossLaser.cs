using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{

    private float _speed = 12f;

    void Update()
    {
        transform.Translate(-transform.up * _speed * Time.deltaTime);

        if (transform.position.y < -4f)
        {
            Destroy(this.gameObject);
        }
    }
}
