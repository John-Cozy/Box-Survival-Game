using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public string PickupType;
    public Renderer Renderer;

    IEnumerator Blink(float waitTime, int nTimes, float timeOn, float timeOff) {
        yield return new WaitForSeconds(waitTime);

        while (nTimes > 0) {
            Renderer.enabled = true;
            yield return new WaitForSeconds(timeOn);
            Renderer.enabled = false;
            yield return new WaitForSeconds(timeOff);
            nTimes--;
        }

        if (timeOn < .1f) {
            Destroy(gameObject);
        } else {
            StartCoroutine(Blink(0, 5, timeOn / 2, .1f));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink(5, 5, .6f, .1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
