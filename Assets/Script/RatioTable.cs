
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    Can be used
        set itemA(30%), itemB(50%), itemC(20%) and get random item
        => Add(itemA, 3), Add(itemB, 5), Add(itemC, 2), GetRandomGameobject()
*/
[System.Serializable]
public class RatioTable
{
    public List<int> _ratioList = new();
    List<GameObject> _prefabList = new();
    public void Add(string prefab, int ratio)
    {
        _ratioList.Add(ratio);
        _prefabList.Add(Resources.Load(prefab) as GameObject);
    }
    public GameObject GetRandomGameobject()
    {
        int probability = Random.Range(0, _ratioList.Sum());
        GameObject gameObject = null;
        for (int i = 0; i < _ratioList.Count; i++)
        {
            probability -= _ratioList[i];
            if (probability < 0)
            {
                gameObject = _prefabList[i];
                break;
            }
        }
        return gameObject;
    }
}
