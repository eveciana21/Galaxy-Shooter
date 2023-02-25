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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fighters")
        {
            Destroy(other.gameObject, 0.05f);
            Destroy(this.gameObject, 0.05f);
        }
    }
}
