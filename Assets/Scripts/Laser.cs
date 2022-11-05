using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private int _speed;
    [SerializeField] private bool _enemyFiredLaser;
    //[SerializeField] private bool _heatSeakingMissiles;
    //[SerializeField] private Transform _enemy;




    void Update()
    {
        if (_enemyFiredLaser == false)
        {
            MoveUp();
        }
        /*else if (_heatSeakingMissiles == true)
        {
            HeatSeakingMissiles();
        }*/
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

    /*public void HeatSeakingMissiles()
    {
        _heatSeakingMissiles = true;

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        transform.LookAt(_enemy);

        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }
    */

}
