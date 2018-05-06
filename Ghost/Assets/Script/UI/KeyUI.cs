using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour {

    public GameObject forward;
    public GameObject right;
    public GameObject left;
    public GameObject backward;
    public GameObject punch;
    public GameObject possess;
    public GameObject jump;

    public Color color = new Color(0.875f, 0.35f, 0.35f);


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        forward.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        right.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        left.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        backward.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        punch.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        possess.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        jump.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);

        if (Input.GetKey(KeyCode.W))
        {
            forward.GetComponent<Image>().color = color;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right.GetComponent<Image>().color = color;
        }
        if (Input.GetKey(KeyCode.A))
        {
            left.GetComponent<Image>().color = color;
        }
        if (Input.GetKey(KeyCode.S))
        {
            backward.GetComponent<Image>().color = color;
        }
        if (Input.GetMouseButton(0))
        {
            punch.GetComponent<Image>().color = color;
        }
        if (Input.GetMouseButton(1))
        {
            possess.GetComponent<Image>().color = color;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            jump.GetComponent<Image>().color = color;
        }
    }
}
