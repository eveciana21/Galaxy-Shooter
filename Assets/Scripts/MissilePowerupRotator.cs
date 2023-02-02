using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePowerupRotator : MonoBehaviour
{
    [SerializeField] private GameObject _centralPoint;
    [SerializeField] private float _speed;

    void Update()
    {
        transform.RotateAround(_centralPoint.transform.position, Vector3.forward, _speed * Time.deltaTime);
    }
}
