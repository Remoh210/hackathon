using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Characters;

    public KeyCode MoveUpKey    = KeyCode.W;
    public KeyCode MoveDownKey  = KeyCode.S;
    public KeyCode MoveLeftKey  = KeyCode.A;
    public KeyCode MoveRightKey = KeyCode.D;
    public KeyCode SwitchCharacterKey = KeyCode.LeftControl;

    private GameObject CurrentCharacter;


    private int count = 0;

    void Start()
    {
        for(int i = 0; i < Characters.Length; i++)
        {
            if(Characters[i].GetComponent<PlayerMovement>())
            {
                Characters[i].GetComponent<PlayerMovement>().enabled = false;
            }
            else
            {
                Debug.LogFormat("Character {0} does not have PlayerMovement component!", i);
            }
        }

        //Enable first characters
        if (Characters[0].GetComponent<PlayerMovement>())
        {
            Characters[0].GetComponent<PlayerMovement>().enabled = true;
            CurrentCharacter = Characters[0];
        }
        else
        {
            Debug.LogFormat("Character 0 does not have PlayerMovement component!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> AvailableCharacters = new List<GameObject>();
        foreach (var character in Characters)
        {
            if (character.activeSelf)
            {
                AvailableCharacters.Add(character);
            }
        }

        if (!CurrentCharacter.activeSelf && AvailableCharacters.Count > 0)
        {
            SwitchCharacter(GetNextCharacter());
        }

        if (Input.GetKeyDown(SwitchCharacterKey))
        {
            if (AvailableCharacters.Count > 1)
            {
                SwitchCharacter(GetNextCharacter());
            }
            else
            {
               Debug.Log("No Available Characters!");
            }
               
        }

        if (Input.GetKeyDown(MoveUpKey))
            CurrentCharacter.GetComponent<PlayerMovement>().SetDirection(EDirection.UP);
        if (Input.GetKeyDown(MoveDownKey))
            CurrentCharacter.GetComponent<PlayerMovement>().SetDirection(EDirection.DOWN);
        if (Input.GetKeyDown(MoveLeftKey))
            CurrentCharacter.GetComponent<PlayerMovement>().SetDirection(EDirection.LEFT);
        if (Input.GetKeyDown(MoveRightKey))
            CurrentCharacter.GetComponent<PlayerMovement>().SetDirection(EDirection.RIGHT);
    }

    private void SwitchCharacter(GameObject Character)
    {
        if(Character == null) { return; }

        PlayerMovement CurCharMovement = CurrentCharacter.GetComponent<PlayerMovement>();
        if(CurCharMovement)
        {
            CurCharMovement.DisableMovement();
            CurCharMovement.enabled = false;
        }

        ParticleSystem CurCharParticle = CurrentCharacter.GetComponent<ParticleSystem>();
        if (CurCharParticle)
        {
            CurCharParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        PlayerMovement NewCharMovement = Character.GetComponent<PlayerMovement>();
        if(NewCharMovement)
        {
            Character.GetComponent<PlayerMovement>().enabled = true;
        }

        ParticleSystem NewCharParticle = Character.GetComponent<ParticleSystem>();
        if (NewCharParticle)
        {
            NewCharParticle.Play();
        }

        CurrentCharacter = Character;
    }

    private GameObject GetNextCharacter()
    {
        if (count < Characters.Length - 1)
        {
            count++;
        } 
        else
        {
            count = 0;
        }

        GameObject SelectedCharacter = Characters[count];
        if (SelectedCharacter.activeSelf)
        {
            return SelectedCharacter;
        }
        else
        {
            return GetNextCharacter();
        }
        
    }

    public bool CheckAlive()
    {
        for(int i = 0; i < Characters.Length; i++)
        {
            if(Characters[i].activeSelf) return true;
        }

        return false;
    }
}
