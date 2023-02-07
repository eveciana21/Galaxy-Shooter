using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float _speed = 3.7f;

    void Start()
    {
        transform.position = new Vector3(13.5f, 4.3f, 0);  
    }

    void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (transform.position.x < -12)
        {
            Destroy(this.gameObject);
        }
    }
}

