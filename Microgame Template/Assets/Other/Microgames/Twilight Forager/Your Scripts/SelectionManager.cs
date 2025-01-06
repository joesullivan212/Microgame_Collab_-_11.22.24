using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;
    [SerializeField] string selectableTag = "Selectable";
    [SerializeField] MicrogameInputManager microgameInputManager;

    GameInitializer gameInitializer;
    MicrogameHandler microgameHandler;

    Material defaultMaterial;
    GameObject _selection;

    void Awake()
    {
        gameInitializer = FindObjectOfType<GameInitializer>();
        microgameHandler = FindObjectOfType<MicrogameHandler>();
    }

    void Update()
    {
        SelectMushroom();
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            List<Material> matList = new List<Material> { defaultMaterial }; 
            selectionRenderer.SetMaterials(matList);
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.collider.gameObject;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
                defaultMaterial = _selection.GetComponent<Renderer>().material;
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    List<Material> matList = new List<Material> { defaultMaterial, highlightMaterial};
                    selectionRenderer.SetMaterials(matList);
                }


            }
        }
        
    }

    void SelectMushroom()
    {
        if (microgameInputManager.Clicked && _selection != null)
        {
            if (_selection == gameInitializer.GetWinningMushroom())
            {
                microgameHandler.Win();
            }
            else
            {
                microgameHandler.Lose();
            }
        }
       
    }

}
