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
    private int _randomPowerup;

    [SerializeField] private bool _isPlayerAlive;
    [SerializeField] private float _speed;
    [SerializeField] private bool _lowAmmo;

    [SerializeField] private GameObject[] _enemyVariant;

    private bool _spawnWaveOne, _spawnWaveTwo, _spawnWaveThree, _spawnWaveFour, _spawnWaveFive;
    private bool _spawnBoss;
    [SerializeField] private GameObject _bossPrefab;

    private UIManager _uiManager;


    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.Log("UI Manager is Null");
        }
    }


    //Create a difficulty level setting
    //SIMPLE, REGULAR 


    public void StartSpawningAmmo()
    {
        _lowAmmo = true;
        StartCoroutine(AmmoStartSpawn());
    }

    IEnumerator AmmoStartSpawn()
    {
        yield return new WaitForSeconds(Random.Range(3f, 7f));

        while (_lowAmmo == true && _isPlayerAlive == true)
        {
            Vector3 _spawnLocation = new Vector3(Random.Range(9f, -9f), 8, 0);
            GameObject ammoPickup = Instantiate(_powerups[2], _spawnLocation, Quaternion.identity);
            Debug.Log("Low Ammo Spawn");

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
            int _randomEnemy = Random.Range(0, 2);

            GameObject newEnemy = Instantiate(_enemyVariant[0], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(1f);

        //WAVE TWO
        while (_isPlayerAlive == true && _spawnWaveTwo == true)
        {
            int _randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.7f);
        }
        yield return new WaitForSeconds(1f);

        //WAVE THREE
        while (_isPlayerAlive && _spawnWaveThree == true)
        {
            int _randomEnemy = Random.Range(0, 3);
            GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.65f);
        }
        yield return new WaitForSeconds(1f);

        //WAVE FOUR
        while (_isPlayerAlive && _spawnWaveFour == true)
        {
            int _randomEnemy = Random.Range(0, _enemyVariant.Length);
            GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(0.88f);

        //WAVE FIVE
        while (_isPlayerAlive && _spawnWaveFive == true)
        {
            int _randomEnemy = Random.Range(0, _enemyVariant.Length);
            GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.35f);
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
    public void BossSpawn ()
    {
        _spawnWaveOne = false; // <<< temporary line of code 
        _spawnWaveFive = false;
        _spawnBoss = true;
        StartCoroutine(SpawnBoss());
        Debug.Log("Boss Wave");
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(5f);

        if (_spawnBoss == true)
        {
            GameObject boss = Instantiate(_bossPrefab, transform.position, Quaternion.identity);
            boss.transform.parent = _enemyContainer.transform;
        }
    }

    IEnumerator PowerupSpawn()
    {
        yield return new WaitForSeconds(9f);

        while (_isPlayerAlive == true)
        {
            _randomPowerup = Random.Range(0, 101);
            Vector3 _spawnLocation = new Vector3(Random.Range(9f, -9f), 8, 0);

            if (_randomPowerup <= 35 && _randomPowerup > 5)
            {
                int randomMidClass = Random.Range(3, 6);
                GameObject newPowerup = Instantiate(_powerups[randomMidClass], _spawnLocation, Quaternion.identity);
                newPowerup.transform.parent = _powerupContainer.transform;
            }
            else if (_randomPowerup > 35)
            {
                int randomLowClass = Random.Range(0, 3);
                GameObject newPowerup = Instantiate(_powerups[randomLowClass], _spawnLocation, Quaternion.identity);
                newPowerup.transform.parent = _powerupContainer.transform;
            }
            if (_randomPowerup <= 5)
            {
                int randomHiClass = Random.Range(6, 8);
                GameObject newPowerup = Instantiate(_powerups[randomHiClass], _spawnLocation, Quaternion.identity);
                newPowerup.transform.parent = _powerupContainer.transform;
            }
            yield return new WaitForSeconds(Random.Range(7f, 11f));
        }
    }

    IEnumerator AsteroidSpawn()
    {
        yield return new WaitForSeconds(8);

        while (_isPlayerAlive == true && _spawnBoss == false)
        {
            Vector3 _spawnLocation = new Vector3(Random.Range(-9f, 9f), 8, 0);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, _spawnLocation, Quaternion.identity);
            newAsteroid.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(9f, 14f));
        }
    }

    public void PlayerDead()
    {
        _isPlayerAlive = false;
    }

    

}