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
    private EnemyController _enemyController;
    private UIController _uiController;
    public int level = 1;

    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _enemyPos = _enemy.transform.position;
        _defaultPos = transform.position;
        _enemyController = FindObjectOfType<EnemyController>();
        _uiController = FindObjectOfType<UIController>();
    }

    private void Update()
    {
        _uiController.UpdateText(level);
        if (_enemyBlocksCount == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                _enemyBlocks[i].SetActive(true);
            }
            _enemyBlocksCount = 3;
            _enemyController.speed += 0.5f;
            level += 1;
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
            _rb.AddForce(transform.forward * _arrow.transform.localScale.x * 1000);
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
        _inputManager.ReturnPlayer();
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
