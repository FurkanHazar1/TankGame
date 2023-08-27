using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector2 _MovePosition;
    Vector2 _offset;
    Vector2 _mousePosition;
    Camera _mainCamera;
    [SerializeField]GameObject _barrel;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameManager _gameManeger;

    float _moveVertical;
    float _moveHorizontal;
    float _moveSpeed=5f;
    float _increaseSpeed=0.7f;
    float _bulletSpeed = 5f;
    bool _isShooting=false;
    
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

   
    void Update()
    {
        _moveVertical = Input.GetAxisRaw("Vertical");
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _MovePosition = new Vector2(_moveHorizontal, _moveVertical) * _moveSpeed;
        if (Input.GetMouseButtonDown(0))
        {
            _isShooting = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_gameManeger._isGameOver)
        {
            MovePlayer();
            RotatePlayer();
            if (_isShooting)
                StartCoroutine(Fire());
        }
        else
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void MovePlayer()
    {
        if (_moveVertical != 0 || _moveHorizontal != 0)
        {
            if (_moveVertical != 0 && _moveHorizontal != 0)
                _MovePosition *= _increaseSpeed;
            _rb.velocity = _MovePosition;
        }
        else
        {
            _MovePosition = new Vector2(0, 0);
            _rb.velocity = _MovePosition;
        }
    }
    void RotatePlayer()
    {
        _mousePosition = Input.mousePosition;
        Vector3 ScreenPoint = _mainCamera.WorldToScreenPoint(transform.localPosition);
        _offset = new Vector2(_mousePosition.x - ScreenPoint.x, _mousePosition.y - ScreenPoint.y).normalized;
        float angle = Mathf.Atan2(_offset.y, _offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        
    }
    IEnumerator Fire()
    {
        _isShooting = false;
        GameObject bullet=Instantiate(_bullet,_barrel.transform.position,Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = _offset * _bulletSpeed;
        yield return new WaitForSeconds(3);
        Destroy(bullet);
    }
}
