using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelCollider : MonoBehaviour
{
    public string currColor; //voxel color
    public int voxelValue; //voxel value

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5); //this destroys the voxel afrer 5 seconds. This long time assures that it will have time to collide with a neuron (if in contact). Destroying it also avoids overloading scene with too many cubes
    }

    //This is called once when this object first contacts another object
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Neuron"))
        {
            NeuronDetails currNeuron = collision.gameObject.transform.parent.transform.parent.GetComponent<NeuronDetails>();
            if (this.currColor == "red")
            {
                currNeuron.red++; //adds to counter of how many voxels of this color touched that neuron
                currNeuron.redV += voxelValue; //adds the value of the voxel to the neuron
            }
            if (this.currColor == "green")
            {
                currNeuron.green++;
                currNeuron.greenV += voxelValue;
            }
            if (this.currColor == "blue")
            {
                currNeuron.blue++;
                currNeuron.blueV += voxelValue;
            }
        }
    }
}
