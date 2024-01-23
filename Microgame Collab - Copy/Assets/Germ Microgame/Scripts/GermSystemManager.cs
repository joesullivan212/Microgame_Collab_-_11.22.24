using KillTheGerms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GermSystemManager : MonoBehaviour
{
    public List<ParticleSystem> activeParticleSystems;
    public UnityEvent OnGermsCleared;

    private void Awake()
    {
        activeParticleSystems = new List<ParticleSystem>();
    }

    private void AddParticleSystem(ParticleSystem ps)
    {
        if (activeParticleSystems.Contains(ps)) 
            return;

        activeParticleSystems.Add(ps);
    }

    private void RemoveParticleSystem(ParticleSystem ps)
    {
        if (activeParticleSystems.Contains(ps))
        {
            activeParticleSystems.Remove(ps);
            if (activeParticleSystems.Count <= 0)
            {
                Debug.Log("WIN!");
                OnGermsCleared?.Invoke();
            }
        }
    }
    private void OnEnable()
    {
        GermFace.OnPlayParticleSystem += AddParticleSystem;
        GermFace.OnIsDead += RemoveParticleSystem;
    }

    private void OnDisable()
    {
        GermFace.OnPlayParticleSystem -= AddParticleSystem;
        GermFace.OnIsDead -= RemoveParticleSystem;
    }
}
