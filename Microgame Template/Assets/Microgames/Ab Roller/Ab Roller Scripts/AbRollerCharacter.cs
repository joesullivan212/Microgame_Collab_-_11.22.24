using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Animations;


public class AbRollerCharacter : MonoBehaviour
{
    [Header("Necessary Components")]
    [SerializeField] private MicrogameInputManager playerMovement;
    [SerializeField] private Animator animator;

    [Header("Settings")]
    public float repDuration;

    [Header("Animation Settings")]
    public string repKey;
    public string restKey;

    [Header("Debugging")]
    [SerializeField, ReadOnly(true)] private AbRollerCharacterState state;
    [SerializeField, ReadOnly(true)] private float timer;

    public UnityEvent OnStart;
    public UnityEvent OnRep;
    public UnityEvent OnRest;
    public UnityEvent OnRolling;

    private void Awake()
    {
        state = AbRollerCharacterState.IDLE;
        animator.SetBool(repKey, false);
        OnStart?.Invoke();
    }

    void RolledStateHandler()
    {
        if (playerMovement.MouseMovementNormalized.y <= -.5f)
        {
            state = AbRollerCharacterState.GOING_BACK_REST;
            animator.SetBool(repKey, false);
            OnRolling?.Invoke();
            return;
        }
    }
    void GoingBackToRep()
    {
        if (timer >= 1.00f)
        {
            timer = 0;
            state = AbRollerCharacterState.REST;
            OnRest?.Invoke();
        }
        else
        {
            timer += Time.deltaTime / repDuration;
        }

    }
    void PerformingRep()
    {
        if(timer >= 1.00f)
        {
            timer = 0;
            state = AbRollerCharacterState.ROLLED;
            OnRep?.Invoke();
        }
        else
        {
            timer += Time.deltaTime / repDuration;
        }
    }
    void ResetHandler()
    {
        if(playerMovement.MouseMovementNormalized.y >= .5f)
        {
            state = AbRollerCharacterState.PERFORMING_REP;
            animator.SetBool(repKey, true);
            OnRolling?.Invoke();
            return;
        }
    }


    void StateHandler()
    {
        switch(state) 
        {
            case AbRollerCharacterState.IDLE: break;
            case AbRollerCharacterState.REST: ResetHandler(); break;
            case AbRollerCharacterState.PERFORMING_REP: PerformingRep(); break;
            case AbRollerCharacterState.ROLLED: RolledStateHandler(); break;
            case AbRollerCharacterState.GOING_BACK_REST: GoingBackToRep(); break;

        
        
        }
    }

    private void Update()
    {
        StateHandler();
        //Vector2 direction = playerMovement.MouseMovementNormalized;
        //Debug.Log(direction);
    }

    private void OnEnable()
    {
        state = AbRollerCharacterState.REST;
    }

    private void OnDisable()
    {
        state = AbRollerCharacterState.IDLE;
        animator.SetBool(repKey,false);
    }


    /*
     * The Ab Roller character has 4 states minus the idle state. The idle state is just there to hold the character
     * before the game starts and when the game ends
     */
    public enum AbRollerCharacterState
    {
        IDLE,
        REST,
        PERFORMING_REP,
        ROLLED,
        GOING_BACK_REST
    }

}
