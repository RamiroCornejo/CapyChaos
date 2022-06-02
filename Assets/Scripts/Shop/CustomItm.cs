using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomItem", menuName = "Shop/Custom")]
public class CustomItm : ShopItem
{
    [SerializeField] Mesh headMesh = null;
    [SerializeField] Mesh bodyMesh = null;
    [SerializeField] Mesh rightArmMesh = null;
    [SerializeField] Mesh leftArmMesh = null;
    [SerializeField] Mesh rightLegMesh = null;
    [SerializeField] Mesh leftLegMesh = null;
    [SerializeField] Material mat = null;

    public override void RefreshItem(CustomAssingment assign)
    {
        MeshFilter[] meshes = null;
#if UNITY_EDITOR
        meshes = assign.character.GetComponentsInChildren<MeshFilter>();
#else
        meshes = assign.characterPrefab.GetComponentsInChildren<MeshFilter>();
#endif

        meshes[0].mesh = leftArmMesh;
        meshes[1].mesh = bodyMesh;
        meshes[2].mesh = headMesh;
        meshes[3].mesh = rightArmMesh;
        meshes[4].mesh = leftLegMesh;
        meshes[5].mesh = rightLegMesh;

        MeshFilter[] corpseMeshes = null;

#if UNITY_EDITOR
        corpseMeshes = assign.corpse.GetComponentsInChildren<MeshFilter>();
#else
        corpseMeshes = assign.corpsePrefab.GetComponentsInChildren<MeshFilter>();
#endif

        corpseMeshes[0].mesh = leftArmMesh;
        corpseMeshes[1].mesh = bodyMesh;
        corpseMeshes[2].mesh = headMesh;
        corpseMeshes[3].mesh = rightArmMesh;
        corpseMeshes[4].mesh = leftLegMesh;
        corpseMeshes[5].mesh = rightLegMesh;


        meshes[0].GetComponent<MeshRenderer>().material = mat;
        meshes[1].GetComponent<MeshRenderer>().material = mat;
        meshes[2].GetComponent<MeshRenderer>().material = mat;
        meshes[3].GetComponent<MeshRenderer>().material = mat;

        corpseMeshes[0].GetComponent<MeshRenderer>().material = mat;
        corpseMeshes[1].GetComponent<MeshRenderer>().material = mat;
        corpseMeshes[2].GetComponent<MeshRenderer>().material = mat;
        corpseMeshes[3].GetComponent<MeshRenderer>().material = mat;
    }
}
