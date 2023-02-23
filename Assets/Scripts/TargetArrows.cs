using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrows : MonoBehaviour
{
    [SerializeField] private Transform _player;


    // Start is called before the first frame update
    void Start()
    {
        //_player = GameObject.Find("Player").transform;
        transform.position = _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position;

        Destroy(this.gameObject, 3f);
    }
}
