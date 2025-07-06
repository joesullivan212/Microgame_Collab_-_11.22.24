using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TargetZoneDetector : MonoBehaviour
{
    [Header("Target Setup")]
    [SerializeField] private RectTransform targetZone;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private bool showTargetZoneInEditor = true;

    [Header("Detection Settings")]
    [SerializeField] private float detectionPadding = 0f;
    [SerializeField] private bool debugMode = false;

    [Header("Win/Lose Screens")]
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;

    [Header("Hand Animation")]
    [SerializeField] private GameObject handObject;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private string pourAnimationName = "Anim_HandPour";
    [SerializeField] private bool moveHandToCrosshair = true;
    [SerializeField] private Vector2 handPositionOffset = Vector2.zero;
    [SerializeField] private bool useAnimationTrigger = false;
    [SerializeField] private string pourTriggerName = "Pour";
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private float audioVolume = 1f;
    [SerializeField] private AmbientAudioController ambientAudioController;

    [Header("Events")]
    public UnityEvent onHit;
    public UnityEvent onMiss;

    private RectTransform crosshairRect;
    private Image targetZoneImage;
    private bool gameActive = true;
    private MicrogameHandler microgameHandler;

    void Awake()
    {
        microgameHandler = FindObjectOfType<MicrogameHandler>();
        if (loseScreen != null) loseScreen.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);
        
        if (ambientAudioController == null)
        {
            ambientAudioController = FindObjectOfType<AmbientAudioController>();
        }
    }

    void Start()
    {
        if (crosshair != null)
        {
            crosshairRect = crosshair.GetComponent<RectTransform>();
        }

        if (targetZone == null) targetZone = GetComponent<RectTransform>();
        targetZoneImage = targetZone.GetComponent<Image>() ?? targetZone.gameObject.AddComponent<Image>();
        UpdateTargetZoneVisibility();
        targetZoneImage.raycastTarget = false;

        if (loseScreen != null) loseScreen.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);
        
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
            }
        }
    }

    void Update()
    {
        if (crosshair == null || crosshairRect == null) FindCrosshair();
    }

    void LateUpdate()
    {
        if (crosshair == null || crosshairRect == null || !gameActive) return;

        if (Input.GetMouseButtonDown(0)) CheckHit();

#if UNITY_EDITOR
        if (debugMode && Input.GetKey(KeyCode.Space)) VisualizeOverlap();
#endif
    }

    public void OnCrosshairStopped()
    {
        if (gameActive) CheckHit();
    }

    void FindCrosshair()
    {
        if (crosshair == null)
        {
            crosshair = GameObject.Find("Crosshair") ?? GameObject.Find("Crosshair(Clone)");
            if (crosshair != null) crosshairRect = crosshair.GetComponent<RectTransform>();
        }
        else if (crosshairRect == null) crosshairRect = crosshair.GetComponent<RectTransform>();
    }

    void CheckHit()
    {
        if (crosshairRect == null || targetZone == null) return;

        if (moveHandToCrosshair && handObject != null) MoveHandToCrosshair();

        bool isHit = IsOverlapping();

        var crosshairMover = crosshair.GetComponent<CrosshairMover>();
        if (crosshairMover != null) crosshairMover.StopMovement();

        gameActive = false;

        if (isHit) StartCoroutine(HandleWin());
        else StartCoroutine(HandleLose());
    }

    void MoveHandToCrosshair()
    {
        RectTransform handRect = handObject.GetComponent<RectTransform>();
        if (handRect == null) return;

        handRect.position = crosshairRect.position;
        if (handPositionOffset != Vector2.zero) handRect.anchoredPosition += handPositionOffset;
        handObject.SetActive(true);

        if (handAnimator != null)
        {
            if (useAnimationTrigger) handAnimator.SetTrigger(pourTriggerName);
            else StartCoroutine(PlayPourAnimation());
        }
    }

    IEnumerator PlayPourAnimation()
    {
        yield return null;
        handAnimator.Play(pourAnimationName, -1, 0f);
        yield return null;
        AnimatorStateInfo stateInfo = handAnimator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(pourAnimationName)) handAnimator.CrossFade(pourAnimationName, 0.1f);
    }

    bool IsOverlapping()
    {
        Vector3[] targetCorners = new Vector3[4];
        Vector3[] crosshairCorners = new Vector3[4];
        targetZone.GetWorldCorners(targetCorners);
        crosshairRect.GetWorldCorners(crosshairCorners);

        Bounds targetBounds = GetBoundsFromCorners(targetCorners, detectionPadding);
        Bounds crosshairBounds = GetBoundsFromCorners(crosshairCorners, 0);

        return targetBounds.Contains(crosshairBounds.center);
    }

    Bounds GetBoundsFromCorners(Vector3[] corners, float padding)
    {
        float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x) - padding;
        float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x) + padding;
        float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y) - padding;
        float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y) + padding;

        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0.1f);

        return new Bounds(center, size);
    }

    IEnumerator HandleWin()
    {
        crosshair.SetActive(false);
        microgameHandler.CancelTimer();
    
        if (ambientAudioController != null)
        {
            ambientAudioController.StopAllAudio();
        }
    
        if (debugMode) targetZoneImage.color = Color.green;
    
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound, audioVolume);
        }
    
        yield return new WaitForSeconds(1f);
        if (winScreen != null) winScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        microgameHandler.Win();
        onHit?.Invoke();
    }

    IEnumerator HandleLose()
    {
        crosshair.SetActive(false);
        microgameHandler.CancelTimer();
    
        if (ambientAudioController != null)
        {
            ambientAudioController.StopAllAudio();
        }
    
        if (debugMode) targetZoneImage.color = Color.red;
    
        if (audioSource != null && loseSound != null)
        {
            audioSource.PlayOneShot(loseSound, audioVolume);
        }
    
        yield return new WaitForSeconds(1f);
        if (loseScreen != null) loseScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        microgameHandler.Lose();
        onMiss?.Invoke();
    }

    void VisualizeOverlap()
    {
        if (targetZoneImage == null) return;
        targetZoneImage.color = IsOverlapping() ? new Color(0, 1, 0, 0.5f) : new Color(1, 1, 0, 0.3f);
    }

    void OnValidate()
    {
        if (targetZone == null) targetZone = GetComponent<RectTransform>();
        if (targetZone != null)
        {
            var img = targetZone.GetComponent<Image>() ?? targetZone.gameObject.AddComponent<Image>();
            Color c = img.color;
            c.a = showTargetZoneInEditor ? 0.3f : 0f;
            img.color = c;
        }
    }

    public void ResetGame()
    {
        gameActive = true;
        if (loseScreen != null) loseScreen.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);
        var crosshairMover = crosshair.GetComponent<CrosshairMover>();
        if (crosshairMover != null) crosshairMover.StartMovement();
    }

    public void SetCrosshair(GameObject newCrosshair)
    {
        crosshair = newCrosshair;
        crosshairRect = crosshair.GetComponent<RectTransform>();
    }

    public void SetDebugMode(bool enabled)
    {
        debugMode = enabled;
        UpdateTargetZoneVisibility();
    }

    void UpdateTargetZoneVisibility()
    {
        if (targetZoneImage != null)
        {
            Color c = targetZoneImage.color;
            c.a = debugMode ? 0.3f : 0f;
            targetZoneImage.color = c;
        }
    }
    
    public void TriggerLose()
    {
        if (!gameActive) return;
        gameActive = false;
        StartCoroutine(HandleLose());
    }
}