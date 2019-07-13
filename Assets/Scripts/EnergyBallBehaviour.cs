using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallBehaviour : MonoBehaviour
{
    private Transform _target;
    private float _speed;
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Money").GetComponent<Transform>();
        _speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            Destroy(this.gameObject,0.2f);
        }
    }



}
