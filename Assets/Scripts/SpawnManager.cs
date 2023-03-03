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
    [SerializeField] private GameObject _heelRope;
    private int _randomPowerup;

    [SerializeField] private bool _isPlayerAlive;
    [SerializeField] private float _speed;
    [SerializeField] private bool _lowAmmo;

    [SerializeField] private GameObject[] _enemyVariant;

    private bool _spawnWaveOne, _spawnWaveTwo, _spawnWaveThree, _spawnWaveFour, _spawnWaveFive;
    private bool _spawnBoss;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private GameObject _cloudPrefab;

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
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        while (_lowAmmo == true && _isPlayerAlive == true)
        {
            Vector3 _spawnLocation = new Vector3(Random.Range(9f, -9f), 8, 0);
            GameObject ammoPickup = Instantiate(_powerups[2], _spawnLocation, Quaternion.identity);
            Debug.Log("Low Ammo Spawn");

            ammoPickup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(6f, 9f));
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
        StartCoroutine(HeelRopeSpawn());
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
                yield return new WaitForSeconds(1.3f);
            }

            yield return new WaitForSeconds(0.2f);

            //WAVE TWO
            while (_isPlayerAlive == true && _spawnWaveTwo == true)
            {
                int _randomEnemy = Random.Range(0, 2);
                GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(1.3f);
            }
            yield return new WaitForSeconds(0.2f);

            //WAVE THREE
            while (_isPlayerAlive == true && _spawnWaveThree == true)
            {
                int _randomEnemy = Random.Range(0, 3);
                GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(1.3f);
            }
            yield return new WaitForSeconds(0.3f);

            //WAVE FOUR
            while (_isPlayerAlive == true && _spawnWaveFour == true)
            {
                int _randomEnemy = Random.Range(0, _enemyVariant.Length);
                GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(1.4f);
            }
            yield return new WaitForSeconds(0.1f);

            //WAVE FIVE
            while (_isPlayerAlive == true && _spawnWaveFive == true)
            {
                int _randomEnemy = Random.Range(0, _enemyVariant.Length);
                GameObject newEnemy = Instantiate(_enemyVariant[_randomEnemy], transform.position, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(1.2f);
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
    public void BossSpawn()
    {
        _spawnWaveFive = false;
        StartCoroutine(SpawnBoss());
        _spawnBoss = true;
        Debug.Log("Boss Wave");
    }


    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(2f);

        if (_isPlayerAlive == true && _spawnBoss == true)
        {
            Instantiate(_cloudPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3.5f);
            GameObject boss = Instantiate(_bossPrefab, transform.position, Quaternion.identity);
            boss.transform.parent = _enemyContainer.transform;
        }
    }

    IEnumerator PowerupSpawn()
    {
        yield return new WaitForSeconds(8f);

        while (_isPlayerAlive == true)
        {
            _randomPowerup = Random.Range(0, 101);
            Vector3 _spawnLocation = new Vector3(Random.Range(9f, -9f), 8, 0);

            if (_randomPowerup <= 40 && _randomPowerup > 10)
            {
                int randomMidClass = Random.Range(3, 6);
                GameObject newPowerup = Instantiate(_powerups[randomMidClass], _spawnLocation, Quaternion.identity);
                newPowerup.transform.parent = _powerupContainer.transform;
            }
            else if (_randomPowerup > 40)
            {
                int randomLowClass = Random.Range(0, 3);
                GameObject newPowerup = Instantiate(_powerups[randomLowClass], _spawnLocation, Quaternion.identity);
                newPowerup.transform.parent = _powerupContainer.transform;
            }
            if (_randomPowerup <= 10)
            {
                int randomHiClass = Random.Range(6, 8);
                GameObject newPowerup = Instantiate(_powerups[randomHiClass], _spawnLocation, Quaternion.identity);
                newPowerup.transform.parent = _powerupContainer.transform;
            }
            yield return new WaitForSeconds(Random.Range(7f, 9f));
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
            yield return new WaitForSeconds(Random.Range(6f, 10f));
        }
    }

    IEnumerator HeelRopeSpawn()
    {
        yield return new WaitForSeconds(Random.Range(15, 25));
        {
            while (_isPlayerAlive == true && _spawnBoss == false)
            {
                GameObject heelRope = Instantiate(_heelRope, transform.position, Quaternion.identity);
                heelRope.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(Random.Range(15, 20));
            }
        }
    }
    public void PlayerDead()
    {
        _isPlayerAlive = false;
    }

}