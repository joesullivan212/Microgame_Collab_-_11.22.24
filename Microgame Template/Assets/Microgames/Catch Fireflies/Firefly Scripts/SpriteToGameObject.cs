using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class SpriteToGameObject : MonoBehaviour
{
    public Texture[] textures;
    public Material sourceMaterial;
    public string savePath;

    public GameObject[] Mushrooms;
    public float Scale;

    public Material[] materials;
    public GameObject PrefabBase;

    private void Start()
    {
        
    }

    private void CreateMaterials() 
    {
        foreach (Texture texture in textures)
        {
            if (sourceMaterial == null)
            {
                Debug.LogWarning("Source material is not assigned.");
                return;
            }

            // Ensure the save path exists
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
                Debug.Log("Created directory: " + savePath);
            }

            // Duplicate the material
            Material newMaterial = new Material(sourceMaterial);

            // Optionally modify the material properties
            // For example, changing the color or texture
            newMaterial.SetTexture("_BaseMap", texture);

            // Define the full path where the new material will be saved
            string assetPath = Path.Combine(savePath, texture.name + ".mat");

#if UNITY_EDITOR
            // Save the material to the Assets folder
            AssetDatabase.CreateAsset(newMaterial, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

#endif

            Debug.Log("New material created and saved at: " + assetPath);
        }
    }

    public void CreatePrefabs(Material[] materials, GameObject BasePrefab) 
    {
        foreach (Material mat in materials)
        {
            GameObject NewPrefab = Instantiate(BasePrefab, new Vector3(0.0f, -2.37f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            NewPrefab.GetComponentInChildren<Renderer>().sharedMaterial = mat;
            NewPrefab.name = mat.GetTexture("_BaseMap").name;
            ScaleMushroomCorrectly(NewPrefab);
        }
    }

    private void ScaleMushroomsCorrectly(GameObject[] Mushrooms)
    {
        foreach(GameObject Mushroom in Mushrooms) 
        {
            Mushroom.transform.localScale = new Vector3(Mushroom.GetComponentInChildren<Renderer>().material.GetTexture("_BaseMap").width * Scale,
                                                        Mushroom.GetComponentInChildren<Renderer>().material.GetTexture("_BaseMap").height * Scale,
                                                        0.1f);
        }
    }

    private void ScaleMushroomCorrectly(GameObject Mushroom)
    {
            Mushroom.transform.localScale = new Vector3(Mushroom.GetComponentInChildren<Renderer>().material.GetTexture("_BaseMap").width * Scale,
                                                        Mushroom.GetComponentInChildren<Renderer>().material.GetTexture("_BaseMap").height * Scale,
                                                        0.1f);
       
    }
}
