using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager 
{
    //Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();
    List<GameObject> _objects = new List<GameObject>();

    public void Add(GameObject go)
    { 
        _objects.Add(go);
    }

    public void Remove(GameObject go)
    {
        _objects.Remove(go);
    }

    public void Clear()
    { 
        _objects.Clear();
    }

    public GameObject Find(Vector3Int cellPos)
    {
        foreach(GameObject obj in _objects)
        {
            CreatureController cc = obj.GetComponent<CreatureController>();
            if(cc == null) continue;

            if(cc.CellPos== cellPos) 
                return obj;
        }
        return null;
    }
}
