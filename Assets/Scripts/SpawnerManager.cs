using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    /*si las condiciones en puntos se cumplen enviar un mensaje a un spawner con la id en orde(es decir el que tenga que salir)
     * y hacer que spawnee al jefe, despues envia un mensaje devuelta al manager y este cambia el destinatario del prox spawn
     */
    [SerializeField]
    private GameObject[] _spawners;
    private SpawnerBehaviour _localSpawner;
    private GameManager _gm;
    private int _bossChallenge,_order;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _bossChallenge = 20;
        _order = 0;
    }


    void Update()
    {
        if (_gm.collectedMoney >= _bossChallenge)
        {
            if(_order > 2)
            {
                _order = 0;
            }
            _localSpawner = _spawners[_order].GetComponent<SpawnerBehaviour>();
            _bossChallenge = _bossChallenge + 15;
            _localSpawner.SpawnBoss();
            if(_order <=2)
            {
                _order++;
            }
            _gm.Messages("the next boss is going to apear at " + _bossChallenge + " essence");
        }
    }
}
