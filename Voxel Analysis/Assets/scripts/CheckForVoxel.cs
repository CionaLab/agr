using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CheckForVoxel : MonoBehaviour
{
    public GameObject vxl; //Prefabricated cube
    public string results; //csv results of analysis
    private string currIMG; //current CSV file channel
    private TextAsset currCSV; //current CSV file
    private string[] currHeight, currWidth; //all values in the CSV as a string
    private int i, j, threshold; //i and j keep track of progress; threshold only adds voxel with a value above the threshold
    public float xMod, scalexy, scalez; //xMod is for flipping the x axis in case the animal is facing a different direction
    public int slices, redT, greenT, blueT; //T is for the individual values for different channels
    private bool done;
    public string startColor, BVOrMG; //startColor is for which channel to start with; BVOrMG is for if testing the brain vesicle or motor ganglion
    public Vector3 MGpos, MGrot, BVpos, BVrot; //These represent the position and rotation of the expression object based on motor ganglion or brain vesicle
    public bool isRed, isGreen, isBlue; //if that color channel exists in the stack

    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        j = 0;
        results = "";
        currIMG = startColor;
        if (currIMG == "red")
        {
            threshold = redT;
        }
        else if (currIMG == "green")
        {
            threshold = greenT;
        }
        else if (currIMG == "blue")
        {
            threshold = blueT;
        }
        currCSV = Resources.Load("Mesh Coordinates/" + this.transform.gameObject.name + "/" + currIMG + "" + i) as TextAsset;
        currHeight = currCSV.text.Split('\n');
        currWidth = currHeight[j].Split(',');
        done = true;
        if (BVOrMG == "MG")
        {
            this.transform.position = MGpos;
            this.transform.eulerAngles = MGrot;
        }
        else if (BVOrMG == "BV")
        {
            this.transform.position = BVpos;
            this.transform.eulerAngles = BVrot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= slices)
        {
            //does only one line of CSV per frame to avoid overloading
            j++;
            if (j >= currHeight.Length - 1)
            {
                j = 0;
                Debug.Log(currIMG + ": " + i + "/" + slices);
                i++;
                if (i > slices)
                {
                    if ((currIMG == "red") && (isGreen))
                    {
                        currIMG = "green";
                        i = 1;
                        threshold = greenT;
                    }
                    else if ((isBlue) && (currIMG != "blue"))
                    {
                        currIMG = "blue";
                        i = 1;
                        threshold = blueT;
                    }
                }
                if (i <= slices)
                {
                    currCSV = Resources.Load("Mesh Coordinates/" + this.transform.gameObject.name + "/" + currIMG + "" + i) as TextAsset;
                    currHeight = currCSV.text.Split('\n');
                }
            }
            currWidth = currHeight[j].Split(',');
            readVoxels();
        }
        else if (done)
        {
            Debug.Log("Calculating voxels...");
            calculateVoxels();
            done = false;
        }
    }

    //loads voxel prefab into scene
    void readVoxels()
    {
        for (var k = 0; k < currWidth.Length; k++)
        {
            if (int.Parse(currWidth[k]) > threshold)
            {
                Vector3 startPos = new Vector3(xMod * k * scalexy, j * scalexy, (i - 1) * scalez); //put voxel relative to tis space in the original image
                GameObject box = Instantiate(vxl, this.gameObject.transform) as GameObject;
                box.GetComponent<VoxelCollider>().currColor = currIMG; //assigns color to voxel
                box.GetComponent<VoxelCollider>().voxelValue = int.Parse(currWidth[k]); //assigns value to voxel
                box.transform.localScale = new Vector3(scalexy, scalexy, scalez);
                box.transform.localPosition = startPos; //local position applies the transform from the mapping of the expression to neurons
            }
        }
    }

    //Called at the end to compile analysis. See VoxelCollider.cs for analysis values
    void calculateVoxels()
    {
        GameObject[] allNeurons = GameObject.FindGameObjectsWithTag("Neuron");
        foreach (GameObject currNeur in allNeurons)
        {
            NeuronDetails tempNeur = currNeur.GetComponent<NeuronDetails>();
            results += "\n" + currNeur.name + "," + tempNeur.red + "," + tempNeur.green + "," + tempNeur.blue + "," + tempNeur.redV + "," + tempNeur.greenV + "," + tempNeur.blueV;
        }
    }
}
