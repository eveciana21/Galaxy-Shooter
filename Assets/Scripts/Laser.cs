using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{


    [SerializeField] private int _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


       transform.Translate(Vector3.up * _speed * Time.deltaTime);

       if (transform.position.y > 7.5f)
        {
            Destroy(this.gameObject);
        }
        
    }
}
//Can write this.gameObject or simply gameObject. Writing "this" just helps you know
