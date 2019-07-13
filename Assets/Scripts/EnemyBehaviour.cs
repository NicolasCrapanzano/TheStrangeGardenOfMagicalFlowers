using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    private float _speed;
    [SerializeField]
    private bool _stop;
    [SerializeField]
    public int _stealMoney,_maxStealed,_ID,_HP;
    [SerializeField]
    private Text _stealedMoneyText;
    [SerializeField]
    private GameObject _deathParticle,_energy;
    private GameObject _temporal;
    private Transform _target, _target2;
    private GameManager _gm;
    [SerializeField]
    private AudioClip[] _deathSound;
    // Start is called before the first frame update

    void Start()
    {
        _stop = false;
        _target = GameObject.FindGameObjectWithTag("Money").GetComponent<Transform>();
        _gm = FindObjectOfType<GameManager>();
        if(_ID == 0)
        {
            _HP = 1;
            _speed = 2f;
            _maxStealed = 5;
            //change sprite
        }
        else if (_ID == 1)
        {
            _HP = 5;
            _speed = 1f;
            _maxStealed = 10;
        }
        if (_target2 == null)
        {
            _target2 = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Transform>();

        }
    }

    // augment the speed of the enemies and health over the curse of the game


    void Update()
    {

        if (_stop == false && _stealMoney <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        } else if (_stop == true)
        {
            if (_gm.collectedMoney > 0)
            {
                if (_stealMoney < _maxStealed)
                {
                    _gm.collectedMoney--;
                    _stealMoney++;
                    _stealedMoneyText.text = _stealMoney.ToString();
                    if (_stealMoney >= _maxStealed)
                    {
                        _stop = false;
                    }
                }
            } else if (_stealMoney >= 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target2.position, _speed * Time.deltaTime);
            }
        } else if (_stop == false && _stealMoney >= _maxStealed)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, _target2.position, _speed * Time.deltaTime);
        }
        if(_HP <= 0)
        {
            Destroy(this.gameObject);
            if (_stealMoney > 0)
            {
                _gm.collectedMoney = _gm.collectedMoney + _stealMoney / 2;
                //Debug.Log(_enemyKilled._stealMoney / 2);
                Instantiate(_energy, transform.position, Quaternion.identity);
            }
            if(_ID >= 1)//extra reward
            {
                _gm.collectedMoney = _gm.collectedMoney + 3 * _ID;
                Instantiate(_energy, transform.position, Quaternion.identity);
            }
            AudioSource.PlayClipAtPoint(_deathSound[Random.Range(0,1)],transform.position);
            _temporal = Instantiate(_deathParticle,transform.position,Quaternion.identity);
            Destroy(_temporal, 1);
        }
    }
    public void TakeDamage(int D)
    {
        _HP -= D;
    }
    private void LinkedSpawner(Transform t)
    {
        //Debug.Log("LINKED");
        _target2 = t;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision);
        if(collision.gameObject.CompareTag("Money"))
        {
            _stop = true;
        }
        if(collision.gameObject.CompareTag("Spawner"))
        {
            if(_stealMoney > 0)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
