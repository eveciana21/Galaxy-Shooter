using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float _speed = 8f;
    //private float _rotatingSpeed = 200f;
    [SerializeField] private GameObject _enemy;
    private Rigidbody2D _rb;
    //private float _offsetAngle = -90f;


    //WORK IN PROGRESS


    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }

        if (_enemy == null)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y > 7.5f)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {

        }
        
        
        
        
        /*{
            transform.position = Vector3.MoveTowards(transform.position, _enemy.transform.position, _speed * Time.deltaTime);
            Vector3 offset = transform.position - _enemy.transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);

            if(Vector3.Distance(transform.position, _enemy.transform.position)<0.001f)
            {
                _enemy.transform.position *= 1f;
            }    
        }*/
        
              
        
        /* {
            Vector3 pointToTarget = (transform.position - _enemy.transform.position).normalized;
            float value = Vector3.Cross(pointToTarget, transform.up).z;
            _rb.angularVelocity = _rotatingSpeed * value;
            _rb.velocity = transform.up * _speed;
        }*/



        /*{
            transform.position=Vector3.MoveTowards(transform.position, _enemy.transform.position, _speed * Time.deltaTime);
            Vector3 playerToEnemy = (_enemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(playerToEnemy.x, playerToEnemy.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + _offsetAngle));
        }*/

    }
}
