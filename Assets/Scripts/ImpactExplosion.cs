using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactExplosion : MonoBehaviour
{
    private Collider2D _collider;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        StartCoroutine(ColliderDisable());
        Destroy(this.gameObject, 1f);
    }

    IEnumerator ColliderDisable()
    {
        yield return new WaitForSeconds(0.4f);
        _collider.enabled = false;
    }
}
