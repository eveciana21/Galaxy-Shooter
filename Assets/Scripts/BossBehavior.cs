using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private int _speed = 3;
    void Start()
    {
        transform.position = new Vector3(0, 11, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed / 2 * Time.deltaTime);

        if (transform.position.y <= 5.23f)
        {
            transform.position = new Vector3(0, 5.23f, 0);
        }
    }
}
