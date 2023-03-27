using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{

    [SerializeField]
    float followSpeed = 1;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, followSpeed * Time.deltaTime);
    }
}
