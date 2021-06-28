using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ReputationSystem : MonoBehaviour
{
    public void addingReputation(Characters character)
    {
        character.setReputation(character.getReputation() + 1);
    }

    public int checkingReputation(Characters character)
    {
        return character.getReputation();
    }
}
