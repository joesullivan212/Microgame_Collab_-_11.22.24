using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    [Header("Movement")]

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _deadZone = 0.5f;
    [SerializeField] private float _turnSpeed = 5f;

    [SerializeField] private float _limit = 10;

    [Header("Grow")]
    [SerializeField] private GameObject[] _bodyPrefabs;

    private List<GameObject> _bodyParts = new List<GameObject>();
    private List<Vector3> _positionHistory = new List<Vector3>();

    [SerializeField] private int _gap = 10;
    [SerializeField] private float _bodySpeed = 5f;

    private int _appleEaten = 0;

    [Header("Audio")]
    public AudioSource _audioSource;
    public AudioClip _clip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { GrowSnake(); }

        MapLimit();

        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        Vector3 toMouse = mouseWorldPosition - transform.position;
        toMouse.y = 0;

        if (toMouse.magnitude > _deadZone)
        {
            Vector3 targetDirection = toMouse.normalized;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * _moveSpeed * Time.deltaTime;

        _positionHistory.Insert(0, transform.position);

        BodyPartMovement();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenMousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenMousePos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }

        return transform.position;
    }

    private void MapLimit()
    {

        float limitsx = Mathf.Clamp(transform.position.x, -_limit, _limit);
        float limitsz = Mathf.Clamp(transform.position.z, -_limit, _limit);

        transform.position = new Vector3(limitsx, transform.position.y, limitsz);

    }
    public void GrowSnake()
    {
        _audioSource.clip = _clip;
        _audioSource.pitch = Random.Range(0.75f, 1f);
        _audioSource.Play();

        int randomParts = Random.Range(0, _bodyPrefabs.Length);

        GameObject body = Instantiate(_bodyPrefabs[randomParts]);
        _bodyParts.Add(body);

        _appleEaten++;
       
    }

    private void BodyPartMovement()
    {
        int index = 0;

        foreach (var body in _bodyParts)
        {

            Vector3 point = _positionHistory[Mathf.Min(index * _gap, _positionHistory.Count - 1)];

            Vector3 moveDir = point - body.transform.position;
            body.transform.position += moveDir * _bodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }

    public int AppleEaten()
    {
        return _appleEaten;
    }
}
