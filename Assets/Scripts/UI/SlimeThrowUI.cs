using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeThrowUI : MonoBehaviour
{
    [SerializeField] Transform slimeUIHolder;
    [SerializeField] GameObject SlimeUIParentPrefab;
    [SerializeField] Mesh currentSlimeMesh;
    [SerializeField] Material currentSlimeMaterial;
    [SerializeField] Material transparentMaterial;

    [SerializeField] private float numberOfThrows;
    // Start is called before the first frame update
    void Start()
    {
        //Test
        SetSlimeModel(currentSlimeMesh, currentSlimeMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlimeModel(Mesh slime, Material mat)
    {
        currentSlimeMesh = slime;

        transparentMaterial.color = mat.color;

        currentSlimeMaterial = transparentMaterial;

        PopulateUI();
    }

    public void PopulateUI()
    {
        for(int i = 0; i < numberOfThrows;  i++)
        {
            GameObject g = Instantiate(SlimeUIParentPrefab, slimeUIHolder);
            GameObject gChild = g.transform.GetChild(0).gameObject;

            gChild.GetComponent<MeshFilter>().mesh = currentSlimeMesh;
            gChild.GetComponent<MeshRenderer>().material = currentSlimeMaterial;
        }
    }
}
