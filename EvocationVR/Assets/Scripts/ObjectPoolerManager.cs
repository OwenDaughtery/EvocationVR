using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}


public class ObjectPoolerManager : MonoBehaviour
{

    public static ObjectPoolerManager SharedInstance;
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;



    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {

                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }


    void Awake()
    {
        SharedInstance = this;
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    public void returnToPool(GameObject gameObject, string tag){
        gameObject.SetActive(false);
        pooledObjects.Add(gameObject);
        //following line causes issues with updating prefab 
        //gameObject.transform.parent = this.transform;
    }


}
