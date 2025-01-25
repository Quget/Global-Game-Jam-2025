using System;
using System.Security.Cryptography.X509Certificates;
using Unity.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    [SerializeField]
    private float bubbleForce = 25;

    [SerializeField]
    private Renderer bubbleRenderer = null;

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

    private Action onLifeEnd;

    private float bubbleStopTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }

    void Start()
    {
        rigidbody.AddForce(bubbleDirection * bubbleForce, ForceMode.Impulse);
        bubbleStopTimer = bubbleLife * 0.1f;

	}

    // Update is called once per frame
    void Update()
    {
        bubbleLife -= Time.deltaTime;
		bubbleStopTimer -= Time.deltaTime;

		if (bubbleLife <= 0) {
            onLifeEnd?.Invoke();
			Destroy(gameObject);
        }

        if(bubbleStopTimer <= 0)
        {
            rigidbody.linearVelocity = Vector3.zero;
        }
    }

	public void Entrap(Transform theEntrapped, Action onLifeEnd)
    {
        this.onLifeEnd = onLifeEnd;
        BubbleLife = GameManager.Instance.gameData.BubbleTimer;
        bubbleRenderer.gameObject.SetActive(false);
		//Destroy(this.gameObject);
		/*
        rigidbody.isKinematic = true;
		transform.parent = theEntrapped;
        transform.localPosition = Vector3.zero;*/
	}

	private void OnDestroy()
	{
        onLifeEnd = null;
	}
}
