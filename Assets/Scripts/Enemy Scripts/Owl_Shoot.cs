using UnityEngine;

public class Owl_Shoot : MonoBehaviour
{

    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
