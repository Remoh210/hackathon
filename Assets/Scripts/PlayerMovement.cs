using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement stuff
    public GameObject testUI;

    public Grid MovementGrid;
    public LayerMask MovementCollision;
    public float TimeToMove = 0.2f;

    //Used by MusicManager
    public bool bAllowToMove = false;

    private bool bIsMoving = false;
    private Vector3 OrigPos;
    private Vector3Int TargetPosition;
    private Vector2 CartesianPos;

    private Vector3Int curDir;

    private Animator Animator;

    private EDirection Dir;

    enum EDirection
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        NONE 
    }

    // Start is called before the first frame update
    void Start()
    {  
        Animator = transform.GetComponent<Animator>();
        TargetPosition = MovementGrid.WorldToCell(transform.position);
        Dir = EDirection.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        HadleInput();

        if (bAllowToMove)
        {
            bAllowToMove = false;
            if (!IsColliding(TargetPosition, 0.2f) && Dir != EDirection.NONE)
            {
                TargetPosition += curDir;
                StartCoroutine(MovePlayer(TargetPosition));
            }         
        }
    }

    private IEnumerator MovePlayer(Vector3Int TargetPos)
    {
        Vector3 TargetPosWorld = MovementGrid.CellToWorld(TargetPos);
        OrigPos = transform.position;

        bIsMoving = true;
        Animator.SetFloat("Speed", 0.2f);
        float ElapsedTime = 0;
        while (ElapsedTime < TimeToMove)
        {
            transform.position = Vector3.Lerp(OrigPos, TargetPosWorld, (ElapsedTime / TimeToMove));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = TargetPosWorld;
        bIsMoving = false;
        Animator.SetFloat("Speed", 0.0f);
    }

    private bool IsColliding(Vector3 TargetPos, float Radius)
    {
        bool bIsCollidng = Physics2D.OverlapCircle(TargetPos, Radius, MovementCollision);
        //Debug.Log("Colliding");
        return bIsCollidng;
    }

    private Vector3 CartesianToIso(Vector3 CartesianPos)
    {
       return new Vector3(CartesianPos.x - CartesianPos.y, (CartesianPos.x + CartesianPos.y)/2, 0);
    }

    private void HadleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && !bIsMoving)
        {
            curDir = Vector3Int.up;
            Dir = EDirection.UP;
        }
        if (Input.GetKeyDown(KeyCode.S) && !bIsMoving)
        {
            curDir = Vector3Int.down;
            Dir = EDirection.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.D) && !bIsMoving)
        {
            curDir = Vector3Int.right;
            Dir = EDirection.RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.A) && !bIsMoving)
        {
            curDir = Vector3Int.left;
            Dir = EDirection.LEFT;
        }
    }
}
