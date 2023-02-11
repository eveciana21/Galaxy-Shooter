using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private int _speed = 3;
    [SerializeField] private GameObject _leftClaw, _rightClaw;
    [SerializeField] private GameObject _head;

    [SerializeField] private bool _clawReachedBottom;
    [SerializeField] private bool _beginRightClawSwipe;
    private int _direction = 1;
    private int _movementDirection = 1;
    private bool _canMove;
    private float _canFire = -1f;
    private float _fireRate;

    private Animator _mouthOpenAnim;

    [SerializeField] private GameObject _bossLaser;
    [SerializeField] private GameObject _eyeParticle;

    private Animator _leftClawSwipe;
    private Animator _rightClawSwipe;

    [SerializeField] private ParticleSystem _leftClawParticle;
    [SerializeField] private ParticleSystem _rightClawParticle;

    [SerializeField] private GameObject _targetArrows;
    [SerializeField] private GameObject _spineBall;
    private bool _fireSpineBall;

    private Transform _player;

    private int _shotsFired = 4;
    private bool _canBeginFiring;

    private bool _bossRush;


    void Start()
    {
        _player = GameObject.Find("Player").transform;
        transform.position = new Vector3(0, 6, 0);
        _eyeParticle.gameObject.SetActive(false);

        _leftClawSwipe = _leftClaw.GetComponent<Animator>();
        _rightClawSwipe = _rightClaw.GetComponent<Animator>();

        _mouthOpenAnim = _head.GetComponent<Animator>();

        _leftClawSwipe.enabled = false;
        _rightClawSwipe.enabled = false;

        _leftClawParticle.gameObject.SetActive(false);
        _rightClawParticle.gameObject.SetActive(false);

        _targetArrows.gameObject.SetActive(false);

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
        if (transform.position.y <= 0f && _bossRush == false)
        {
            _canMove = true;
        }

        if (_canBeginFiring == true)
        {
            FireSpineBalls();
        }

        //FireSpineBall();
    }
    private void SideMovement()
    {
        if (_canMove == true)
        {
            //FireLaser();

            if (transform.position.x <= -3.5f)
            {
                _movementDirection = 1;
            }
            else if (transform.position.x >= 3.5f)
            {
                _movementDirection = -1;
            }
            transform.Translate(Vector3.right * _speed * _movementDirection * Time.deltaTime);
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
        /*StartCoroutine(ClawSwipe());

        if (_fireSpineBall == false)
        {            
            StartCoroutine(Arrows());
            _fireSpineBall = true;
        }*/


        BossRush();
        yield return new WaitForSeconds(10f);

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

    IEnumerator Arrows()
    {
        _targetArrows.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        _targetArrows.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _canBeginFiring = true;
        _mouthOpenAnim.SetBool("_shotsFired", true);
    }

    /*private void TargetArrows()
    {
        //StartCoroutine(Arrows());
    }*/

    private void FireSpineBalls()
    {
        if (Time.time > _canFire)
        {
            _fireRate = 0.7f;
            _canFire = Time.time + _fireRate;

            if (_shotsFired >= 1)
            {
                _shotsFired--;
                Instantiate(_spineBall, _head.transform.position + new Vector3(0, -2.4f, 0), Quaternion.identity);

                if (_shotsFired == 0)
                {
                    _mouthOpenAnim.SetBool("_shotsFired", false);
                }
            }
            else
            {
                _canBeginFiring = false;
                _shotsFired = 4;
            }
        }
    }

    private void BossRush()
    {
        _canMove = false;
        _bossRush = true;

        transform.Translate(Vector3.down * _speed * 2 * Time.deltaTime);

        if (transform.position.y < -15f)
        {
            transform.position = new Vector3(0, 6, 0);
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            _bossRush = false;
            _canMove = true;
        }
    }
}








