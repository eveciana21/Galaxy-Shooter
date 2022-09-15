using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 2.5f;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private float _canFire = 0f;
    [SerializeField] private float _fireRate = 1f;






   
    
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        ControlMovement();



        //if we hit the space key AND the time lapsed is greater than time lapsed + 1 = instantiate laser
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
         // _canFire = Time lapsed + 1
            _canFire = Time.time + _fireRate;

            Instantiate(_laserPrefab, transform.position + new Vector3 (0, 0.6f, 0), Quaternion.identity);





            


            //transform.position is spawning the object at the player position



        }

    }

     void ControlMovement()
     {

       float horizontal = Input.GetAxis("Horizontal");
        //new Vector3 (1,0,0) * 0 * 3.5f * real time
        //transform.Translate(Vector3.right * horizontal * _speed * Time.deltaTime);

       float vertical = Input.GetAxis("Vertical");
        //new Vector3 (0,1,0) * 0 * 3.5f * real time
        //transform.Translate(Vector3.up * vertical * _speed * Time.deltaTime);

       Vector3 direction = new Vector3(horizontal, vertical, 0);
       transform.Translate(direction * _speed * Time.deltaTime);
       //transform.Translate(new Vector3(horizontal, vertical, 0) * _speed * Time.deltaTime);

        //X position
        if (transform.position.x > 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.25f)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }

        //Y position
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.87f, 5.9f), 0);
    }

   
}
