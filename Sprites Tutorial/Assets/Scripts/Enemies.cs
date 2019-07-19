using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 pointA;
    public Vector3 pointB;

    private int count;

    void Start()
    {
        count = 0;
    }

    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
        if(count >= 8)
        {
            gameObject.SetActive(false);
        }
    }
}