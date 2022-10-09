using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private int powerupID;

    [SerializeField] AudioClip _powerupAudio;
    [SerializeField] private float _volume = 1.0f;


    void Start()
    {
        transform.position=new Vector3(Random.Range(-9, 9),8f, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerupAudio, transform.position, _volume);

            if (player !=null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                }


                Destroy(this.gameObject);
                

            }


        }
    }

    



}
