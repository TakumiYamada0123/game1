using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventText : MonoBehaviour {

    public int surviveTime;
    public int showingframe;
    public float hidableHeight = 250.0f;
    public RectTransform textPosition;

    private int frameCounter;
    private float currentHeight;
    private float protectZFighting = 0.1f;
    private Vector3 StartPosition;
    private Vector3 EndPosition;
    private int oneWayTime;
    private int wating;

	// Use this for initialization
	void Start () {
        frameCounter = 0;
        currentHeight = hidableHeight;
        textPosition.GetComponent<RectTransform>();
        StartPosition = new Vector3(textPosition.position.x, hidableHeight * 2.5f, protectZFighting);
        EndPosition = new Vector3(textPosition.position.x, hidableHeight, protectZFighting);
        oneWayTime = surviveTime / 2;
        wating = -2;
    }
	
	// Update is called once per frame
	void Update () {

        if (frameCounter >= surviveTime) {
            Death();
            //Debug.Log("End!!!!!");
        }

        if (frameCounter <= oneWayTime) {
            float rate = (float)frameCounter / (float)oneWayTime;
            Outing(rate);
            //Debug.Log("down");

            if (frameCounter == oneWayTime && wating == -2) {
                wating = showingframe;
                //Debug.Log("wait_Time:" + wating);
            }
        }
        else {
            float rate = (float)(frameCounter - oneWayTime) / (float)oneWayTime;
            Returning(rate);
            //Debug.Log("up");
        }

        if (wating >= 0) {
            --wating;
            //Debug.Log("waiting");
        }
        else {
            ++frameCounter;
        }
    }

    private void Outing(float rate) {
        textPosition.position = Vector3.Lerp(StartPosition, EndPosition, rate);
    }

    private void Returning(float rate) {
        textPosition.position = Vector3.Lerp(EndPosition, StartPosition, rate);
    }

    private void Death() {
        Destroy(this.gameObject);
    }
}
