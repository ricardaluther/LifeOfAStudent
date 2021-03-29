using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //we need a speed variable for the weapon (in this case study)
    [SerializeField] 
    private float _studySpeed = 3f;


    // Update is called once per frame
    void Update()
    {
        //moves the vaccine up
        if (name.Contains("Pencil"))
        {
            transform.Translate(Vector3.up * _studySpeed * Time.deltaTime);
        }

        //if the position of our vaccine drop y > 7, then destroy the drop
        if (name.Contains("PowerIdea"))
        {
            //transform.
            transform.Translate(Vector3.forward * 20* Time.deltaTime);
        }
        if (transform.position.y > 7)
        {
            Destroy(this.gameObject);
        }

    }
}
