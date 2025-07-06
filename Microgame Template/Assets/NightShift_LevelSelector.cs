using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightShift_LevelSelector : MonoBehaviour
{
    public int AreaSelectedIndex;

    [Header("Refrences")]
    public LayerMask layerMask;
    public NightShift_LevelPouch SelectedPouch;
    public NightShift_LevelPouch[] AllPouches;
    public Animator LevelPanelAnim;
    public NightShift_LevelDisplay levelDisplay;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastFromMouse();
        }
    }

    void RaycastFromMouse()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found! Make sure your camera has the 'MainCamera' tag.");
            return;
        }

        // Create a ray from the mouse cursor position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.gameObject.CompareTag("Win")) 
            {
                SelectLevelPuch(hit.collider.gameObject.GetComponent<NightShift_LevelPouch>());
            }

        }

        // Draw debug ray (visible in Scene View)
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 1.0f);
    }

    public void SelectLevelPuch(NightShift_LevelPouch LevelPouch) 
    {
        foreach (NightShift_LevelPouch level in AllPouches)
        {
            level.IsSelected = false;
        }

        LevelPouch.IsSelected = true;

        LevelPanelAnim.Play("LevelSelectCanvasFadeIn");

        AreaSelectedIndex = LevelPouch.LevelIndex;

        levelDisplay.DisplayLevel(AreaSelectedIndex);

    }

    public void DeselectAllLevles() 
    {
        foreach (NightShift_LevelPouch level in AllPouches)
        {
            level.IsSelected = false;
        }

        LevelPanelAnim.Play("LevelSelectCanvasFadeOut");
    }
}
