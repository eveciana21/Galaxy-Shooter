using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerupContainer;
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private GameObject[] _powerups;
    [SerializeField] private bool _isPlayerAlive;
    [SerializeField] private float _speed;
    [SerializeField] private bool _lowAmmo;

    [SerializeField] private GameObject[] _enemyVariant;

    public void StartSpawningAmmo()
    {
        _lowAmmo = true;
        StartCoroutine(AmmoStartSpawn());
    }

    IEnumerator AmmoStartSpawn()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));

        while (_lowAmmo == true)
        {
            Vector3 _spawnLocation = new Vector3(Random.Range(9f, -9f), 8, 0);
            GameObject ammoPickup = Instantiate(_powerups[3], _spawnLocation, Quaternion.identity);
            ammoPickup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(6f, 11f));
        }
    }

    public void EnoughAmmo()
    {
        _lowAmmo = false;
    }

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerupSpawn());
        StartCoroutine(AsteroidSpawn());
    }


    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(1f);

        while (_isPlayerAlive == true)
        {
            int _randomEnemy = Random.Range(0, 2);
             GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator PowerupSpawn()
    {
        yield return new WaitForSeconds(10f);

        while (_isPlayerAlive == true)
        {
            int _randomPowerup = Random.Range(0, 6);
            Vector3 _spawnLocation = new Vector3(Random.Range(9f, -9f), 8, 0);
            GameObject newPowerup = Instantiate(_powerups[_randomPowerup], _spawnLocation, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(7f, 11f));
        }
    }

    IEnumerator AsteroidSpawn()
    {
        yield return new WaitForSeconds(8);

        while (_isPlayerAlive == true)
        {
            Vector3 _spawnLocation = new Vector3(Random.Range(-9f, 9f), 8, 0);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, _spawnLocation, Quaternion.identity);
            newAsteroid.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
    }

    public void PlayerDead()
    {
        _isPlayerAlive = false;
    }

}




