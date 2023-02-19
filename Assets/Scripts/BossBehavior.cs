using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private float _speed = 3f;
    private float _multipliedSpeed = 10f;

    [SerializeField] private GameObject _leftClaw, _rightClaw, _bothClaws;
    [SerializeField] private GameObject _head;

    private Vector3 _startPos;

    [SerializeField] private bool _clawReachedBottom;
    [SerializeField] private bool _beginRightClawSwipe;

    private int _movementDirection = 1;
    private float _canFire = -1f;
    private float _fireRate;

    private float _canFireSpineBall;

    [SerializeField] private GameObject _bossLaser;
    [SerializeField] private GameObject _eyeParticle;

    private Animator _mouthOpenAnim;
    private Animator _clawsAnim;
    private Animator _mainBossAnim;

    [SerializeField] private ParticleSystem _leftClawParticle, _rightClawParticle;

    [SerializeField] private ParticleSystem _leftRushingParticle, _rightRushingParticle;

    [SerializeField] private ParticleSystem _headRushingParticle;

    [SerializeField] private GameObject _targetArrows;
    [SerializeField] private GameObject _spineBall;

    [SerializeField] private bool _fireSpineBall;

    [SerializeField] private int _shotsFired = 4;

    [SerializeField] private bool _canBeginFiring;


    private CameraShake _cameraShake;

    [SerializeField] private bool _canShakeCamera;

    [SerializeField] private bool _bossEnteredGame;

    private bool _bossHasDied;

    [SerializeField] private bool _canSwipe;
    [SerializeField] private bool _trySwiping;

    private float _newAttack;

    [SerializeField] private int _randomAttack;

    [SerializeField] private bool _bossRush;

    [SerializeField] private bool _moveUpwards;

    [SerializeField] private bool _reachedBottom;

    [SerializeField] private int _upwardCount = 1;

    [SerializeField] private int _rushCount = 3;

    [SerializeField] private int _swipeCount = 2;

    private int _previousAttack = 4;

    private bool _animOn;
    private bool _animTrue;

    private UIManager _uiManager;
    [SerializeField] private int _health = 100;

    [SerializeField] private GameObject _healthSlider;

    [SerializeField] private GameObject _greenExplosion;
    [SerializeField] private GameObject _largeGreenExplosion;

    private bool _endBossExplosion;
    private bool _deathSeqOver;



    void Start()
    {
        _startPos = transform.position = new Vector3(0, 6, 0);

        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _clawsAnim = _bothClaws.GetComponent<Animator>();
        _mouthOpenAnim = _head.GetComponent<Animator>();
        _mainBossAnim = GetComponent<Animator>();

        _mainBossAnim.enabled = false;

        _eyeParticle.gameObject.SetActive(false);

        _leftClawParticle.gameObject.SetActive(false);
        _rightClawParticle.gameObject.SetActive(false);

        _leftRushingParticle.gameObject.SetActive(false);
        _rightRushingParticle.gameObject.SetActive(false);

        _headRushingParticle.gameObject.SetActive(false);

        _targetArrows.gameObject.SetActive(false);
        _healthSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_bossEnteredGame == false)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y <= 0)
            {
                _bossEnteredGame = true;
                _newAttack = Time.time + 5f;
            }
        }

        else
        {

            if (_bossHasDied == false)
            {
                _healthSlider.gameObject.SetActive(true);

                StartBossSequences();

                if (_bossRush == false)
                {
                    SideMovement();
                }
                else if (_canShakeCamera == true && _bossRush == true)
                {
                    _cameraShake.FighterBrigadeCameraShake();
                }

                if (_bossRush == true)
                {
                    BossRush();
                }
                else if (_canBeginFiring == true)
                {
                    FireSpineBalls();
                }
            }
            else
            {
                if (_endBossExplosion == false)
                {
                    StartCoroutine(BossDeathSequence());
                    _endBossExplosion = true;
                }
                if (_deathSeqOver == true)
                {
                    Destroy(this.gameObject, 0.2f);
                }
            }
        }
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(6);
        Instantiate(_largeGreenExplosion, _head.transform.position, Quaternion.identity);
        _deathSeqOver = true;
    }

    IEnumerator BossDeathSequence()
    {
        StartCoroutine(DeathTimer());

        if (_animTrue != true)
        {
            DeathAnimStart(true);
        }
        while (_deathSeqOver == false)
        {
            float _randomRange = Random.Range(-2.5f, 2.5f);
            float _randomSeconds = Random.Range(0.2f, 0.5f);
            Instantiate(_greenExplosion, _head.transform.position + new Vector3(_randomRange, _randomRange, 0), Quaternion.identity);
            yield return new WaitForSeconds(_randomSeconds);
        }
    }
    private void DeathAnimStart(bool isTrue)
    {
        _animTrue = isTrue;
        _mainBossAnim.enabled = isTrue;
        _mainBossAnim.SetBool("Death State", isTrue);
        _clawsAnim.SetBool("Claw Death", isTrue);
        _mouthOpenAnim.SetBool("Head Shake", isTrue);
    }

    private void StartBossSequences()
    {
        if (_bossHasDied == false && Time.time > _newAttack)
        {
            _newAttack = Time.time + 1000f;
            _randomAttack = Random.Range(0, 3);

            if (_randomAttack == _previousAttack)
            {
                _randomAttack = Random.Range(0, 3);
                Debug.Log("Rerolling Random Attack " + _randomAttack);
            }

            _previousAttack = _randomAttack;

            switch (_randomAttack)
            {
                case 0:
                    if (_bossRush == false)
                    {
                        StartCoroutine(BossRushPattern());
                    }
                    break;
                case 1:
                    if (_fireSpineBall == false)
                    {
                        StartCoroutine(Arrows());
                        _fireSpineBall = true;
                    }
                    break;
                case 2:
                    if (_canSwipe == false)
                    {
                        StartCoroutine(ClawSwipe());
                        _canSwipe = true;
                    }
                    break;
            }
        }
    }

    private void BossRush()
    {
        if (_bossRush == true && _rushCount > 0)
        {
            if (_moveUpwards == true)
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.down * _multipliedSpeed * Time.deltaTime);
                _canShakeCamera = true;

                if (_animOn != true)
                {
                    AnimStart(true);
                }
                if (transform.position.y < -15f)
                {
                    transform.position = new Vector3(Random.Range(-6f, 6f), 6, 0);
                    _reachedBottom = true;
                }
            }
            if (_reachedBottom == true)
            {
                if (transform.position.y <= 0)
                {
                    _rushCount--;

                    if (_rushCount <= 0)
                    {
                        _canShakeCamera = false;

                        if (_animOn != false)
                        {
                            AnimStart(false);
                        }
                        _clawsAnim.SetBool("Claws Together", false);

                        _upwardCount = 1;
                        _rushCount = 3;
                        _bossRush = false;
                    }
                    _reachedBottom = false;

                    _canFire = Time.time + 2; // allowing for 2 seconds before firing laser
                    _newAttack = Time.time + 3f; //allowing for 3 seconds before next attack
                }
            }
        }
    }
    IEnumerator BossRushPattern()
    {
        if (_upwardCount >= 1)
        {
            _upwardCount--;
            _bossRush = true;
            _moveUpwards = true;
            _clawsAnim.SetBool("Claws Together", true);
            yield return new WaitForSeconds(1);
            _moveUpwards = false;
        }
    }

    private void AnimStart(bool isOn)
    {
        _animOn = isOn;
        _eyeParticle.gameObject.SetActive(isOn);
        _mouthOpenAnim.SetBool("Open Mouth", isOn);
        _headRushingParticle.gameObject.SetActive(isOn);
        _leftRushingParticle.gameObject.SetActive(isOn);
        _rightRushingParticle.gameObject.SetActive(isOn);
    }

    private void SideMovement()
    {
        if (_bossHasDied == false)
        {
            if (transform.position.x <= -4.5f)
            {
                _movementDirection = 1;
            }
            else if (transform.position.x >= 4.5f)
            {
                _movementDirection = -1;
            }
            transform.Translate(Vector3.right * _speed * _movementDirection * Time.deltaTime);

            if (_bossRush == false && _fireSpineBall == false)
            {
                FireLaser();
            }
        }
    }

    private void FireLaser()
    {
        if (_bossHasDied == false && Time.time > _canFire)
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
        if (_bossHasDied != true)
        {
            Instantiate(_bossLaser, transform.position + new Vector3(0, 5.6f, 0), Quaternion.identity);
        }
        _eyeParticle.gameObject.SetActive(false);
       
    }

    IEnumerator ClawSwipe()
    {
        if (_bossHasDied == false && _swipeCount >= 1)
        {
            _clawsAnim.SetBool("Left Claw Swipe", true);
            yield return new WaitForSeconds(0.75f);
            _leftClawParticle.gameObject.SetActive(true);
            _swipeCount--;

            yield return new WaitForSeconds(2);
            _clawsAnim.SetBool("Right Claw Swipe", true);

            yield return new WaitForSeconds(0.75f);
            _rightClawParticle.gameObject.SetActive(true);
            _swipeCount--;

            if (_swipeCount == 0)
            {
                _trySwiping = true;
            }
        }

        if (_trySwiping == true)
        {
            yield return new WaitForSeconds(2);
            _clawsAnim.SetBool("Left Claw Swipe", false);
            _clawsAnim.SetBool("Right Claw Swipe", false);
            _leftClawParticle.gameObject.SetActive(false);
            _rightClawParticle.gameObject.SetActive(false);

            _trySwiping = false;
            _canSwipe = false;
            _swipeCount = 2;

            _canFire = Time.time + 2; //allowing for 2 seconds before firing laser
            _newAttack = Time.time + 3f; //allowing for 3 seconds before next attack
        }
    }

    IEnumerator Arrows()
    {
        _targetArrows.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        _targetArrows.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _canBeginFiring = true;
        _mouthOpenAnim.SetBool("Open Mouth", true);
    }

    //if _canBeginFiring = true , Continue to FireSpineBalls ()
    // V   V   V   V   V   V   V   V   V   V   V   V   V 

    private void FireSpineBalls()
    {
        if (_bossHasDied == false && Time.time > _canFireSpineBall)
        {
            _fireRate = 0.6f;
            _canFireSpineBall = Time.time + _fireRate;

            if (_shotsFired >= 1)
            {
                _shotsFired--;
                Instantiate(_spineBall, _head.transform.position + new Vector3(0, -2.4f, 0), Quaternion.identity);

                if (_shotsFired == 0)
                {
                    _mouthOpenAnim.SetBool("Open Mouth", false);
                }
            }
            else
            {
                _canBeginFiring = false;
                _fireSpineBall = false;
                _shotsFired = 4;

                _canFire = Time.time + 2; //allowing for 2 seconds before firing laser
                _newAttack = Time.time + 3f; //allowing for 3 seconds before next attack
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (_bossEnteredGame == true)
            {
                //_health--;
                _health -= 25;
                _uiManager.BossHealthSlider(_health);

                if (_health <= 0)
                {
                    _bossHasDied = true;
                    _healthSlider.gameObject.SetActive(false);
                }
            }
            Destroy(other.gameObject);
        }
    }


}








