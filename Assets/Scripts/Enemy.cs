using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    void Start()
    {
        transform.position = new Vector3 (Random.Range(9,-9),8,0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if Enemy position reaches bottom of screen
        if (transform.position.y <= -5)
        {
            //spawn at random x position
            float randomX = Random.Range(9, -9);
            transform.position = new Vector3 (randomX, 8, 0);
        }       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            } 
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }

}
