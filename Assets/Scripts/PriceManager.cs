using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PriceManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text[] _seedsPrice;//number 0 has id 4
    private int[] _prices;
    public Sprite[] _seedSprites;
    private GameManager _gm;
    private MouseBehaviour _mouse;
    private int _price,_seed;
    public int id;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _mouse = FindObjectOfType<MouseBehaviour>();
        id = -1;
        _prices = new int[_seedsPrice.Length];

        for(int i = 0;i < _seedsPrice.Length;i++)
        {
            _price = i + 1;
            if(i >= 2)
            {
                _price = _price + 1;
            }
            _prices[i] = _price;
            _seedsPrice[i].text = "Cost : " + _price.ToString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ASeedPlease(int ID)
    {
        if (id < 0)
        {
            id = ID;
            if (ID > 0)
            {
                _price = _prices[ID - 4];
                if (_gm.collectedMoney >= _price)
                {
                    _gm.collectedMoney -= _price;
                    _seed = ID - 3;
                    _mouse.Identification(_seedSprites[_seed], id);
                }
                else
                {
                    //display text somewere advising that you dont have enough money for the seed
                    _gm.Messages("You Don´t have enough money for that . . .");
                    id = -1;
                }
            }
            else
            {
                _seed = ID;
                _mouse.Identification(_seedSprites[_seed], id);
            }
        }
        else
        {
            _gm.Messages("You already have a seed");
        }
        
    }
}
