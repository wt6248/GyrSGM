using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IDDisplayScript : MonoBehaviour
{
    int parentID;
    TMP_Text textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        parentID = gameObject.transform.parent.GetInstanceID();
        textDisplay = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        showIDOnText(parentID);
    }

    void showIDOnText(int ID)
    {
        textDisplay.text = ID.ToString();
    }
}
