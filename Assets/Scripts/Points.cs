using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour {

    void Start()
    {
        StartCoroutine(DeletePoints());
    }

    IEnumerator DeletePoints() {
        yield return new WaitForSeconds(2);
        GetComponent<UIGroup>().Hide();
        Destroy(gameObject, 1f);
    }
}
