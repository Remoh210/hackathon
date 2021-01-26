using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float TimeToMove = 0.2f;

    private bool bIsMoving = false;
    private Vector3 OrigPos, TargetPos;
    private Animator Animator;

    public LayerMask MovementCollision;

    // Start is called before the first frame update
    void Start()
    {
        Animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !bIsMoving)
        {
            Vector3 TargetPos = transform.position + Vector3.up;
            if (!IsColliding(TargetPos, 0.2f))
            {
                StartCoroutine(MovePlayer(TargetPos));
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && !bIsMoving)
        {
            Vector3 TargetPos = transform.position + Vector3.down;
            if (!IsColliding(TargetPos, 0.2f))
            {
                StartCoroutine(MovePlayer(TargetPos));
            }
        }
       
        if (Input.GetKeyDown(KeyCode.D) && !bIsMoving)
        {
            Vector3 TargetPos = transform.position + Vector3.right;
            if (!IsColliding(TargetPos, 0.2f))
            {
                StartCoroutine(MovePlayer(TargetPos));
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && !bIsMoving)
        {
            Vector3 TargetPos = transform.position + Vector3.left;
            if (!IsColliding(TargetPos, 0.2f))
            {
                StartCoroutine(MovePlayer(TargetPos));
            }
        }      
    }

    private IEnumerator MovePlayer(Vector3 TargetPos)
    {
        OrigPos = transform.position;

        bIsMoving = true;
        Animator.SetFloat("Speed", 0.2f);
        float ElapsedTime = 0;
        while (ElapsedTime < TimeToMove)
        {
            transform.position = Vector3.Lerp(OrigPos, TargetPos, (ElapsedTime / TimeToMove));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = TargetPos;
        bIsMoving = false;
        Animator.SetFloat("Speed", 0.0f);
    }
    private bool IsColliding(Vector3 TargetPos, float Radius)
    {
        bool bIsCollidng = Physics2D.OverlapCircle(TargetPos, Radius, MovementCollision);
        //Debug.Log("Colliding");
        return bIsCollidng;
    }
}
