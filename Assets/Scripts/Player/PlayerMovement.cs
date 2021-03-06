using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EDirection
{
    UP,
    DOWN,
    RIGHT,
    LEFT,
    NONE
}


public enum EFlipDir
{
    UP,
    DOWN,
}

public class PlayerMovement : MonoBehaviour
{
    //Movement stuff
    public Grid MovementGrid;
    //public LayerMask MovementCollision;
    public Vector2Int MaxXY;
    public Vector2Int MinXY;
    public float TimeToMove = 0.2f;

    public GameObject TargetDebug;

    public float TargetTileCollisionRadius = 0.1f;

    public float FlipAnimTime = 0.5f;

    //Set by PlayerBeatListner component
    [HideInInspector]
    public bool bAllowToMove = false;

    private Vector3 OrigPos;
    private Vector3Int TargetPosition;
    private Vector2 CartesianPos;

    private Vector3Int IsoDir;

    private Animator Animator;

    private EDirection Dir;

    private float CurrentRotation = 0.0f;

    private float TargetRotation = 0.0f;

    private bool bShouldRotate = false;



    Vector3 OffsetCellToWorld(Vector3Int CellPosition)
    {
        Vector3 newPos = MovementGrid.CellToWorld(TargetPosition) + new Vector3(0, (MovementGrid.cellSize.y / 2.0f), 1);
        return newPos;
    }
    private void Awake()
    {
        Animator = transform.GetComponent<Animator>();
        TargetPosition = MovementGrid.WorldToCell(transform.position);
        transform.position = OffsetCellToWorld(TargetPosition);
        Dir = EDirection.NONE;
    }

    void Update()
    {
        
        if (bAllowToMove)
        { 
            if (TargetDebug)
            {
                TargetDebug.transform.position = OffsetCellToWorld(TargetPosition + IsoDir);
            }

            Vector3Int FuturePos = TargetPosition + IsoDir;
            
            bool bShouldReturn = false;
            bShouldRotate = false;

            if(FuturePos.x >= MaxXY.x)
            {
                bShouldReturn = true;
            }
            else if (FuturePos.y >= MaxXY.y)
            {

                bShouldReturn = true;
                bShouldRotate = true;
            }
            else if (FuturePos.x <= MinXY.x)
            {
                bShouldReturn = true; 
            }
            else if (FuturePos.y <= MinXY.y)
            {
                bShouldRotate = true;
                bShouldReturn = true;
            }

            if (bShouldReturn)
            {
                if(bShouldRotate)
                {
                    if (CurrentRotation == 180)
                        TargetRotation = 0;
                    else
                        TargetRotation = 180;
                }

                IsoDir *= -1;
            }


            if(Dir != EDirection.NONE)
            {
                //Debug.LogFormat("Current Rot {0}, Tartget Rot{1}", CurrentRotation, TargetRotation);
                if (CurrentRotation != TargetRotation)
                {
                    StartCoroutine(Flip(180));
                    CurrentRotation = TargetRotation;
                }

                TargetPosition += IsoDir;
                StartCoroutine(MovePlayer(TargetPosition));
            }

            bAllowToMove = false;
        }
    }

    private IEnumerator MovePlayer(Vector3Int TargetPos)
    {
        Vector3 TargetPosWorld = OffsetCellToWorld(TargetPos);
        OrigPos = transform.position;

        //if(Animator) { Animator.SetFloat("Speed", 0.2f); }
        float ElapsedTime = 0;
        while (ElapsedTime < TimeToMove)
        {
            transform.position = Vector3.Lerp(OrigPos, TargetPosWorld, (ElapsedTime / TimeToMove));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = TargetPosWorld;
        //if (Animator) { Animator.SetFloat("Speed", 0.0f); }
    }

    private Vector3 CartesianToIso(Vector3 CartesianPos)
    {
       return new Vector3(CartesianPos.x - CartesianPos.y, (CartesianPos.x + CartesianPos.y)/2, 0);
    }

    //Called by PlayerManagerComponent
    public void SetDirection(EDirection Direction)
    {
        Dir = Direction;
        switch (Direction)
        {
            case EDirection.UP:
                IsoDir = Vector3Int.up;
                bShouldRotate = true;
                TargetRotation = 180;
                break;
            case EDirection.DOWN:
                IsoDir = Vector3Int.down;
                TargetRotation = 0;
                bShouldRotate = true;
                break;
            case EDirection.LEFT:
                IsoDir = Vector3Int.left;
                break;
            case EDirection.RIGHT:
                IsoDir = Vector3Int.right;
                break;
        }
       
    }

    public void DisableMovement()
    {
        Dir = EDirection.NONE;
    }

    private IEnumerator Flip(float Angle)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + Angle;

        float currentTime = 0.0f;
        do
        {
            //Rotate
            float yRotation = Mathf.Lerp(startRotation, endRotation, currentTime / FlipAnimTime) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);

            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= FlipAnimTime);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, endRotation, transform.eulerAngles.z);
    }
}
