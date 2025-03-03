using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] private float offsetParalax = -0.15f;

    private Camera mainCamera;
    private Vector2 startPosition;
    private Vector2 travel => (Vector2)Camera.main.transform.position - startPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPosition + travel * offsetParalax;
    }
}
