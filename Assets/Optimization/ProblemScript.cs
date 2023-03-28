using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Target Frame rate: " + Application.targetFrameRate);
    }

    // Update is called once per frame
    void Update()
    {
        int someNumber = 0;
        for (int i = 0; i < 10; i++)
        {
            print("We add one and one here");
            someNumber = 1;
            someNumber += 1;
            print("Result of add: " + someNumber.ToString());
        }
    }
}
