using System.Security.Cryptography.X509Certificates;
using Unity.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    [SerializeField]
    private float bubbleForce = 25;
    private Rigidbody rigidbody;
    private Vector3 bubbleDirection;
    public Vector3 BubbleDirection
    {
        get {return bubbleDirection; }
        set {bubbleDirection = value; }
    }

    //[SerializeField]
    //private float bubbleSpeed;
    // public float BubbleSpeed
    // {
    //     get {return bubbleSpeed; }
    //     set {bubbleSpeed = value; }
    // }
    private float bubbleLife;
    public float BubbleLife
    {
        get {return bubbleLife; }
        set {bubbleLife = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigidbody.AddForce(bubbleDirection * bubbleForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        bubbleLife -= Time.deltaTime;
        if (bubbleLife <= 0) {
            Destroy(gameObject);
        }
    }
}
