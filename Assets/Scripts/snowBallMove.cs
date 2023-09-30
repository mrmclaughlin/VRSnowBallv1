using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBallMove : MonoBehaviour
{
	private Transform randomPosition;
	private Transform TransformBall;
	public float areaRadius = 2f;      // Radius of the pile

    // Start is called before the first frame update
    void Start()
    {
		
         Vector3 randomPosition = new Vector3(
                Random.Range(-areaRadius, areaRadius),
                Random.Range(0, areaRadius),
                Random.Range(-areaRadius, areaRadius));
		TransformBall = GetComponent<Transform>();
		TransformBall.position = randomPosition;
				
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
