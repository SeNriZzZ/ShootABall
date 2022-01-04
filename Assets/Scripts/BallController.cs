using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool _isLaunched = false;
    private Vector3 _defaultPos;
    private Vector3 _enemyPos;
    private Vector3 _playerPos;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject[] _enemyBlocks;
    private int _enemyBlocksCount = 3;
    private Rigidbody _rb;
    [SerializeField] private GameObject _arrow;
    [SerializeField]
    private EnemyController _enemyController;
    [SerializeField]
    private UIController _uiController;
    public int level = 1;
    [SerializeField] private float _ballSpeed = 20f;
    [SerializeField] private float _enemySpeedIncrement = 0.05f;
    public bool _returnPlayerAvaliable = false;
    

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _enemyPos = _enemy.transform.position;
        _defaultPos = transform.position;
        _uiController.UpdateText(level);
    }

    private void Update()
    {
        if (_enemyBlocksCount == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                _enemyBlocks[i].SetActive(true);
            }
            _enemyBlocksCount = 3;
            _enemyController.speed += _enemySpeedIncrement;
            level += 1;
            _uiController.UpdateText(level);
        }

        if (_isLaunched == true)
        {
            _arrow.SetActive(false);
        }
        
    }

    public void Shoot()
    {
        if (_isLaunched == false && _uiController.gameIsPaused == false)
        {
            _rb.velocity = transform.forward * _arrow.transform.localScale.x * _ballSpeed;
            _isLaunched = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Reload();
        }

        if (collision.gameObject.CompareTag("EnemyBlock"))
        {
            Reload();
            _enemyBlocksCount -= 1;
            collision.gameObject.SetActive(false);
        }
    }

    private void Reload()
    {
        _isLaunched = false;
        _enemy.transform.position = _enemyPos;
        transform.position = _defaultPos;
        _returnPlayerAvaliable = true;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
