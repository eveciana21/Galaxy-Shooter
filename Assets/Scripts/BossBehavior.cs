using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private int _speed = 3;
    [SerializeField] private GameObject _leftClaw, _rightClaw;
    private Vector3 _originalPos;
    private Vector3 _originalRightClawPos;
    private Vector3 _originalLeftClawPos;

    [SerializeField] private bool _clawReachedBottom;
    [SerializeField] private bool _beginRightClawSwipe;
    private int _direction = 1;
    private int _movementDirection = 1;
    private bool _canMove;
    private float _canFire = -1f;
    private float _fireRate;

    [SerializeField] private GameObject _bossLaser;
    [SerializeField] private GameObject _eyeParticle;

    private Animator _leftClawSwipe;
    private Animator _rightClawSwipe;

    [SerializeField] private ParticleSystem _leftClawParticle;
    [SerializeField] private ParticleSystem _rightClawParticle;


    void Start()
    {
        transform.position = new Vector3(0, 6, 0);
        _eyeParticle.gameObject.SetActive(false);

        _leftClawSwipe = _leftClaw.GetComponent<Animator>();
        _rightClawSwipe = _rightClaw.GetComponent<Animator>();

        _leftClawSwipe.enabled = false;
        _rightClawSwipe.enabled = false;

        _leftClawParticle.gameObject.SetActive(false);
        _rightClawParticle.gameObject.SetActive(false);

    }

    void Update()
    {
        if (_canMove == true)
        {
            SideMovement();
            StartCoroutine(StartBossSequences());
        }
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if (transform.position.y <= 0f)
        {
            _canMove = true;
        }


    }
    private void SideMovement()
    {
        if (_canMove == true)
        {
            FireLaser();

            if (transform.position.x <= -4.5f)
            {
                _movementDirection = 1;
            }
            else if (transform.position.x >= 4.5f)
            {
                _movementDirection = -1;
            }
            //transform.Translate(Vector3.right * _speed * _movementDirection * Time.deltaTime);
        }
    }


    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 4f);
            _canFire = Time.time + _fireRate;

            StartCoroutine(LaserWarning());
        }
    }


    IEnumerator LaserWarning()
    {
        _eyeParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        Instantiate(_bossLaser, transform.position + new Vector3(0, 5.6f, 0), Quaternion.identity);
        _eyeParticle.gameObject.SetActive(false);
    }


    IEnumerator StartBossSequences()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ClawSwipe());
        yield return new WaitForSeconds(5f);
    }


    IEnumerator ClawSwipe()
    {
        _leftClawSwipe.enabled = true;
        yield return new WaitForSeconds(0.6f);
        _leftClawParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        _rightClawSwipe.enabled = true;
        yield return new WaitForSeconds(0.6f);
        _rightClawParticle.gameObject.SetActive(true);
    }






}








