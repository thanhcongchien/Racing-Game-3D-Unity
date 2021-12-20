using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class PickRandomMaterial : MonoBehaviour
{
    public Material[] materialList;
    public GameObject skinMaterialBody;
    public GameObject skinMaterialKart;
    private int index;

    private void OnEnable()
    {
        if (materialList.Length > 0)
        {
            index = Random.Range(0, materialList.Length - 1);
            this.skinMaterialBody.GetComponent<SkinnedMeshRenderer>().material = materialList[index];
            this.skinMaterialKart.GetComponent<SkinnedMeshRenderer>().material = materialList[index];
            Debug.Log("index: " + index);
        }
    }
    void Start()
    {
        //if(materialList.Length>0){
        //    index = Random.Range(0,materialList.Length-1);
        //    this.skinMaterial.GetComponent<SkinnedMeshRenderer> ().material = materialList[index];
        //}
    }

}
