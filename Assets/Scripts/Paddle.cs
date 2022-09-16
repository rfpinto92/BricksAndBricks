using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    private float Speed = 2.0f;
    private float MaxMovement = 1.9f;
    private float input = 0f;

    [SerializeField]
    public Configurations Configuration;


    // Start is called before the first frame update
    void Start()
    {
        Speed = Configuration.AppConfig.maxPaddleSpeed;
    }

    public void GoLeft()
    {
        input = -1f;
    }
    public void GoRight()
    {
        input = 1f;
    }
    public void DontMove()
    {
        input = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }
}
