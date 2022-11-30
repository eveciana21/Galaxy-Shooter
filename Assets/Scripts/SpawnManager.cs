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

    private bool _spawnWaveOne, _spawnWaveTwo, _spawnWaveThree, _spawnWaveFour, _spawnWaveFive;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.Log("UI Manager is Null");
        }
    }

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
        _spawnWaveOne = true;
        _uiManager.WaveOneUI();

        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerupSpawn());
        StartCoroutine(AsteroidSpawn());
    }


    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(2f);
       
        //WAVE ONE
        while (_isPlayerAlive == true && _spawnWaveOne == true)
        {
            GameObject newEnemy = Instantiate(_enemyVariant[0], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2f);
        
        //WAVE TWO
        while (_isPlayerAlive == true && _spawnWaveTwo == true)
        {
            int _randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.7f);
        }
        yield return new WaitForSeconds(1.2f);

        //WAVE THREE
        while (_isPlayerAlive && _spawnWaveThree == true)
        {
            int _randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.45f);
        }
        yield return new WaitForSeconds(1);

        //WAVE FOUR
        while (_isPlayerAlive && _spawnWaveFour == true)
        {
            int _randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.25f);
        }
        yield return new WaitForSeconds(0.88f);

        //WAVE FIVE
        while (_isPlayerAlive && _spawnWaveFive == true)
        {
            int _randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(0.75f);
        }
    }

    public void WaveTwo()
    {
        _spawnWaveOne = false;
        _spawnWaveTwo = true;
        Debug.Log("Wave Two");
    }
    public void WaveThree()
    {
        _spawnWaveTwo = false;
        _spawnWaveThree = true;
        Debug.Log("Wave Three");
    }
    public void WaveFour()
    {
        _spawnWaveThree = false;
        _spawnWaveFour = true;
        Debug.Log("Wave Four");
    }
    public void WaveFive()
    {
        _spawnWaveFour = false;
        _spawnWaveFive = true;
        Debug.Log("Wave Five");
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




