using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManger : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public float zSpawn =0; //這是代表road的Z值
    public float roadLength = 30; //road 的長度
    public int numberOfRoad =5;//要出現幾次道路
    private List<GameObject> activeRoads = new List<GameObject>();

    public Transform playerTransform;//玩家

    void Start()
    {
        for(int i = 0 ; i < numberOfRoad; i++)//會開始自動生成初始一開始道路
        {
            if(i==0)
                SpawnTile(1);
            else
                SpawnTile(Random.Range(0,roadPrefabs.Length));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z -35 > zSpawn-(numberOfRoad*roadLength))//用來判斷現在玩家位置已經離已生成道路的末端位置距離35單位了 所以要再生成新的道路
        {
            int randomIndex = Random.Range(0, roadPrefabs.Length);
            if (roadPrefabs[randomIndex] != null)
            {
                SpawnTile(randomIndex);
                DeleteRoad();
            }
            else
            {
                Debug.LogError("Prefab at index " + randomIndex + " is null.");
            }
            }
    }
    public void SpawnTile(int roadIndex)//生成之後的道路 
    {
        if (roadIndex >= 0 && roadIndex < roadPrefabs.Length  && roadPrefabs[roadIndex] != null)
        {
            GameObject go = Instantiate(roadPrefabs[roadIndex], transform.forward * zSpawn, transform.rotation);//生成物件,物件位置,物件旋傳值
            activeRoads.Add(go);
            zSpawn += roadLength;
        }
        else
        {
            Debug.LogError("Invalid roadIndex: " + roadIndex);
        }
    }
    private void DeleteRoad() //刪除走過的道路
    {
        if (activeRoads.Count > 0)
        {
            Destroy(activeRoads[0]);
            activeRoads.RemoveAt(0);
        }
        else
        {
            Debug.LogWarning("No roads to delete.");
        }
    }
}