using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [SerializeField] private float _speed;
    [SerializeField] private bool isDestroyed=true;
    void Start()
    {
        transform.position = new Vector3 (Random.Range(9,-9),8,0);

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if player position reaches bottom of screen

        if (transform.position.y <= -5 || isDestroyed==true)
        {
            //spawn at random x position
            float randomX = Random.Range(9, -9);
            transform.position = new Vector3 (randomX, 8, 0);
            isDestroyed = this.gameObject == null;

        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }


}
