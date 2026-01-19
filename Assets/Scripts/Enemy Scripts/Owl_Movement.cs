using System.Collections.Generic;
using UnityEngine;

public class Owl_Movement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform owlTransform;
    public Transform startPoint;
    public Transform endPoint;
    private float speed = 1.5f;
    private float positioningSpeed = 3f;
    private int direction = 1;
    private Vector2 endPointReference;
    private Vector2 startPointReference;
    private bool owlIsInitialized = false;
    public bool owlInPosition { get; private set; } = false;
    private int stopPointNumber;
    private bool owlMovingToSpot;
    private bool owlFindingParking;
    private Vector2 spotPointTarget;
    private int totalSpotPoints = 5;
    private Owl_Stop_Point_Script stopPointScript;

    // y 0 - 5.5 - 2.6 - 4.3 - 1.5

    public List<float> yPositionList = new List<float>();
    

    private void Start()
    {
        yPositionList.Add(2.5f);
        yPositionList.Add(4f);
        yPositionList.Add(5.5f);
        yPositionList.Add(7f);
        yPositionList.Add(8.5f);
        int yPositionListCount = yPositionList.Count;
        owlIsInitialized = false;
        owlFindingParking = true;
        while (owlFindingParking)
        {
            stopPointNumber = Random.Range(1, totalSpotPoints + 1);
            stopPointScript = GameObject.FindGameObjectWithTag("Owl Stop Point " + stopPointNumber).GetComponent<Owl_Stop_Point_Script>();
            if (stopPointScript == null)
            {
                Destroy(startPoint);
                Destroy(endPoint);
                gameObject.GetComponent<Enemy_Properties>().Destroy();
            }
            else if (!stopPointScript.spotOccupied)
            {
                owlFindingParking = false;
            }
        }
        stopPointScript.spotOccupied = true;
        spotPointTarget = new Vector2(Random.Range(-15f, 38.5f) * .1f, yPositionList[stopPointNumber - 1]);
        owlMovingToSpot = true;
    }

    private void OnDestroy()
    {
        Destroy(startPoint);
        Destroy(endPoint);

    }
    // Update is called once per frame
    void Update()
    {
        if (owlInPosition)
        {
            owlMovingToSpot = false;
            InitializeOwlStrafe();
            owlInPosition = false;

        }

        float distanceToSpot = (spotPointTarget - (Vector2)owlTransform.position).magnitude;

        if (distanceToSpot <= 0.1f && owlMovingToSpot)
        {
            owlInPosition = true;
        }

        if (owlMovingToSpot)
        {
            moveOwlToStrafeSpot();
        }

        if (owlIsInitialized)
        {
            Vector2 target;

            if (direction == 1)
            {
                target = startPointReference;
            }
            else
            {
                target = endPointReference;
            }

            //owlTransform.position = Vector2.SmoothDamp(owlTransform.position, target, ref velocity, speed);
            owlTransform.position = Vector2.MoveTowards(owlTransform.position, target, speed * Time.deltaTime);

            float distance = (target - (Vector2)owlTransform.position).magnitude;

            if (distance <= 0.1f)
            {

                direction *= -1;
            }
        }




        
    }

    void InitializeOwlStrafe()
    {
        Transform endReference = owlTransform;
        endPoint.position = endReference.position + new Vector3(2, 0, 0);
        Transform startReference = owlTransform;
        startPoint.position = startReference.position + new Vector3(-2, 0, 0);
        startPointReference = new Vector2(startPoint.position.x, startPoint.position.y);
        endPointReference = new Vector2(endPoint.position.x, endPoint.position.y);
        owlIsInitialized = true;
    }

    void moveOwlToStrafeSpot()
    {
        owlTransform.position = Vector2.MoveTowards(owlTransform.position, spotPointTarget, positioningSpeed * Time.deltaTime);
    }
}
