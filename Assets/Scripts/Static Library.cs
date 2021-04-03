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

[System.Serializable]
public struct WeightedObject {
    public GameObject Prefab;
    public float Weight;
}

[System.Serializable]
public class WeightedList {
    public WeightedObject[] Objects;

    private float totalWeight;

    public GameObject GetRandomWeightedObject() {

        float enemyWeightPosition = Random.Range(0, totalWeight);
        float currentWeightPosition = 0;

        foreach (WeightedObject o in Objects) {
            currentWeightPosition += o.Weight;

            if (currentWeightPosition > enemyWeightPosition) {
                return o.Prefab;
            }
        }

        throw new System.Exception("Failed to return weighted object");
    }

    public void CalculateWeightTotal() {
        totalWeight = 0;

        foreach (WeightedObject o in Objects) {
            totalWeight += o.Weight;
        }
    }

    public void SetWeight(int index, float weight) {
        Objects[index].Weight = weight;
        CalculateWeightTotal();
    }
}