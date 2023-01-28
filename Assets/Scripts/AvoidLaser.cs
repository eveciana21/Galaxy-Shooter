using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidLaser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Enemy enemy = GetComponentInParent<Enemy>();
            enemy.AvoidLaser();
        }
    }
}
