using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehevior : MonoBehaviour
{
    GameManager _gameManager;
    GameObject _player;
    Rigidbody2D _rb;

    float _enemyHealth = 100f;
    float _enemySpeed = 1f;
    Vector2 _moveDirection;
    bool _disaleEnemy = false;
    Quaternion _targetRotation;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        if (!_gameManager._isGameOver && !_disaleEnemy)
        {
            MoveEnemy();
            RotateEnemy();
        }
       
    }

    void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _enemySpeed * Time.deltaTime);

    }

    void RotateEnemy()
    {
        _moveDirection = _player.transform.position - transform.position;
        _moveDirection.Normalize();
        _targetRotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);

        if (transform.rotation != _targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 200 * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
           Destroy(collision.gameObject);
           _enemyHealth -= 50f;
            StartCoroutine(KnockBack());
            if(_enemyHealth<=0f)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _gameManager._isGameOver = true;          
            _disaleEnemy = true;
            _gameManager.GameOver();
        }
        
    }

    IEnumerator KnockBack()
    {
        _disaleEnemy = true;
        yield return new WaitForSeconds(0.3f);
        _disaleEnemy = false;
    }
    
}
