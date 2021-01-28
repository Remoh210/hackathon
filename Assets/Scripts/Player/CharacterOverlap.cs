using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ECharacterType
{
    Fire,
    Water,
    Earth
}
public class CharacterOverlap : MonoBehaviour
{
    public int Team;
    public ECharacterType CharacterType;
   
    private BoxCollider2D Collider;

    void Start()
    {
        Collider = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        DoCollision(col);
    }

    private void DoCollision(Collider2D col)
    {
        GameObject Enemy = col.gameObject;
        CharacterOverlap enemyOverlapComp = Enemy.GetComponent<CharacterOverlap>();
        if(enemyOverlapComp)
        {
            if(Team == enemyOverlapComp.Team)
            {
                return;
            }

            ECharacterType enemyType = enemyOverlapComp.CharacterType;

            switch (enemyType)
            {
                case ECharacterType.Water:
                    if(CharacterType == ECharacterType.Fire)
                    {
                        enemyOverlapComp.Die();
                    }
                    break;
                case ECharacterType.Earth:
                    if (CharacterType == ECharacterType.Water)
                    {
                        enemyOverlapComp.Die();
                    }
                    break;
                case ECharacterType.Fire:
                    if (CharacterType == ECharacterType.Earth)
                    {
                        enemyOverlapComp.Die();
                    }
                    break;
            }
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
