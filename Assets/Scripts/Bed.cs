using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{

     
    [SerializeField]
    private float _sleepSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // the bed rotates down around the y-axis
        transform.Translate(Vector3.down * _sleepSpeed * Time.deltaTime);
        transform.Rotate(0f,5f,0f, Space.World);
    }
    
      private void OnTriggerEnter(Collider other)
    {
        // if the object we collide with is the student, fall asleep (details see student)
        if (other.CompareTag("Student"))
        {
            other.GetComponent<Student>().StartSleeping();
            Destroy(this.gameObject);
        }
        // if collision with weapon: you have resisted the temptation of sleep!
        // Add score and destroy pencil (not PowerIdea, since it is a strong PowerUp)
        else if (other.CompareTag("Weapon"))
        {
            if (!other.name.Contains("PowerIdea"))
            {
                Destroy(other.gameObject);
            }
            Destroy(this.gameObject);
            GameObject.FindWithTag("Student").GetComponent<Student>().RelayScore(1);
        }
    }
}
