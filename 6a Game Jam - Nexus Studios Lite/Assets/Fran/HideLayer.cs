using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLayer : MonoBehaviour
{
    public LayerMask layerToHide; // The layer to hide
    public Material hiddenMaterial; // The material to use for hiding the mesh

    private void Start()
    {
        // Get the mesh renderer component
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Check if the layerToHide matches the mesh renderer's layer
        if (((1 << gameObject.layer) & layerToHide) != 0)
        {
            // Assign the hidden material to the mesh renderer
            meshRenderer.material = hiddenMaterial;
        }
    }

}
