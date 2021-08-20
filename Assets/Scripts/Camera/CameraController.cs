using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float leftEdge;
    private float rightEdge;
    private float topEdge;
    private float bottomEdge;

    private Transform camTransform;
    private Transform target;

    private Camera cam;

    private float moveSpeed = 5f;

    private float tempX;
    private float tempY;

    private bool horizontalMove = true;
    private bool verticalMove = true;
    private bool camUpdate = false;

    void Start()
    {
        camTransform = GameObject.Find("Main Camera").transform;
        cam          = GameObject.Find("Main Camera").GetComponent<Camera>();

        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = cam.orthographicSize * Screen.width / Screen.height;

        leftEdge   = GameObject.Find("Left").transform.position.x + camHalfWidth;
        rightEdge  = GameObject.Find("Right").transform.position.x - camHalfWidth;
        topEdge    = GameObject.Find("Top").transform.position.y - camHalfHeight;
        bottomEdge = GameObject.Find("Bottom").transform.position.y + camHalfHeight;

        if (rightEdge - leftEdge < 0)
        {
            horizontalMove = false;
            tempX = leftEdge;
        }
        
        if (topEdge - bottomEdge < 0)
        {
            verticalMove = false;
            tempY = bottomEdge;
        }

        if (horizontalMove || verticalMove)
        {
            camUpdate = true;
        }

        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (!camUpdate)
            return;

        if (horizontalMove)
        {
            tempX = Mathf.Lerp(camTransform.position.x, target.position.x, Time.deltaTime * moveSpeed);
            tempX = Mathf.Clamp(tempX, leftEdge, rightEdge);
        }
        
        if (verticalMove)
        {
            tempY = Mathf.Lerp(camTransform.position.y, target.position.y, Time.deltaTime * moveSpeed);
            tempY = Mathf.Clamp(tempY, bottomEdge, topEdge);
        }

        camTransform.position = new Vector3(tempX, tempY, camTransform.position.z);
    }
}
