using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int someNumber = 0;
        print("We add one and one here");
        for (int i = 0; i < 1000000; i++)
        {
            someNumber = 1;
            someNumber += 1;
        }
        print("Result of add: " + someNumber.ToString());
    }
}
