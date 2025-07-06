using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrosshairMover : MonoBehaviour
{
    public enum DifficultyMode
    {
        Easy,
        Medium,
        Hard
    }
    
    [Header("Difficulty Settings")]
    [SerializeField] private DifficultyMode currentDifficulty = DifficultyMode.Easy;
    [SerializeField] private bool randomizeDifficulty = true;
    [SerializeField] private float easyChance = 0.2f; // 20% chance
    [SerializeField] private float mediumChance = 0.4f; // 40% chance
    // Hard chance is automatically 20% (1 - easyChance - mediumChance)
    
    [Header("Visual Feedback")]
    [SerializeField] private bool showDifficultyColor = true;
    [SerializeField] private Color easyColor = new Color(0.4f, 1f, 0.4f, 1f); // Light green
    [SerializeField] private Color mediumColor = new Color(1f, 1f, 0.4f, 1f); // Yellow
    [SerializeField] private Color hardColor = new Color(1f, 0.4f, 0.4f, 1f); // Light red
    
    [Header("Easy Mode Settings")]
    [SerializeField] private float easySpeed = 1.5f;
    [SerializeField] private float easyRange = 250f;
    
    [Header("Medium Mode Settings")]
    [SerializeField] private float mediumBaseSpeed = 2.5f;
    [SerializeField] private float mediumSpeedVariation = 0.5f;
    [SerializeField] private float mediumRange = 350f;
    [SerializeField] private float mediumVerticalRange = 60f;
    
    [Header("Hard Mode Settings")]
    [SerializeField] private float hardBaseSpeed = 4f;
    [SerializeField] private float hardSpeedVariation = 2f;
    [SerializeField] private float hardRange = 400f;
    [SerializeField] private float hardVerticalRange = 150f;
    [SerializeField] private float directionChangeInterval = 2f;
    [SerializeField] private float randomPauseChance = 0.05f; // 5% chance per second
    
    [Header("Smoothing")]
    [SerializeField] private bool useSmoothStop = true;
    [SerializeField] private float stopSmoothTime = 0.3f;
    
    [Header("Audio Settings")]
    [SerializeField] private GameObject handObject;
    [SerializeField] private AudioSource handAudioSource;
    [SerializeField] private float audioDelay = 0f;
    
    private RectTransform rectTransform;
    private bool isMoving = true;
    private float horizontalTime = 0f;
    private float verticalTime = 0f;
    private Vector2 targetPosition;
    private Vector2 velocity = Vector2.zero;
    
    // Hard mode variables
    private float currentHorizontalSpeed;
    private float currentVerticalSpeed;
    private float directionTimer = 0f;
    private int horizontalDirection = 1;
    private int verticalDirection = 1;
    private bool isPaused = false;
    private float pauseTimer = 0f;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
        // Center the crosshair
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        
        // Randomly select difficulty if enabled
        if (randomizeDifficulty)
        {
            SelectRandomDifficulty();
        }
        
        // Initialize speeds for hard mode
        if (currentDifficulty == DifficultyMode.Hard)
        {
            RandomizeHardModeSpeed();
        }
        
        // Get hand audio source if not assigned
        if (handObject != null && handAudioSource == null)
        {
            handAudioSource = handObject.GetComponent<AudioSource>();
        }
        
        // Try to find hand if not assigned
        if (handObject == null)
        {
            handObject = GameObject.Find("Hand");
            if (handObject != null)
            {
                handAudioSource = handObject.GetComponent<AudioSource>();
            }
        }
    }
    
    void SelectRandomDifficulty()
    {
        float randomValue = Random.value; // 0 to 1
        
        if (randomValue < easyChance)
        {
            SetDifficulty(DifficultyMode.Easy);
        }
        else if (randomValue < easyChance + mediumChance)
        {
            SetDifficulty(DifficultyMode.Medium);
        }
        else
        {
            SetDifficulty(DifficultyMode.Hard);
        }
    }
    
    void Update()
    {
        // Check for mouse button press to stop/start movement
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = false;
            PlayHandAudio();
        }
        
        if (isMoving)
        {
            UpdateMovement();
        }
        else if (useSmoothStop)
        {
            // Smooth stop at current position
            rectTransform.anchoredPosition = Vector2.SmoothDamp(
                rectTransform.anchoredPosition,
                targetPosition,
                ref velocity,
                stopSmoothTime
            );
        }
    }
    
    void PlayHandAudio()
    {
        if (handAudioSource != null && handAudioSource.clip != null)
        {
            if (audioDelay > 0)
            {
                StartCoroutine(PlayAudioWithDelay());
            }
            else
            {
                handAudioSource.Play();
            }
        }
    }
    
    IEnumerator PlayAudioWithDelay()
    {
        yield return new WaitForSeconds(audioDelay);
        if (handAudioSource != null && handAudioSource.clip != null)
        {
            handAudioSource.Play();
        }
    }
    
    void UpdateMovement()
    {
        Vector2 newPosition = Vector2.zero;
        
        switch (currentDifficulty)
        {
            case DifficultyMode.Easy:
                // Simple horizontal sine wave movement
                horizontalTime += Time.deltaTime * easySpeed;
                newPosition.x = Mathf.Sin(horizontalTime) * easyRange;
                newPosition.y = 0;
                break;
                
            case DifficultyMode.Medium:
                // Horizontal movement with speed variation and slight vertical movement
                float mediumSpeed = mediumBaseSpeed + Mathf.Sin(Time.time * 0.5f) * mediumSpeedVariation;
                horizontalTime += Time.deltaTime * mediumSpeed;
                verticalTime += Time.deltaTime * mediumSpeed * 0.7f;
                
                newPosition.x = Mathf.Sin(horizontalTime) * mediumRange;
                newPosition.y = Mathf.Sin(verticalTime * 1.3f) * mediumVerticalRange;
                break;
                
            case DifficultyMode.Hard:
                UpdateHardMode(ref newPosition);
                break;
        }
        
        targetPosition = newPosition;
        rectTransform.anchoredPosition = targetPosition;
    }
    
    void UpdateHardMode(ref Vector2 position)
    {
        // Random pauses
        if (!isPaused && Random.value < randomPauseChance * Time.deltaTime)
        {
            isPaused = true;
            pauseTimer = Random.Range(0.2f, 0.8f);
        }
        
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isPaused = false;
                RandomizeHardModeSpeed();
            }
            position = rectTransform.anchoredPosition;
            return;
        }
        
        // Random direction changes
        directionTimer += Time.deltaTime;
        if (directionTimer >= directionChangeInterval)
        {
            directionTimer = 0f;
            directionChangeInterval = Random.Range(1f, 3f);
            
            // Randomly change directions
            if (Random.value < 0.5f) horizontalDirection *= -1;
            if (Random.value < 0.5f) verticalDirection *= -1;
            
            RandomizeHardModeSpeed();
        }
        
        // Update position with current speeds and directions
        horizontalTime += Time.deltaTime * currentHorizontalSpeed * horizontalDirection;
        verticalTime += Time.deltaTime * currentVerticalSpeed * verticalDirection;
        
        // Use combination of sine and cosine for more erratic movement
        position.x = Mathf.Sin(horizontalTime) * hardRange;
        position.y = Mathf.Cos(verticalTime * 0.8f) * hardVerticalRange + 
                     Mathf.Sin(Time.time * 3f) * hardVerticalRange * 0.3f;
    }
    
    void RandomizeHardModeSpeed()
    {
        currentHorizontalSpeed = hardBaseSpeed + Random.Range(-hardSpeedVariation, hardSpeedVariation);
        currentVerticalSpeed = hardBaseSpeed * 0.7f + Random.Range(-hardSpeedVariation, hardSpeedVariation);
    }
    
    public void SetDifficulty(DifficultyMode difficulty)
    {
        currentDifficulty = difficulty;
        
        // Reset movement parameters when changing difficulty
        horizontalTime = 0f;
        verticalTime = 0f;
        directionTimer = 0f;
        isPaused = false;
        
        if (difficulty == DifficultyMode.Hard)
        {
            RandomizeHardModeSpeed();
        }
        
        // Apply visual feedback
        if (showDifficultyColor)
        {
            Image crosshairImage = GetComponent<Image>();
            if (crosshairImage != null)
            {
                switch (difficulty)
                {
                    case DifficultyMode.Easy:
                        crosshairImage.color = easyColor;
                        break;
                    case DifficultyMode.Medium:
                        crosshairImage.color = mediumColor;
                        break;
                    case DifficultyMode.Hard:
                        crosshairImage.color = hardColor;
                        break;
                }
            }
        }
    }
    
    public void StartMovement()
    {
        isMoving = true;
    }
    
    public void StopMovement()
    {
        isMoving = false;
        targetPosition = rectTransform.anchoredPosition;
    }
    
    public void StopMovementWithAudio()
    {
        isMoving = false;
        targetPosition = rectTransform.anchoredPosition;
        PlayHandAudio();
    }
    
    public DifficultyMode GetCurrentDifficulty()
    {
        return currentDifficulty;
    }
    
    // Called in editor when values change
    void OnValidate()
    {
        // Ensure probability values are valid
        easyChance = Mathf.Clamp01(easyChance);
        mediumChance = Mathf.Clamp01(mediumChance);
        
        // Make sure total doesn't exceed 1
        if (easyChance + mediumChance > 1f)
        {
            mediumChance = 1f - easyChance;
        }
    }
}