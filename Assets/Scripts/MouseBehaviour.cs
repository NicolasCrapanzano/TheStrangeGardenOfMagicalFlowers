using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{

    private Vector2 _cursorPos;
    private Sprite _seedPickUp;
    private PlantBehaviour _plantLocal;
    private PriceManager _buySeed;
    private GameManager _gm;
    private SpriteRenderer _childSprite;
    private EnemyBehaviour _enemyKilled;
    private Rigidbody2D _rb;
    private int _ID = -1, _actualDamage,_seed;

    void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody2D>();
        _gm = FindObjectOfType<GameManager>();
        _childSprite = GameObject.Find("PickedUpObject").GetComponent<SpriteRenderer>();
        _buySeed = FindObjectOfType<PriceManager>();
        _actualDamage = 1;
    }

    void Update()
    {
        _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.transform.position = _cursorPos;
        if(_rb.IsSleeping())
        {
            _rb.WakeUp();
        }
    }

    public void Identification(Sprite seed,int id) 
    {
        _childSprite.sprite = seed;
        _ID = id;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Hole"))
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                _plantLocal = other.GetComponent<PlantBehaviour>();
                if (_plantLocal._isActive == true)
                {
                    if(_plantLocal._spawnedBug == true)
                    {
                        //Debug.Log("nice try");
                        _plantLocal.KillBug();
                    }else
                    if (_ID != -1 && _plantLocal._haveAPlantOn == false)
                    {
                        _plantLocal.NewPlants(_ID);
                        _ID = -1;
                        _buySeed.id = _ID;
                        _childSprite.sprite = null;
                    } else
                    {
                        _plantLocal.RemovePlant();
                    }
                }else
                {
                    _gm.BuySoil(_plantLocal);
                }
            }
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            _enemyKilled = other.GetComponent<EnemyBehaviour>();
            
            if(Input.GetMouseButtonDown(0))
            {
                _enemyKilled.TakeDamage(_actualDamage);
                
                
            }
        }

    }
}
