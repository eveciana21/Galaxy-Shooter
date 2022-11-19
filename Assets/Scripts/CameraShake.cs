using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float _xShake;
    private float _yShake;

    private float _shakeDuration;




    private void Start()
    {
        transform.position = new Vector3(0, 1, -10);
    }
    public void CameraShaking()
    {
        StartCoroutine(CameraShakingTime());
    }

    IEnumerator CameraShakingTime()
    {
        Vector3 _originalPos = transform.position;
        _shakeDuration = Time.time + 0.22f;

        while (_shakeDuration > Time.time)
        {
            _xShake = Random.Range(-0.1f, 0.1f);
            _yShake = Random.Range(0.9f, 1.1f);
            Vector3 _shakeDirection = new Vector3(_xShake, _yShake, transform.position.z);
            transform.position = _shakeDirection;
            yield return new WaitForEndOfFrame();
        }
        transform.position = _originalPos;
    }
    
    public void FighterBrigadeCameraShake()
    {
        StartCoroutine(FighterBrigadeShake());
    }
    IEnumerator FighterBrigadeShake()
    {
        Vector3 _originalPos = transform.position;
        _shakeDuration = Time.time + 0.2f;

        while (_shakeDuration > Time.time)
        {
            _xShake = Random.Range(-0.025f, 0.025f);
            _yShake = Random.Range(0.985f, 1.025f);
            Vector3 _shakeDirection = new Vector3(_xShake, _yShake, transform.position.z);
            transform.position = _shakeDirection;
            yield return new WaitForEndOfFrame();
        }
        transform.position = _originalPos;
    }

}

