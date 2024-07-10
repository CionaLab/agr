using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;

public class NeuronLoader : MonoBehaviour
{
    public TextAsset csvFile;
    private string[] cells;
    private char lineSeperater = '\n';
    private char fieldSeperater = ',';
    public GameObject neuron;
    private string[] fields;

    // Start is called before the first frame update
    void Start()
    {
        cells = csvFile.text.Split(lineSeperater);
        foreach (string cell in cells)
        {
            fields = cell.Split(fieldSeperater);
            GameObject go = Instantiate(neuron, this.gameObject.transform) as GameObject;
        }
    }

    public string realID()
    {
        return fields[0];
    }

    public string getID()
    {
        return fields[1];
    }

    public string getType()
    {
        return fields[2];
    }

    public Vector3 getCor()
    {
        return new Vector3(-float.Parse(fields[3]), float.Parse(fields[4]), float.Parse(fields[5]));
    }

    public Vector3 getRadius()
    {
        float aR = 3 * (float.Parse(fields[10]) / (4 * (float)Math.PI));
        float aThird = 1.0f / 3.0f;
        float radius = (float)Math.Pow(aR, aThird);
        return new Vector3(radius * 2, radius * 2, radius * 2);
    }

    public string getNT1()
    {
        return fields[6];
    }

    public string getNT2()
    {
        return fields[7];
    }

    public string getBrainRegion()
    {
        return fields[8];
    }

    public string getBrainSide()
    {
        return fields[9];
    }
}
