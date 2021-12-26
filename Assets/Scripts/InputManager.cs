using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _defaultPos;
    private BallController _ballController;
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private float _maxBottom = -4f;
    [SerializeField] private float _maxLeft = -5f;
    [SerializeField] private float _maxRight = 5f;
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;
    private UIController _uiController;

    private void Awake()
    {
        _ballController = FindObjectOfType<BallController>();
        _uiController = FindObjectOfType<UIController>();
        _defaultPos = transform.position;
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _arrow.SetActive(false);
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_uiController.gameIsPaused == false)
        {
            if (Input.touchCount == 1)
            {
                _arrow.SetActive(true);
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, transform.position.y, touch.position.y));
                
                    transform.position = Vector3.Lerp(transform.position, new Vector3(-Mathf.Clamp(touchPosition.x, _maxLeft, _maxRight), 
                        transform.position.y,  Mathf.Clamp(touchPosition.z, _maxBottom, 0.0f)), Time.deltaTime);
                
                    _arrow.transform.localScale =
                        new Vector2(((transform.position.z-_defaultPos.z)/4),
                            _arrow.transform.localScale.y); 
                
                    _ball.transform.LookAt(transform.position);


                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _ballController.Shoot();
                    _boxCollider.enabled = false;
                    _meshRenderer.enabled = false;
                    _arrow.SetActive(false);
                }
            }
        }
        
    }

    public void ReturnPlayer()
    {
        _boxCollider.enabled = true;
        _meshRenderer.enabled = true;
        transform.position = _defaultPos;
    }
}
