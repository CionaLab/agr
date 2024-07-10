# Voxel Analysis user guide

## Import neuron reconstruction

1. Download the reconstruction from
[Zenodo](https://doi.org/10.5281/zenodo.12705627).
2. Import the meshes to Unity.

## Creating Mesh in Unity

1. Run the MATLAB code `in_situ_to_CSVs.m` to get CSV files
2. Move all the CSV files into the Unity folder `Resources/CSVs`
3. If inactive, set the gameobject `meshcreator` to active
4. In the inspector window for `meshcreator`, go to the component for the
"Convert Csv 2 Mesh (Script)" and fill in the necessary values. For more
information, check out the scripts
    - X Val (if the mesh needs to be mirrored/flipped on x-axis; 1 for no, -1
    for yes)
    - Xy scale (pixel reoslution of the Tiff Stack in um)
    - Z Scale (thickness of Tiff stack slice in um)
    - Z (the slice to start on; default is 1)
    - Total Z (number of slices in the Tiff stack per channel)
    - Save Name (the name given to the mesh when it gets saved)
    - Color (the color channel to start on)
5. Hit play. The debugger will update on the progress. When finished, the meshes
will save under `Resources/meshes`

## Aligning meshes to neurons

1. Add a fresh "Expression Object" from the Prefabs folder into the scene. This
object has 4 children object, allowing you to put up to channels at the same
time.
2. Add a mesh into one of the 3 mesh objects. Repeat until all the channels are
assigned to the objects. (Optional, set unused Mesh # object to inactive)
3. The expression object should look like your TIFF stack with all visible
channels. Now it should be easy to hide/unhide channels by gameing the object
inactive.
4. Rotate & move the parent object (Expression Object) to align with the neurons
(The neurons are separated into High Res and Low Res; avoid changing anything
about these objects! Instead, you can just make the whole thing active or
inactive depending on which dataset you want to use. Moving the parent object
should be fine).

## Running the Analysis

1. In the inspector window for "Expression Object", go to the component for the
"Check For Voxel (Script)" and fill in the necessary values
    - Results (this is where you get the analysis results when it's finished).
    Click in, select all, copy and paste
    - X Mod (same as X Val from the mesh creator)
    - Scalexy (pixel reoslution of the Tiff Stack in um)
    - Scalez (thickness of Tiff stack slice in um)
    - Slices (number of slices per channel)
    - Red T (threshold value for the red channel)
    - Green T (threshold value for the green channel)
    - Blue T (threshold value for the blue channel)
    - Start Color (which channel with which to start; goes in the order red,
    green, and blue)
    - BV Or MG (This is to determine if using the coordinates for BV or for MG)
    - M Gpos (transform position of the expression object in reference to the MG)
    - M Grot (transform rotation of the expression object in reference to the MG)
    - B Vpos (transform position of the expression object in reference to the BV)
    - B Vrot (transform rotation of the expression object in reference to the BV)
    - Is Red (if it has a red channel)
    - Is Green (if it has a green channel)
    - Is Blue (if it has a blue channel)
2. Hit play. The debugger will update on the progress. When finished, it will
create a CSV string that can be copied from Results.
