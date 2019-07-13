using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBehaviour : MonoBehaviour
{
    public bool _isActive, _spawnedBug, _haveAPlantOn;
    private bool _isGrowing, _extraTextOn, _bugs, _onlyOnce;
    [SerializeField]
    private AudioClip _pickUpSound, _grownSound;
    private GameObject _temporal;
    public Sprite[] _posiblePlant; //this is the array of possible flowers to get
    [SerializeField]
    private Sprite _off, _on;
    private Color _textColor;
    [SerializeField]
    private GameObject _childPlant, _doneText, _particlePickUp, _bugGO, _energy; // this is the child game object that is the plant
    [SerializeField]
    private Text _childText1;
    private SpriteRenderer _plantSkin, _thisSR;
    private int _ID, _money;
    private float _scale = 0.01f, _bugApearIn = 0, _timeUntilEat,_willSpawnBug;
    private GameManager _gM;

    void Start()
    {
        _childPlant.gameObject.SetActive(false);
        _plantSkin = _childPlant.GetComponent<SpriteRenderer>();
        _childPlant.transform.localScale = new Vector3(0, 0, 0);
        _doneText.SetActive(false);
        _gM = FindObjectOfType<GameManager>();
        _thisSR = GetComponent<SpriteRenderer>();
        _textColor = _childText1.color;

    }

    // Update is called once per frame
    //al iniciar el crecimiento de una planta, si esta es de nivel alto(lenta) que haya una probabilidad rand, de que en un momento le aparezcan bichos que se la coman, teniendo que matarlos para no perder la plata 
    void Update()
    {
        if (_isActive == false)
        {
            _thisSR.sprite = _off;
        }
        else
        {
            _thisSR.sprite = _on;
            if (_haveAPlantOn == true)
            {
                if (_spawnedBug == true)
                {
                    if(_timeUntilEat <= Time.time)//if the bug eats the plant
                    {
                        Debug.Log("muere la planta");
                        _doneText.SetActive(false);
                        _haveAPlantOn = false;
                        _childPlant.gameObject.SetActive(false);
                        _childPlant.transform.localScale = new Vector3(0, 0, 0);
                        _bugGO.SetActive(false);
                        _spawnedBug = false;
                    }
                }
                if (_childPlant.transform.localScale.x <= 1)
                {
                    if (_bugs == true)
                    {
                        
                        _bugApearIn = Random.Range(0.3f, 0.9f);
                        Debug.Log(_bugApearIn);
                        _bugs = false;
                    }
                    if (_bugApearIn != 0)//if the moment when the bug appear is true 
                    {
                        Debug.Log("estan pasando cosas");
                        if (_childPlant.transform.localScale.x >= _bugApearIn && _childPlant.transform.localScale.x <= _bugApearIn + _bugApearIn)// then check if it reach the time to spawn the bug
                        {
                            //spawn bug set bool true
                            _bugGO.SetActive(true);
                            _spawnedBug = true;
                            BugAppeared();
                            //goto 0 
                            _bugApearIn = 0;
                        }
                    }
                    _childPlant.transform.localScale = _childPlant.transform.localScale + new Vector3(_scale, _scale, _scale);//multiply for time.deltatime to limit the speed
                }
                else
                {
                    if (_onlyOnce == false)
                    {
                        AudioSource.PlayClipAtPoint(_grownSound, transform.position);
                        _onlyOnce = true;
                    }
                    _isGrowing = false;
                    _doneText.SetActive(true);
                    //cerra al salir
                    _bugs = false;
                    _bugApearIn = 0;
                }
            }
        }
        if (_extraTextOn == true)//vanish the extra text gradually
        {

            if (_childText1.color.a != 0)
            {
                _childText1.color = _childText1.color - new Color(0, 0, 0, 0.01f);
            } else
            {

                _extraTextOn = false;
            }
        }

    }
    private void BugAppeared()
    {
        
        _timeUntilEat = Time.time + 3f;
    }

    public void KillBug()
    {
        _bugGO.SetActive(false);
        _spawnedBug = false;
        Debug.Log("he ded");
    }
    public void ChangeText(string T)
    {
        _childText1.color = _textColor;
        _childText1.gameObject.SetActive(true);
        _childText1.text = T;
        _extraTextOn = true;
    }

    public void NewPlants(int ID)
    {
        int _aFlower;
        if(ID == 0)
        {
            _aFlower = Random.Range(0, 3);
            _scale = 0.01f;
            _money = 1;
        }
        else
        {
            _aFlower = ID;
        }
        if(_haveAPlantOn==false)
        {
            _haveAPlantOn = true;
            _childPlant.gameObject.SetActive(true);
            _plantSkin.sprite = _posiblePlant[_aFlower];
            _ID = ID;
            _isGrowing = true;
        }
        if(_ID == 4)
        {
            _scale = 0.0045f;
            _money = 3;
        }
        else if(_ID == 5)
        {
            _scale = 0.0035f;
            _money = 5;
        }
        else if(_ID == 6)
        {
            _scale = 0.0025f;
            _money = 8;
            if (_willSpawnBug >= 0 && _willSpawnBug <= 4)
            {
                _bugs = true; // this let the game enable some bugs that eat your flowers and make you lose money
            }
        }
        else if(_ID == 7)
        {
            _scale = 0.0015f;
            _money = 11;
            _willSpawnBug = Random.Range(0, 10f);
            if(_willSpawnBug >=0 && _willSpawnBug <=4)
            {
                _bugs = true; 
            }
        }
        else if (_ID == 8)
        {
            _scale = 0.0009f;
            _money = 15;
            _willSpawnBug = Random.Range(0, 10f);
            if (_willSpawnBug >= 0 && _willSpawnBug <= 4)
            {
                _bugs = true; 
            }
        }
    }

    public void RemovePlant()
    {
        if (_haveAPlantOn == true)
        {
            if (_isGrowing == false)
            {
                _onlyOnce = false;
                _temporal = Instantiate(_particlePickUp, transform.position, Quaternion.identity);
                Destroy(_temporal, 0.5f);
                AudioSource.PlayClipAtPoint(_pickUpSound,transform.position);
                _haveAPlantOn = false;
                _childPlant.gameObject.SetActive(false);
                _doneText.SetActive(false);
                _childPlant.transform.localScale = new Vector3(0, 0, 0);
                _gM.Money(_money);
                Instantiate(_energy,transform.position,Quaternion.identity);
            }
        }
        
    }

    
}
