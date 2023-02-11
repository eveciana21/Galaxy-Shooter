using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelRope : MonoBehaviour
{
    private float _speed = 3;
    [SerializeField] private GameObject _laserBeam;
    [SerializeField] private GameObject _static;
    private int _randomStart;
    private int _direction = -1;

    void Start()
    {
        _randomStart = Random.Range(0, 2);

        if (_randomStart == 0)
        {
            transform.position = new Vector3(11.5f, Random.Range(-2f, 2f), 0);
        }
        else if (_randomStart == 1)
        {
            transform.position = new Vector3(-11.5f, Random.Range(-2f, 2f), 0);
        }

        StartCoroutine(LaserTiming());
    }

    void Update()
    {
        if (_randomStart == 0)
        {
            _direction = 1;

            if (transform.position.x < -13)
            {
                Destroy(this.gameObject);
            }
        }
        else if (_randomStart == 1)
        {
            _direction = -1;

            if (transform.position.x > 13)
            {
                Destroy(this.gameObject);
            }
        }

        transform.Translate(Vector3.left * _speed * _direction * Time.deltaTime);
    }

    IEnumerator LaserTiming()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 4));
            _laserBeam.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            _static.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            _static.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            _laserBeam.gameObject.SetActive(true);
        }
    }
}
