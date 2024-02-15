using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IDDisplayScript : MonoBehaviour
{
    int _parentID;
    TMP_Text _textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        _parentID = gameObject.transform.parent.GetInstanceID();
        _textDisplay = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowIDOnText(_parentID);
    }

    void ShowIDOnText(int ID)
    {
        _textDisplay.text = ID.ToString();
    }
}
