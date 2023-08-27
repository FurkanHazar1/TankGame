using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool _isGameOver = false;
    Rigidbody2D _rb;
    public void GameOver()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject enemy in enemies)
        {
            _rb = enemy.GetComponent<Rigidbody2D>();
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    } 

}
