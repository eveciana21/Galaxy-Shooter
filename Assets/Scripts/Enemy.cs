using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Player _player;

    private Animator _destroyAnim;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

        _destroyAnim = GetComponent<Animator>();

        transform.position = new Vector3(Random.Range(9, -9), 7.5f, 0);

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if Enemy position reaches bottom of screen
        if (transform.position.y <= -6.5f)
        {
            //spawn at random x position
            float randomX = Random.Range(9, -9);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            if (_destroyAnim != null)
            {
                _destroyAnim.SetTrigger("OnEnemyDestroy");
                _speed = 0.5f;
                Destroy(this.gameObject, 1.15f);
            }
        }

        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddToScore(50);
            }

            if (_destroyAnim != null)
            {
                _destroyAnim.SetTrigger("OnEnemyDestroy");
                _speed = 0.5f;
                Destroy(this.gameObject, 1.15f);
            }

        }




    }




}
