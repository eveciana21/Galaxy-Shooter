using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private bool _enemyFiredLaser;

    void Update()
    {
        if (_enemyFiredLaser == false)
        {
            MoveUp();
        }

        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 7.5f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5)
        {
            Destroy(this.gameObject);
        }
    }

    public void EnemyFiredLaser()
    {
        _enemyFiredLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Easy")
        {
            PlayerPrefs.GetInt("Difficulty", 0);
            Debug.Log("Easy");
        }
        if (other.tag == "Moderate")
        {
            PlayerPrefs.GetInt("Difficulty", 1);
            Debug.Log("Moderate");
        }
        if (other.tag == "Hard")
        {
            PlayerPrefs.GetInt("Difficulty", 2);
            Debug.Log("Hard");
        }
    }


}

