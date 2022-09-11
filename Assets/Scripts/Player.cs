using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 2.5f;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        //new Vector3 (1,0,0) * 0 * 3.5f * real time
        //transform.Translate(Vector3.right * horizontal * _speed * Time.deltaTime);

        float vertical = Input.GetAxis("Vertical");
        //new Vector3 (0,1,0) * 0 * 3.5f * real time
        //transform.Translate(Vector3.up * vertical * _speed * Time.deltaTime);

        //transform.Translate(new Vector3(horizontal, vertical, 0) * _speed * Time.deltaTime);

        Vector3 direction = new Vector3 (horizontal, vertical, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        
    }

      
}
