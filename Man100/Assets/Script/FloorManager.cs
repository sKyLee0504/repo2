using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{   
    // 创建一个数组对象
    [SerializeField] private GameObject[] floorPrefabs;

    public void SpawnFloor()
    {
       int r = Random.Range(0, floorPrefabs.Length);
        // Instantiate(第二个参数可以控制floor在哪个父节点下生成
        GameObject floor =  Instantiate(floorPrefabs[r],transform);
        floor.transform.position = new Vector3(Random.Range(-3.8f, 3.8f), -6f, 0);
    }
}
