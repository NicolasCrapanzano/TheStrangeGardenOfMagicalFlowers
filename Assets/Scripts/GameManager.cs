using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameOver,_messageOn,_final,_MPMinScale;
    private float _mpActualScale,_timer;
    public int collectedMoney;
    private int _plantCost;
    [SerializeField]
    private Text _displayMoney,_plantText1,_messages;
    [SerializeField]
    private Image _messageCanvas;
    private Color _textColor,_messageCanvasColor;
    [SerializeField]
    private GameObject _seeds1, _seeds2,_menu,_theEnd,_messageCanvasGO;
    [SerializeField]
    private GameObject[] _Plants;
    private PlantBehaviour _actualPlant;
    private SpawnerBehaviour _spawner;
    private SpriteRenderer _masterFlowerGO;
    private Transform _MasterFlower;
    [SerializeField]
    private AudioClip _buyLandSound,_evilLaugh;
    [SerializeField]
    private Sprite _secondPhase;
    void Start()
    {
        _actualPlant = GameObject.Find("Test Hole").GetComponent<PlantBehaviour>();
        _MasterFlower = GameObject.FindGameObjectWithTag("MasterFlower").GetComponent<Transform>();
        _masterFlowerGO = GameObject.FindGameObjectWithTag("MasterFlower").GetComponent<SpriteRenderer>();
        _spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerBehaviour>();
        BuySoil(_actualPlant);
        _plantCost = 10;
        _textColor = _messages.color;
        _messageCanvasColor = new Color(0,0,0,0.6f);
        _messageCanvas.color = new Color(0, 0, 0,0);
        _theEnd.SetActive(false);
    }
    

    void Update()
    {
        //use this to make the background change to a darker color, put the color in a independent variable so it dosnt change back with the esence
        _displayMoney.text = "Essence : " + collectedMoney.ToString();
        if (_MPMinScale == true && collectedMoney <= 20)
        {
            _MasterFlower.localScale = new Vector3(20 / 10, 20 / 10, 20 / 10);
        }
        else
        {
            _mpActualScale = collectedMoney / 10;
            _MasterFlower.localScale = new Vector3(_mpActualScale, _mpActualScale, _mpActualScale);
        }
        if (_gameOver == false)
        {
            if(collectedMoney >= 20 && _MPMinScale== false)
            {
                _MPMinScale = true;
            }
            if (_mpActualScale >= 13)
            {
                
                 //call another function that starts the ending
                _gameOver = true;
                SpawnerBehaviour.gameOver = true;
            }
        }else
        {
            TheEnd();
            
        }
        if(_messageOn==true)
        {
            if(_messages.color.a > 0)
            {
                _messages.color = _messages.color - new Color(0, 0, 0, 0.005f);
                Debug.Log("noentiendo");
            }else
            {
                _messageOn = false;
                //disable GO
                _messageCanvasGO.SetActive(false);
                Debug.Log("end message");
            }
            if(_messageCanvas.color.a != 0)
            {
                _messageCanvas.color = _messageCanvas.color - new Color(0,0,0,0.005f);
            }

        }
    }

    public void Messages(string display)
    {
        _messageCanvas.color = _messageCanvasColor;
        _messages.color = _textColor;
        _messageOn = true;
        _messageCanvasGO.SetActive(true);
        _messages.text = display;
    }
    public void TheEnd()
    {

        //change the sprite for the flowers to the one with eyes
        _masterFlowerGO.sprite = _secondPhase;
        //play a sound like an evil laugh
        
        //display text that tells the player the evil that they awakened
        if(_final == false)
        {
            AudioSource.PlayClipAtPoint(_evilLaugh, transform.position);
            Debug.Log("You Win");
            _final = true;
            _timer = Time.time + 5;
        }
        if (_timer < Time.time)
        {
            _theEnd.SetActive(true);
        }
    }
    public void CloseEnd()
    {
        SceneManager.LoadScene(0);
    }
    public void Money(int A)
    {
        collectedMoney = collectedMoney + A;
        
    }
    public void UIButtons(int CODE)
    {
        if(CODE == 0)
        {
            _seeds1.SetActive(false);
            _seeds2.SetActive(true);
        }if(CODE == 1)
        {
            _seeds1.SetActive(true);
            _seeds2.SetActive(false);
        }
    }
    public void MenuOpen()
    {
        _menu.SetActive(true);
    }
    public void MenuClose()
    {
        _menu.SetActive(false);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void BuySoil(PlantBehaviour p)
    {
        _actualPlant = p;
        if (collectedMoney >= _plantCost)//check if the player has enough money
        {
            collectedMoney = collectedMoney - _plantCost;
            _plantCost = _plantCost + 5;
            
            //_actualPlant = _Plants[_nextBuy].GetComponent<PlantBehaviour>(); para que carajo hice esto?
            _actualPlant._isActive = true;
            //_nextBuy++;
            AudioSource.PlayClipAtPoint(_buyLandSound,transform.position);
        }else
        {
            _actualPlant.ChangeText("You dont have enough money" + "\n" + "Cost :" + _plantCost);
            Debug.Log("No tenes plata amiguito");
        }
    }

}
