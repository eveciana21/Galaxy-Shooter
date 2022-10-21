using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerupContainer;
    [SerializeField] private GameObject _asteroidPrefab;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1.5f);
    [SerializeField] private GameObject[] _powerups;
    [SerializeField] private bool _isPlayerAlive;
    [SerializeField] private float _speed;


    public void StartSpawning()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerupSpawn());
        StartCoroutine(AsteroidSpawn());
    }


    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(91.5f);
        
        while (_isPlayerAlive == true)
        {
            Vector3 _spawnLocation = new Vector3(Random.Range(9, -9), 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, _spawnLocation, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return _waitForSeconds;
        }
    }

    IEnumerator PowerupSpawn()
    {
        yield return new WaitForSeconds(12);
        
        while (_isPlayerAlive == true)
        {
            int _randomPowerup = Random.Range(0, 3);
            Vector3 _spawnLocation = new Vector3(Random.Range(9, -9), 8, 0);
            GameObject newPowerup = Instantiate(_powerups[3], _spawnLocation, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(10, 15));
        }
    }

    IEnumerator AsteroidSpawn ()
    {
        yield return new WaitForSeconds(8);

        while (_isPlayerAlive == true)
        {
            Vector3 _spawnLocation = new Vector3(Random.Range(-9, 9), 8, 0);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, _spawnLocation, Quaternion.identity);
            newAsteroid.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds (Random.Range (3,8));

        }
    }


    public void PlayerDead()
    {
        _isPlayerAlive = false;
    }





}




