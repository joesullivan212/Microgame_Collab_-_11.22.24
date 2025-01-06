using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [System.Serializable]
    public class SpritePair
    {
        public GameObject mushroomMesh;
        public Sprite mushroomSprite;

        public string mushroomType;
    }

    [SerializeField] List<SpritePair> spritePairs = new List<SpritePair>();
    [SerializeField] GameObject treeMushroomSpawns;
    [SerializeField] GameObject groundMushroomSpawns;
    [SerializeField] GameObject pictureLocation;
    GameObject winningMushroom;
    List<(GameObject mushroomMesh, Sprite mushroomSprite, string mushroomType)> meshToSpriteMap;

    List<GameObject> treeMushrooms = new List<GameObject>();
    List<GameObject> groundMushrooms = new List<GameObject>();

    void Awake()
    {
        BuildMushroomList();
        
    }

    void Start()
    {
        SelectWinningMushroom();
        SpawnOtherMushrooms();
    }

    public GameObject GetWinningMushroom()
    {
        return winningMushroom;
    }

    List<(GameObject, Sprite, string)> BuildMushroomList()
    {
        meshToSpriteMap = new List<(GameObject, Sprite, string)>();
        foreach (var pair in spritePairs)
        {
            meshToSpriteMap.Add((pair.mushroomMesh, pair.mushroomSprite, pair.mushroomType));
        }
        return meshToSpriteMap;
    }


    void SelectWinningMushroom()
    {
        int randomIndex = Random.Range(0,meshToSpriteMap.Count);
        (GameObject mesh, Sprite sprite, string type) randomMushroom = meshToSpriteMap[randomIndex];
        if (randomMushroom.type == "Tree")
        {
            SpawnWinningMushroom(treeMushroomSpawns, randomMushroom.mesh);
        }
        if (randomMushroom.type == "Ground")
        {
            SpawnWinningMushroom(groundMushroomSpawns, randomMushroom.mesh);
        }
        AssignMushroomPicture(randomMushroom.sprite);
        meshToSpriteMap.RemoveAt(randomIndex);

        foreach (var mushroomTuple in meshToSpriteMap)
        {
            if (mushroomTuple.mushroomType == "Tree")
            {
                treeMushrooms.Add(mushroomTuple.mushroomMesh);
            }
            else
            {
                groundMushrooms.Add(mushroomTuple.mushroomMesh);
            }
        }
    }

    void SpawnWinningMushroom(GameObject spawnPoints, GameObject randomMushroomMesh)
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.transform.childCount);
        Transform randomSpawn = spawnPoints.transform.GetChild(randomSpawnIndex);
        winningMushroom = Instantiate(randomMushroomMesh, randomSpawn);
        randomSpawn.GetComponent<UsedLocationFlag>().isUsed = true;

    }

    void SpawnOtherMushrooms()
    {
        foreach (Transform child in treeMushroomSpawns.transform)
        {
            if (child.GetComponent<UsedLocationFlag>().isUsed == false)
            {
                int randomMushroomIndex = Random.Range(0, treeMushrooms.Count);
                GameObject randomMushroom = treeMushrooms[randomMushroomIndex];
                Instantiate(randomMushroom, child.transform);
            }
        }
        
        foreach (Transform child in groundMushroomSpawns.transform)
        {
            if (child.GetComponent<UsedLocationFlag>().isUsed == false)
            {
                int randomMushroomIndex = Random.Range(0, groundMushrooms.Count);
                GameObject randomMushroom = groundMushrooms[randomMushroomIndex];
                Instantiate(randomMushroom, child.transform);
            }
        }
    }

    void AssignMushroomPicture(Sprite winningMushroomSprite)
    {
        pictureLocation.GetComponent<SpriteRenderer>().sprite = winningMushroomSprite;
    }

}
