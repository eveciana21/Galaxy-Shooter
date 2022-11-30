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
            Destroy(this.gameObject);

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            
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

    public void BrigadeFiredLaser()
    {
        _enemyFiredLaser = false;
    }

}
