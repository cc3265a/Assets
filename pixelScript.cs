using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pixelScript : MonoBehaviour
{
    public bool path = false;
    public GameObject gmo;

    public Color wallColor;
    public Color pathColor;

    Renderer rend;

    public int x = -1;
    public int y = -1;

    void Start()
    {
        // GameObject gmObj = GameObject.Find("GameManagerObj");
        Component gm = gameObject.GetComponent("GameManager");
        rend = gameObject.GetComponentInChildren<Renderer>();
        UpdateColor();

    }
    void Update()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        if (path){
            rend.material.color = pathColor;
            // Debug.Log("PATH");
        }
        else{
            rend.material.color = wallColor;
            // Debug.Log("WALL");
        }
    }
}
