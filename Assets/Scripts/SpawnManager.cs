using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(2);


    void Start()
    {
        StartCoroutine(EnemySpawn());

    }

    void Update()
    {

    }

    IEnumerator EnemySpawn()
    {

        while (true)
        {
            Vector3 _spawnLocation = new Vector3(Random.Range(9, -9), 8, 0); 
            GameObject newEnemy = Instantiate(_enemyPrefab, _spawnLocation, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return _waitForSeconds;
        }
      
        }
        

    }




