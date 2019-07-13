using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip _playSound;
    [SerializeField]
    private GameObject[] _backDeco;
    [SerializeField]
    private GameObject _history;
    private int _randInstObj;
    private float _randX, _randY;
    void Start()
    {
        Cursor.visible = true;
        for (int i = 25;i >0;i--)
        {
            _randX = Random.Range(-8.88f, 8.84f);
            _randY = Random.Range(4.0f, -4.99f);
            _randInstObj = Random.Range(0, 8);
            Instantiate(_backDeco[_randInstObj], new Vector2(_randX, _randY), Quaternion.identity);
        }
    }

    
    void Update()
    {
        
    }
    public void CloseHistory()
    {
        _history.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
        AudioSource.PlayClipAtPoint(_playSound,transform.position);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
