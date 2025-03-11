using UnityEngine;
using System.Collections;

public class MovingPad : MonoBehaviour
{
    public Transform pointA;  
    public Transform pointB;  
    public float speed = 2f;  
    public float waitTime = 1f; 

    private Vector3 targetPosition;
    private bool isActivated = false;
    private bool isMoving = false; 
    private bool isReturning = false;
    
    void Start()
    {
        targetPosition = transform.position; 
    }

    void Update()
    {
        if (isActivated && !isMoving)
        {
            StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            isActivated = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
            isActivated = false;
            StartCoroutine(ReturnToStart());
        }
    }
    private IEnumerator ReturnToStart()
    {
        isReturning = true;
        targetPosition = pointA.position; 

        while (Vector3.Distance(transform.position, pointA.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            yield return null;
        }

        isReturning = false; // 원래 위치로 돌아온 후 초기화
    }
}