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

public class PlayerMovement : MonoBehaviour
{
    //Movement stuff
    public Grid MovementGrid;
    public LayerMask MovementCollision;
    public float TimeToMove = 0.2f;

    public GameObject TargetDebug;

    public float TargetTileCollisionRadius = 0.1f;

    //Set by PlayerBeatListner component
    [HideInInspector]
    public bool bAllowToMove = false;

    private Vector3 OrigPos;
    private Vector3Int TargetPosition;
    private Vector2 CartesianPos;

    private Vector3Int IsoDir;

    private Animator Animator;

    private EDirection Dir;

    private void Awake()
    {
        Animator = transform.GetComponent<Animator>();
        TargetPosition = MovementGrid.WorldToCell(transform.position);
        transform.position = MovementGrid.CellToWorld(TargetPosition);
        Dir = EDirection.NONE;
    }

    void Update()
    {
        if (bAllowToMove)
        {
            if (TargetDebug)
            {
                TargetDebug.transform.position = MovementGrid.CellToWorld(TargetPosition);
            }
            if (IsColliding(MovementGrid.CellToWorld(TargetPosition + IsoDir), TargetTileCollisionRadius))
            {
                IsoDir *= -1;
            }

            if (!IsColliding(TargetPosition + IsoDir, TargetTileCollisionRadius) && Dir != EDirection.NONE)
            {
                TargetPosition += IsoDir;
                StartCoroutine(MovePlayer(TargetPosition));
            }

            bAllowToMove = false;
        }
    }

    private IEnumerator MovePlayer(Vector3Int TargetPos)
    {
        Vector3 TargetPosWorld = MovementGrid.CellToWorld(TargetPos);
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

    private bool IsColliding(Vector3 TargetPos, float Radius)
    {
        bool bIsCollidng = Physics2D.OverlapCircle(TargetPos, Radius, MovementCollision);
        return bIsCollidng;
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
                break;
            case EDirection.DOWN:
                IsoDir = Vector3Int.down;
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
}
