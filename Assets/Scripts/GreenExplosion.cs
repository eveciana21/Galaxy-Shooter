using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenExplosion : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 1.5f);
    }

}
