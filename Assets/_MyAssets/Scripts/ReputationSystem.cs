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

    public int gettingSelfies(int rep, Characters character)
    {
        if (rep >= 3 && character.bronze == false)
        {
            character.bronze = true;
            return 1;
        }
        else if (rep >= 8 && character.silver == false)
        {
            character.silver = true;
            return 2;
        }
        else if (rep >= 13 && character.gold == false)
        {
            character.gold = true;
            return 3;
        }
        else
        {
            return 0;
        }
    }

}
