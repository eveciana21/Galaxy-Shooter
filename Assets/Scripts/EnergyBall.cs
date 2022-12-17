using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _explosion;
    private CameraShake _cameraShake;
    void Start()
    {
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        StartCoroutine(ExplodeTimer());
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(_explosion, transform.position, Quaternion.identity);
        _cameraShake.EnemyScreenShake();
        Destroy(this.gameObject,0.1f);
    }
}
