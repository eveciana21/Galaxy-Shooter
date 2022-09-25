using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _powerupContainer;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(2);
    [SerializeField] private GameObject[] _powerups;
    
    
    [SerializeField] private bool _isPlayerAlive;


    void Start()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerupSpawn());
    }


    IEnumerator EnemySpawn()
    {
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
        while (_isPlayerAlive == true)
        {
            int _randomPowerup = Random.Range(0, 3);

            Vector3 _spawnLocation = new Vector3(Random.Range(9, -9), 8, 0);
            GameObject newPowerup = Instantiate(_powerups[2], _spawnLocation, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(2,3));
        }
    }

    public void PlayerDead ()
    {
        _isPlayerAlive = false;
    }


}




