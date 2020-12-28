using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    private static float _speed = 20;
    private bool _isInsideAgroField = false, _justDidDamage = false, _isInsideAttackRange = false;
    public GameObject hero;
    public GameObject secondHero;
    private Animator _animator;
    //private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static int _maxHealth = 2;
    private int _currentHealth = _maxHealth;
    private int heroNum;
    //[SerializeField] private ParticleSystem _deathParticle, _hitParticle;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        heroNum = 0;

    }

    void Update()
    {
       // if (!CanvasController.instance.isGameRunning) return;
        if (_isInsideAgroField && !_isInsideAttackRange) 
        {
            if (heroNum == 0)
            {
                transform.LookAt(hero.transform.position);
                transform.position += transform.forward * _speed * Time.deltaTime;
                if (heroNum == 1)
                {
                    transform.LookAt(secondHero.transform.position);
                    transform.position += transform.forward * _speed * Time.deltaTime;
                   
                }
            }
            else if(heroNum == 1)
            {
                transform.LookAt(secondHero.transform.position);
                transform.position += transform.forward * _speed * Time.deltaTime;
            }
          
            //transform.position += (hero.transform.position - transform.position).normalized * Time.deltaTime * _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Weapon":
                Debug.Log("hit");
                _currentHealth--;
                if (_currentHealth == 0)
                {
                    //_deathParticle.Play();
                   
                    _animator.SetInteger("condition", 4);
                    _animator.SetBool("Dead", true);

                    DOVirtual.DelayedCall(2f, () => { Destroy(gameObject); });
                }
                else
                {
                    //_hitParticle.Play();
                    _animator.SetInteger("condition", 3);
                    _animator.SetInteger("condition", 1);
                    
                }
                break;
            case "AgroField":
                //CanvasController.instance.UpdateHealthBar();
                //_justDidDamage = true;
                    _isInsideAgroField = true;
                    _animator.SetInteger("condition", 2);
                //Invoke(nameof(EnableFollow), 2);
                break;
            case "AttackRange":
                    _isInsideAttackRange = true;
                    _animator.SetInteger("condition", 1);               
                break;
            /*case "Hero":
                CanvasController.instance.UpdateHealthBar();
                break;*/

        }
    }

    private void EnableFollow()
    {
        _justDidDamage = false;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AttackRange"))
        {
            _isInsideAttackRange = false;
            _animator.SetInteger("condition", 2);
        }
        if (other.CompareTag("AgroField"))
        {
            _isInsideAgroField = false;
            _animator.SetInteger("condition", 0);
        }
    }

    public void ChangeLookAt()
    {
        if (heroNum == 0)
        {
            heroNum = 1;
            Debug.Log(heroNum + "hero");
        }
        else if (heroNum == 1)
        {
            heroNum = 0;
        }
    }

    

    
}
