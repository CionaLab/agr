using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NeuronDetails : MonoBehaviour
{
    public int red, green, blue, redV, greenV, blueV;

    // Start is called before the first frame update
    void Awake()
    {
        red = 0;
        green = 0;
        blue = 0;
        redV = 0;
        greenV = 0;
        blueV = 0;
    }

    public void ResetVoxels()
    {
        red = 0;
        green = 0;
        blue = 0;
        redV = 0;
        greenV = 0;
        blueV = 0;
    }

}
