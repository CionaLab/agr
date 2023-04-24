using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NeuronDetails : MonoBehaviour
{
    public int red, green, blue, redV, greenV, blueV;
    private NeuronLoader spawnInfo;
    private GameObject[] orphans;
    public string realID;

    // Start is called before the first frame update
    void Awake()
    {
        spawnInfo = GameObject.Find("Neurons").GetComponent<NeuronLoader>();
        orphans = GameObject.FindGameObjectsWithTag("ReconstructedMeshes");
        realID = spawnInfo.realID();
        for (int i = orphans.Length - 1; i >=0;i--)
        {
            if (orphans[i].name == realID)
            {
                orphans[i].transform.SetParent(this.gameObject.transform, false);
                break;
            }
        }
        red = 0;
        green = 0;
        blue = 0;
        redV = 0;
        greenV = 0;
        blueV = 0;
    }
    
}
