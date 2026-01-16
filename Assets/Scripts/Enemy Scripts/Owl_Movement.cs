using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Owl_Movement : MonoBehaviour
{

    private float xMoveTracker;
    private float yMoveTracker;

    public Rigidbody2D rb;
    public Transform owlTransform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 4f;
    private int direction = 1;
    private Vector2 endPointReference;
    private Vector2 startPointReference;
    private Vector2 velocity = Vector2.zero;

    private void Start()
    {
        Transform endReference = owlTransform;
        endPoint.position = endReference.position + new Vector3(2, 0, 0);
        Transform startReference = owlTransform;
        startPoint.position = startReference.position + new Vector3(-2, 0, 0);
        startPointReference = new Vector2 (startPoint.position.x, startPoint.position.y);
        endPointReference = new Vector2(endPoint.position.x, endPoint.position.y);

    }
    // Update is called once per frame
    void Update()
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

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }
}
