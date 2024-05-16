using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public TextMeshProUGUI basuraText;

    private Rigidbody rb;
    private int latasCollected = 0;
    private int botellasCollected = 0;
    private List<GameObject> latas = new List<GameObject>();
    private List<GameObject> botellas = new List<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        rb.velocity = movement * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedBall"))
        {
            latasCollected++;
            latas.Add(other.gameObject);
            CollectBall(other.gameObject);
        }
        else if (other.CompareTag("BlueBall"))
        {
            botellasCollected++;
            botellas.Add(other.gameObject);
            CollectBall(other.gameObject);
        }

        OnDestroy();
    }

    void CollectBall(GameObject ball)
    {
        Destroy(ball);
    }

    void OnDestroy()
    {
        var allGarbajeCollected = latas.Concat(botellas).ToList();

        basuraText.text = "Latas recogidas: " + latasCollected + "\n" +
                                  "Botellas recogidas: " + botellasCollected + "\n" +
                                  "Total de basura recogida: " + allGarbajeCollected.Count;
    }
}