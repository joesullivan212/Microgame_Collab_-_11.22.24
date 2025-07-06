using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float displayDuration = 1f;
    [SerializeField] private bool fadeOut = false;
    [SerializeField] private float fadeDuration = 0.5f;
    
    [Header("Crosshair")]
    [SerializeField] private GameObject crosshairPrefab;
    [SerializeField] private bool spawnCrosshairOnDisappear = true;
    
    private CanvasGroup canvasGroup;
    private float timer = 0f;
    private bool isFading = false;
    
    void Start()
    {
        // If fade is enabled, try to get or add a CanvasGroup component
        if (fadeOut)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (!isFading && timer >= displayDuration)
        {
            if (fadeOut && canvasGroup != null)
            {
                isFading = true;
            }
            else
            {
                // Destroy immediately if not fading
                Destroy(gameObject);
            }
        }
        
        // Handle fade out
        if (isFading)
        {
            float fadeProgress = (timer - displayDuration) / fadeDuration;
            canvasGroup.alpha = 1f - fadeProgress;
            
            if (fadeProgress >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }
    
    void OnDestroy()
    {
        // Spawn crosshair when this start screen is destroyed
        if (spawnCrosshairOnDisappear && crosshairPrefab != null)
        {
            SpawnCrosshair();
        }
    }
    
    void SpawnCrosshair()
    {
        // Try to find TimerCanvas first (based on your hierarchy)
        Canvas targetCanvas = null;
        GameObject timerCanvas = GameObject.Find("TimerCanvas");
        
        if (timerCanvas != null)
        {
            targetCanvas = timerCanvas.GetComponent<Canvas>();
        }
        
        // If TimerCanvas not found, use the parent canvas of this StartScreen
        if (targetCanvas == null)
        {
            targetCanvas = GetComponentInParent<Canvas>();
        }
        
        // If still no canvas found, find any canvas in the scene
        if (targetCanvas == null)
        {
            targetCanvas = FindObjectOfType<Canvas>();
        }
        
        // Spawn the crosshair
        if (targetCanvas != null)
        {
            GameObject crosshair = Instantiate(crosshairPrefab, targetCanvas.transform);
            
            // Ensure it's centered
            RectTransform rt = crosshair.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
            }
            
            Debug.Log($"Crosshair spawned in {targetCanvas.name}");
            
            // Find and notify the TargetZoneDetector
            TargetZoneDetector detector = FindObjectOfType<TargetZoneDetector>();
            if (detector != null)
            {
                detector.SetCrosshair(crosshair);
                Debug.Log("Crosshair reference sent to TargetZoneDetector");
            }
        }
        else
        {
            Debug.LogWarning("No canvas found to spawn crosshair!");
        }
    }
}