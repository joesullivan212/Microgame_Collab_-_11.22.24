using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using static UnityEngine.ParticleSystem;

public class ColorPotionPlayerController : MonoBehaviour
{
    public Camera camera;
    public MicrogameInputManager MicrogameInputManager;
    public MicrogameHandler microgameHandler;
    public ColorPotion currentPouringColorPotion;
    public GameObject mainPotion;
    public GameObject mainPotionLiquid;
    public TextMeshPro PercentageClosenessText;

    public GameObject[] Corks;
    public ParticleSystem[] particleSystems;
    public float LiquidFlowRate = 5.0f;

    public float Intensity;
    public float WhiteIntensity;
    public float BlackIntensity;
    public float FillingIntensity;

    public Color PlayersPotionColor;

    public float TopColorOffset;

    public float Fullness = 0.15f;

    public Color TargetColor;
    public Color[] PotentialTargetColors;
    public Material TargetColorMaterial;
    public int TargetColorIndex;

    public float DistanceFromTargetColor;
    public float DistanceFromTargetColorPercentage;

    public float DistanceAllowed;

    public bool HasWon = false;

    public float PlayerPotionEmitionLevel;
    public Renderer PlayerPotionRenderer;

    public float ColorMatchLerpSpeed = 0.4f;

    public MMF_Player WinFeedback;

    // Start is called before the first frame update
    void Start()
    {
        SetTargetColor();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPotionRenderer.material.SetFloat("_Emission", PlayerPotionEmitionLevel);

        CheckForWin();

        if (HasWon) 
        {
            LerpColors();
            return;
        }

        DistanceFromTargetColor = DetermineDiffrenceBetweenColors(PlayersPotionColor, TargetColor);

        DistanceFromTargetColorPercentage = ExpressDistanceAsAPercentage(DistanceFromTargetColor);

        PercentageClosenessText.text = "%" + Mathf.RoundToInt(DistanceFromTargetColorPercentage).ToString();

        Reset();

        if (MicrogameInputManager.MouseBeingHeld)
        {

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(MicrogameInputManager.MouseScreenPosition);

            if (Physics.Raycast(ray, out hit))
            {
                hit.collider.gameObject.TryGetComponent<ColorPotion>(out currentPouringColorPotion);
            }
        }

        if (currentPouringColorPotion != null)
        {
            currentPouringColorPotion.Cork.SetActive(false);

            if (currentPouringColorPotion.r)
            {
                Color newColor = new Color(PlayersPotionColor.r + (Time.deltaTime * Intensity),
                                           PlayersPotionColor.g,
                                           PlayersPotionColor.b,
                                           1.0f);

                PlayersPotionColor = newColor;

                Fullness += Time.deltaTime * FillingIntensity;
            }
            if (currentPouringColorPotion.g)
            {
                Color newColor = new Color(PlayersPotionColor.r,
                                           PlayersPotionColor.g + (Time.deltaTime * Intensity),
                                           PlayersPotionColor.b,
                                           1.0f);

                PlayersPotionColor = newColor;

                Fullness += Time.deltaTime * FillingIntensity;
            }
            if (currentPouringColorPotion.b)
            {
                Color newColor = new Color(PlayersPotionColor.r,
                                           PlayersPotionColor.g,
                                           PlayersPotionColor.b + (Time.deltaTime * Intensity),
                                           1.0f);

                PlayersPotionColor = newColor;

                Fullness += Time.deltaTime * FillingIntensity;
            }
            if (currentPouringColorPotion.white)
            {
                Color newColor = new Color(PlayersPotionColor.r + (Time.deltaTime * WhiteIntensity),
                                           PlayersPotionColor.g + (Time.deltaTime * WhiteIntensity),
                                           PlayersPotionColor.b + (Time.deltaTime * WhiteIntensity),
                                           1.0f);

                PlayersPotionColor = ClampColor(newColor);

                Fullness += Time.deltaTime * FillingIntensity;
            }
            if (currentPouringColorPotion.black)
            {
                Color newColor = new Color(PlayersPotionColor.r - (Time.deltaTime * BlackIntensity),
                                           PlayersPotionColor.g - (Time.deltaTime * BlackIntensity),
                                           PlayersPotionColor.b - (Time.deltaTime * BlackIntensity),
                                           1.0f);

                PlayersPotionColor = ClampColor(newColor);

                Fullness += Time.deltaTime * FillingIntensity;
            }
        }
        SetColor(PlayersPotionColor);

        //Flow
        if (currentPouringColorPotion != null)
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                if (currentPouringColorPotion.FlowParticleSystem == particleSystem)
                {
                    var emission = currentPouringColorPotion.FlowParticleSystem.emission;
                    emission.rateOverTime = LiquidFlowRate;
                }
                else
                {
                    var emission = particleSystem.emission;
                    emission.rateOverTime = 0;
                }
            }
        }
        else
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                var emission = particleSystem.emission;
                emission.rateOverTime = 0;
            }
        }
    }

    void LerpColors() 
    {

        PlayersPotionColor = new Color(Mathf.Lerp(PlayersPotionColor.r, TargetColor.r, ColorMatchLerpSpeed * Time.deltaTime),
                                       Mathf.Lerp(PlayersPotionColor.g, TargetColor.g, ColorMatchLerpSpeed * Time.deltaTime),
                                       Mathf.Lerp(PlayersPotionColor.b, TargetColor.b, ColorMatchLerpSpeed * Time.deltaTime),
                                       1.0f);
        SetColor(PlayersPotionColor);

    }

    void CheckForWin() 
    { 
       if(HasWon == false) 
       { 
          if(DistanceFromTargetColorPercentage >= 100.0f) 
          {
                HasWon = true;
                microgameHandler.WinWhenTimeIsUp();
                microgameHandler.CancelTimer();
                StartCoroutine(WinSequence());
          } 
       }
    }

    void SetTargetColor() 
    { 
       TargetColorIndex = Random.Range(0, PotentialTargetColors.Length);

        Color SelectedColor = PotentialTargetColors[TargetColorIndex];

        SetTargetColorMaterial(SelectedColor);

        TargetColor = SelectedColor;
    }

    void SetTargetColorMaterial(Color color) 
    { 
        TargetColorMaterial.color = color;
    }

    float DetermineDiffrenceBetweenColors(Color Color1, Color Color2) 
    {
        float R = DistanceBetweenTwoNumbers(Color1.r, Color2.r);
        float G = DistanceBetweenTwoNumbers(Color1.g, Color2.g);
        float B = DistanceBetweenTwoNumbers(Color1.b, Color2.b);

        //Vector3 TempDistanceBetweenColorsVec3 = new Vector3(R, G, B);

        Vector3 DistanceBetweenColorsVec3 = new Vector3(R, G, B);

        return DistanceBetweenColorsVec3.magnitude;

    }

    public float DistanceBetweenTwoNumbers(float Num1, float Num2)
    {
        float NewResult1;
        float NewResult2;

        NewResult1 = Num1 - Num2;
        NewResult2 = Num2 - Num1;

        if (NewResult1 <= NewResult2)
        {
            return NewResult1;
        }
        else
        {
            return NewResult2;
        }
    }

    float ExpressDistanceAsAPercentage(float distance)
    {
        float DistanceAsAPercent = (100.0f + DistanceAllowed) - (distance * 100.0f);

        if (DistanceAsAPercent > 100.0f)
        {
            DistanceAsAPercent = 100.0f;
        }
        if (DistanceAsAPercent < 0.0f)
        {
            DistanceAsAPercent = 0.0f;
        }

        Mathf.Round(DistanceAsAPercent);

        return DistanceAsAPercent;
    }

   

    private void Reset()
    {
        currentPouringColorPotion = null;

        foreach (GameObject Cork in Corks)
        {
            Cork.SetActive(true);
        }
    }

    private void SetColor(Color color) 
    { 
        mainPotionLiquid.GetComponent<Renderer>().material.SetColor("_Color", color);

        Color TopColor = new Color(color.r + TopColorOffset, 
                                   color.g + TopColorOffset, 
                                   color.b + TopColorOffset, 
                                   1.0f);

        mainPotionLiquid.GetComponent<Renderer>().material.SetColor("_Top_color", TopColor);

        mainPotionLiquid.GetComponent<Renderer>().material.SetFloat("_Fullness", Fullness);

    }

    public Color ClampColor(Color color) 
    {

        Color ClampedColor = new Color(Mathf.Clamp(color.r, 0.0f, 1.0f),
                                       Mathf.Clamp(color.g, 0.0f, 1.0f),
                                       Mathf.Clamp(color.b, 0.0f, 1.0f),
                                       1.0f);

        return ClampedColor;
    }

    IEnumerator WinSequence() 
    {
        Reset();
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var emission = particleSystem.emission;
            emission.rateOverTime = 0;
        }

        WinFeedback.PlayFeedbacks();

        yield return new WaitForSeconds(2.0f);

        microgameHandler.Win();
    }
}
