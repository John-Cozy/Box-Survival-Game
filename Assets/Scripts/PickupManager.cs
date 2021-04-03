using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public WeightedList PickupTypes;

    private static PickupManager Singleton;

    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
        PickupTypes.CalculateWeightTotal();
    }

    public static void EnableHealthPickups() {
        Singleton.PickupTypes.SetWeight(0, 1);
    }

    public static void DisableHealthPickups() {
        Singleton.PickupTypes.SetWeight(0, 0);
    }

    public static void PlaceRandomPickup(Vector3 position) {


        Instantiate(Singleton.PickupTypes.GetRandomWeightedObject(), position, Quaternion.identity);
    }
}
