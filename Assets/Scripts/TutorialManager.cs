using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _tutorialPopUps;
    // Start is called before the first frame update
    void Start()
    {
        _tutorialPopUps[0].SetActive(true);
        _tutorialPopUps[1].SetActive(false);
        _tutorialPopUps[2].SetActive(false);
        _tutorialPopUps[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next(int id)
    {
        int _next = id + 1;
        _tutorialPopUps[id].SetActive(false);
        if (_next <= 3)
        {
            _tutorialPopUps[_next].SetActive(true);
        }
    }
}
