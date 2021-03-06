using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    private Vector3 _defaultPos;
    [SerializeField]
    private BallController _ballController;
    public float speed = 2f;
    [SerializeField] private float _minPos = 8f;
    [SerializeField] private float _maxFloat = 12f;
    private void Start()
    {
        _defaultPos = transform.position;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        var position = transform.position;
        if (_ballController.level > 4)
        {
            position.z = Mathf.Clamp(_ball.transform.position.z, _minPos, _maxFloat);
        }
        Vector3 target = new Vector3(_ball.transform.position.x, position.y, position.z);
        if (_ballController._isLaunched == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            transform.position = _defaultPos;
        }
    }
}
