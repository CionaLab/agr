using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// This script takes a series of CSV files with pixel values and creates a 3D mesh of it
public class ConvertCsv2Mesh : MonoBehaviour
{
    public float xVal,xyScale,zScale; //xVal is for flipping the x axis in case the animal is facing a different direction
    private bool notOver; //to determine when the code is finished generating the mesh
    public GameObject pxl; //Prefabricated cube
    public int z,totalZ; //keeping track of the progress from slices
    private int y; //keeping track of the row of current slice
    private TextAsset currCSV; //current CSV file generating the mesh
    private string[] currHeight,currWidth; //all values in the CSV as a string
    public string saveName,color; //filename to be saved, and what color channel to start with

    // Start is called before the first frame update
    void Start()
    {
        notOver = true;
        y = 0;
        currCSV = Resources.Load("CSVs/" + color + z) as TextAsset;
        currHeight = currCSV.text.Split('\n');
    }

    // Update is called once per frame
    void Update()
    {
        if (notOver)
        {
            currWidth = currHeight[y].Split(',');
            for (int i = 0; i < currWidth.Length; i++)
            {
                float pxVal = float.Parse(currWidth[i]);
                if (pxVal != 0.0f)
                {
                    Vector3 startPos = new Vector3(i * xyScale * xVal, y * xyScale, z * zScale);
                    GameObject box = Instantiate(pxl) as GameObject;
                    box.transform.localScale = new Vector3(xyScale, xyScale, zScale);
                    box.transform.position = startPos;
                    box.transform.SetParent(transform);
                }
            }
            y++;
            if (y == currHeight.Length-1)
            {
                z++;
                Debug.Log(z);
                y = 0;
                if (z > totalZ)
                {
                    notOver = false;
                    // unifies all the cubes into a single mesh at the end of the image
                    Unify();
                }
                else
                {
                    // every ten slices, all the current voxel cubes are combined in order to avoid too many objects in the scene
                    if (z % 10 == 1)
                    {
                        Unify();
                    }
                    currCSV = Resources.Load("CSVs/" + color + z) as TextAsset;
                    currHeight = currCSV.text.Split('\n');
                }
            }
        }
    }

    // combines all the voxel cubes into a single mesh
    void Unify()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int x = 0;
        while (x < meshFilters.Length)
        {
            combine[x].mesh = meshFilters[x].sharedMesh;
            combine[x].transform = meshFilters[x].transform.localToWorldMatrix;

            x++;
        }

        int allChilds = transform.childCount-1;
        for (int i = allChilds; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        if ((notOver) && (allChilds >= 0))
        {
            GameObject box = Instantiate(pxl,new Vector2(0.0f,0.0f),pxl.transform.rotation);
            box.GetComponent<MeshFilter>().mesh = new Mesh();
            box.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            box.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            box.transform.SetParent(transform);
        }
        else
        {
            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            AssetDatabase.CreateAsset(this.transform.GetComponent<MeshFilter>().mesh, "Assets/Resources/meshes/" + saveName + ".asset");
        }
    }
}
