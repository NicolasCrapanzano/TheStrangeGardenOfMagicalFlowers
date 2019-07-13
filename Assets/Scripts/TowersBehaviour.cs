using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersBehaviour : MonoBehaviour
{
    private bool _isActive;
    public int damage, kills;
    public float _timeBtwHits;
    private float _timer;
    private EnemyBehaviour _localEnemy;
    private GameManager _gm;
    [SerializeField]
    private GameObject _buyMenu,_buyText,_buyButton;
    //list the contents of the menu to change them when you buy a tower
    void Start()
    {
        damage = 1;
        _timeBtwHits = 4f;
        _gm = FindObjectOfType<GameManager>();
    }

    
    void Update()
    {
       
    }

    public void BuyTower(bool isActive)
    {
        if (_isActive== false && _gm.collectedMoney >= 50)
        {
            //Debug.Log("Tower bought");
            _isActive = isActive;
            _buyMenu.SetActive(false);
            _buyButton.SetActive(false);
            _buyText.SetActive(false);
            _gm.collectedMoney = _gm.collectedMoney - 50;
        }else if(_isActive == true)
        {
            _gm.Messages("You already bought that");
        }
        else if (_gm.collectedMoney < 50)
        {
            _gm.Messages("You Dont have enough money");
        }
    }
    public void CloseMenu()
    {
        //Debug.Log("close menu");
        _buyMenu.SetActive(false);
    }
    public void OpenMenu()
    {
        _buyMenu.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (_isActive == true)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                _localEnemy = collision.GetComponent<EnemyBehaviour>();
                if (_timer <= Time.time)
                {
                    _timer = Time.time + _timeBtwHits;
                    _localEnemy.TakeDamage(damage);
                    Debug.Log("enemy hit by tower");
                }
            }
        }

    }
}
