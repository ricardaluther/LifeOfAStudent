using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Assignment : MonoBehaviour
{
    
    [SerializeField]
    private float _deadlineSpeed = 5f;
    
    void Update()
    {
        //moves assignment down
        transform.Translate(Vector3.down * _deadlineSpeed * Time.deltaTime);
        if (name.Contains("ReadingAssignment"))
        {
            transform.Translate(Vector3.right*Random.Range(1f,.1f)*2*_deadlineSpeed * Time.deltaTime);
            transform.Rotate(0f,7f,0f, Space.World);
        }
      
        //if the position of assigment y > x, then respawn it at top
        if (transform.position.y < -5.5f) 
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7f, z:0f);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is the student
        //damage student or destroy it
        if (other.CompareTag("Student"))
        {
            other.GetComponent<Student>().Damage();
            GameObject.FindWithTag("Student").GetComponent<Student>().RelayLives(1);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Weapon"))
        {
            if (!other.name.Contains("PowerIdea"))
            {
                Destroy(other.gameObject);
            }
            GameObject.FindWithTag("Student").GetComponent<Student>().RelayScore(1);
            Destroy(this.gameObject);
            if (this.name.Contains("ReadingAssignemnt"))
            {
                GameObject.FindWithTag("Student").GetComponent<Student>().RelayScore(3);
            }
        }
    }
}
