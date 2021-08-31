using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public int zPos = -10;
    public bool creatingSection = false;
    public int secNum;
    void Update()
    {
        if(creatingSection == false)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0,2);
        Instantiate(section[secNum], new Vector3(0,0,zPos), transform.rotation * Quaternion.Euler (0f, 90, 0f));
        zPos += 19;
        yield return new WaitForSeconds(3);
        creatingSection = false;
    }
}
