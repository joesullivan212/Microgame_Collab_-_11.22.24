using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _apple;
    [SerializeField] private float _limit = 3.25f;
    [SerializeField] private int _spawnedApple = 5;

    [Header("Win")]
    private MicrogameHandler _handler;
    private SnakeController _snake;
    private bool _hasWon = false;

    [SerializeField] private GameObject _toShow;

    [Header("Audio")]
    public AudioSource _audioSource;
    public AudioClip _clip;


    private void Start()
    {

        _handler = FindFirstObjectByType<MicrogameHandler>();
        _snake = FindFirstObjectByType<SnakeController>();

        for (int i = 0; i < _spawnedApple; i++)
        {
            Spawn();
        }
    }

    private void Update()
    {
        End();
    }

    private void Spawn()
    {
        float PosX = Random.Range(-_limit, _limit);
        float PosZ = Random.Range(-_limit, _limit);

        Vector3 spawnPosition = new Vector3(PosX, gameObject.transform.position.y, PosZ);

        Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        Instantiate(_apple, spawnPosition, randomRotation, gameObject.transform);
    }

    private void End()
    {
        if (_spawnedApple == _snake.AppleEaten())
        {
            if (!_hasWon)
            {
                StartCoroutine(WinScreen());
            }
        }
    }

    IEnumerator WinScreen()
    {
        _handler.PauseTimer();
        _audioSource.clip = _clip;
        _audioSource.pitch = 1f;
        _audioSource.Play();
        _toShow.SetActive(true);
        _hasWon = true;
        yield return new WaitForSeconds(2f);
        _handler.Win();
    }

}
