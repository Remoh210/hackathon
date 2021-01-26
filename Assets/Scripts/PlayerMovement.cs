using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool bIsMoving = false;
    private Vector3 OrigPos, TargetPos;
    public float TimeToMove = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !bIsMoving)
            StartCoroutine(MovePlayer(Vector3.up));

        if (Input.GetKey(KeyCode.S) && !bIsMoving)
            StartCoroutine(MovePlayer(Vector3.down));

          
        if (Input.GetKey(KeyCode.D) && !bIsMoving)
            StartCoroutine(MovePlayer(Vector3.right));

        if (Input.GetKey(KeyCode.A) && !bIsMoving)
            StartCoroutine(MovePlayer(Vector3.left));


    }


    private IEnumerator MovePlayer(Vector3 Direction)
    {
        bIsMoving = true;
        float ElapsedTime = 0;

        OrigPos = transform.position;
        TargetPos = OrigPos + Direction;
       

        while (ElapsedTime < TimeToMove)
        {
            transform.position = Vector3.Lerp(OrigPos, TargetPos, (ElapsedTime / TimeToMove));
            ElapsedTime += Time.deltaTime;
            Debug.Log(transform.position);
            yield return null;
        }

        transform.position = TargetPos;
        bIsMoving = false;
    }
}
