using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class PowerUpCollectables : MonoBehaviour
{
    
    [SerializeField] 
    private float _speed = 2f;

   

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (name.Contains("Cola"))
        {
            transform.Rotate(0f,3f,0f, Space.World);
        }
        if (name.Contains("Donut"))
        {
            transform.Translate(Vector3.right*Random.Range(1f,.1f)*2*_speed * Time.deltaTime);
            transform.Rotate(0f,7f,0f, Space.World);
        }
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
        {
            
            if (this.CompareTag("Cola") && other.CompareTag("Student"))
            {
                other.GetComponent<Student>().ActivatePowerUp();
                Destroy(this.gameObject);
            }
            else if (this.CompareTag("Bafog") && other.CompareTag("Student"))
            {
                other.GetComponent<Student>().GetBafog();
                Destroy(this.gameObject);
            }
            else if (this.CompareTag("Donut") && other.CompareTag("Student"))
            {
                other.GetComponent<Student>().EatDonut();
                Destroy(this.gameObject);
            }
        }
    }

    
