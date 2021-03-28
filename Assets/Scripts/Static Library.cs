using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLibrary
{
    public static bool RandomBool(float successPercentage) {
        float chance = Random.Range(0, 100);

        if (chance < successPercentage) return true;
        else return false;
    }

    public static int RandomChoice(int choiceNumber) {
        return Random.Range(0, choiceNumber);
    }
}
