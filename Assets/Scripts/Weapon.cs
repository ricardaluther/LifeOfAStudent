using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] 
    private float _studySpeed = 3f;
    
    void Update()
    {
        //moves the weapon (e.g. pencil) up
        if (name.Contains("Pencil"))
        {
            transform.Translate(Vector3.up * _studySpeed * Time.deltaTime);
        }

        //if the position of our weapon drops lower than y > 7, then destroy the weapon
        if (name.Contains("PowerIdea"))
        {
            // I have worked with quaternions in the instantiate of the powerIdea, thus I have to use Vector3.forward and not Vector3.up
            transform.Translate(Vector3.forward * 20* Time.deltaTime);
        }
        // if weapon is out of sight, destroy it
        if (transform.position.y > 7)
        {
            Destroy(this.gameObject);
        }

    }
}
