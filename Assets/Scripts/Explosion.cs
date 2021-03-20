using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour
{
    public GameObject TextPrefab;
    public int numOfPoints;

    // Start is called before the first frame update
    void Start() {
        Vector3 position = GameObject.Find("Camera").GetComponent<Camera>().WorldToScreenPoint(transform.position);
        GameObject points = Instantiate(TextPrefab, position, Quaternion.identity, GameObject.Find("Canvas").transform);
        points.GetComponent<Text>().text = "" + numOfPoints;

        Destroy(points, 2f);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
