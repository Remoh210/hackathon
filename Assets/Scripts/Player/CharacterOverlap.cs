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
    public LayerMask DamageLayer;
    public float DeahAnimDuration = 0.7f;

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
        GameObject CollidedGameObjet = col.gameObject;
        CharacterOverlap enemyOverlapComp = CollidedGameObjet.GetComponent<CharacterOverlap>();
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


        if ((DamageLayer & 1 << CollidedGameObjet.layer) == 1 << CollidedGameObjet.layer)
        {        
            Die();
        }
    }
    private void Die()
    {
        ParticleSystem SelectionParticleSystem = GetComponent<ParticleSystem>();
        if(SelectionParticleSystem)
        {
            SelectionParticleSystem.Stop();
        }
        StartCoroutine(DeathAnimation());
    }

    private IEnumerator DeathAnimation()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(0.0f, 0.0f, 0.0f);

        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 720.0f;

        float currentTime = 0.0f;
        do
        {
            //Scale
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / DeahAnimDuration);
            //Rotate
            float yRotation = Mathf.Lerp(startRotation, endRotation, currentTime / DeahAnimDuration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);

            currentTime += Time.deltaTime;

            yield return null;
        } while (currentTime <= DeahAnimDuration);

        gameObject.SetActive(false);
    }
}
