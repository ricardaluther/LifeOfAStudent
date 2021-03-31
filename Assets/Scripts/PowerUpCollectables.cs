using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class PowerUpCollectables : MonoBehaviour
{
    
    [SerializeField] 
    private float _speed = 2f;
    

    void Update()
    {
        //here the movements of the PowerUps are defined. All move down, but each of them has a specific pattern of speed and rotation
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (name.Contains("Cola") || name.Contains("cup")){
            transform.Rotate(0f,3f,0f, Space.World);
        }
        if (name.Contains("Donut"))
        {
            transform.Translate(Vector3.right*Random.Range(3f,-3f)*2*_speed * Time.deltaTime);
        }
        if (name.Contains("cup"))
        {
            transform.Rotate(0f,-.4f,0f,Space.Self);
        }
        // when the PowerUps are no longer visible they get destroyed
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }
    // here the specific collision pattern are specified. For descriptio see Student.
    // After colliding with the student the powerups are destroyed
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
            else if (this.CompareTag("cup") && other.CompareTag("Student"))
            {
                other.GetComponent<Student>().DrinkCoffee();
                Destroy(this.gameObject);
            }
        }
    }

    
