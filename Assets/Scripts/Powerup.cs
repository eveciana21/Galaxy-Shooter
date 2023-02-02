using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private int powerupID;

    [SerializeField] AudioClip _powerupAudio;
    private int _volume = 1;

    [SerializeField] private GameObject _explosionPrefab;

    private GameObject _enemy;
    private GameObject _shieldedEnemy;


    void Start()
    {
        _enemy = GameObject.Find("Enemy");
        _shieldedEnemy = GameObject.Find("Enemy Shielded");
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

            if (player != null)
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
                        player.AmmoPickup();
                        break;
                    case 3:
                        player.ShieldActive();
                        break;
                    case 4:
                        player.HealthPickup();
                        break;
                    case 5:
                        player.NegativePowerupSlow();
                        break;
                    case 6:
                        player.FighterBrigadePowerup();
                        break;
                    case 7:
                        player.HeatSeakingMissiles();
                        break;
                }
                Destroy(this.gameObject);
            }
        }

        if (other.tag == "Enemy Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.05f);
        }

        if (other.tag == "Explosion")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.05f);
        }

        if (other.tag == "Collider")
        {
            if (_enemy != null)
            {
                Enemy enemy = _enemy.GetComponent<Enemy>();
                enemy.FireAtPowerup();
            }

            if (_shieldedEnemy != null)
            {
                EnemyShielded shieldedEnemy = _shieldedEnemy.GetComponent<EnemyShielded>();
                shieldedEnemy.FireAtPowerup();
            }
        }
    }
}
