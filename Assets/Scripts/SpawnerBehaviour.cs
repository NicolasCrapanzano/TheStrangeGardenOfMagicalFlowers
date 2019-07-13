using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    public static bool gameOver;
    [SerializeField]
    private GameObject[] _enemy;
    public int _ID;
    private bool _alreadySpawned,_minChallenge;
    private int _orderApear=0;
    private static int _bossSpawnPoint;
    private GameObject _spawnedEn,_spawnedBoss;
    private GameManager _gm;
    private float _timer,_timerForBoss, _timeUntilWave,_nextWave;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _nextWave = 30;
    }

    //cambiar el spawner a enemigos x tiempo, jefes por objetivos
    
    void Update()
    {
        if(_orderApear > 0)//instantiate boss in order
        {
            if(_timerForBoss < Time.time)
            {
                _orderApear=_orderApear -1;
                _timerForBoss = Time.time + 0.5f;
                _spawnedBoss = Instantiate(_enemy[1], transform.position, Quaternion.identity);
                _spawnedBoss.SendMessage("LinkedSpawner", this.transform);
            }
        }
        if (gameOver == false)
        {
            if (_minChallenge == false)
            {
                if (_gm.collectedMoney >= 10)
                {

                    _timer = Time.time + _nextWave;
                    if (_nextWave > 5)
                    {
                        _nextWave = _nextWave - 5;
                    }
                    _minChallenge = true;
                    _timeUntilWave = _timer - Time.time;
                    _gm.Messages("prepare for the horde in " + _timeUntilWave);
                    //Debug.Log("prepare for the horde in " + _timeUntilWave);
                    //add a message  saying that enemies will come en the next minit
                    //sound when enemies are going to spawn?
                }
            }
            else if (_timer <= Time.time)
            {
                //Debug.Log("THEY ARE COMINGG!!");
                _spawnedEn = Instantiate(_enemy[0], transform.position, Quaternion.identity);
                _spawnedEn.SendMessage("LinkedSpawner", this.transform);
                _timer = Time.time + _nextWave;
                if (_nextWave > 10)
                {
                    _nextWave = _nextWave - 5;
                }
            }
            
        }

    }

    public void SpawnBoss()
    {

        _orderApear = _orderApear + 1;
    }
}


