using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MovePlatform : MonoBehaviour
{

    public float speed;
    public Transform[] points;
    private int index;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, points[index].position) < 0.1f)
        {

            index++;
            if (index == points.Length)
            {
                index = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);
        
    }

    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.tag == "Player")
        {
            Collision.gameObject.transform.SetParent(transform);
        }

       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
