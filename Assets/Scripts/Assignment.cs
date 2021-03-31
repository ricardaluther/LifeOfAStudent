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
        //the Reading assignment rotates around the y-axes and moves randomly to the right and is thereby harder to catch as well as it is not always catching you.
        //just like in uni: sometimes the ReadingAssigment is super important and if you do not read it you do not get anything
        //and sometimes it is never talked about again.
        if (name.Contains("ReadingAssignment"))
        {
            transform.Translate(Vector3.right*Random.Range(-1f,1f)*2*_deadlineSpeed * Time.deltaTime);
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
        //damage student and destroy assignment
        if (other.CompareTag("Student"))
        {
            other.GetComponent<Student>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Weapon"))
        {
            // If assignment collides with weapon, we get destroyed and the score gets +1
            if (!other.name.Contains("PowerIdea"))
            {
                Destroy(other.gameObject);
            }
            GameObject.FindWithTag("Student").GetComponent<Student>().RelayScore(1);
            Destroy(this.gameObject);
            
            //since the reading assignment is hard to catch you get 3 points for catching it
            if (this.name.Contains("ReadingAssignment"))
            {
                GameObject.FindWithTag("Student").GetComponent<Student>().RelayScore(3);
            }
        }
    }
}
