using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 start;
    private Vector2 goal;
    public Vector2 goalOffset;

    public float speed;

    //todo: add condition for auto loop and triggering'
    // of platform move

    // Start is called before the first frame update
    void Start()
    {
        start = gameObject.transform.position;
        goal = start + goalOffset;
    }

    // Update is called once per frame
    void Update()
    {
        // the time function for the platform's movement is
        // defined by sinusoidal movement. An amplitude of 0.5
        // and a y shift of 1 makes this function return values
        // between 0 and 1. Making the cos negative starts t at 0
        float t = 0.5f * (1 - Mathf.Cos(speed * Time.timeSinceLevelLoad));
        transform.position = Vector2.Lerp(start, goal, t);
    }
}
