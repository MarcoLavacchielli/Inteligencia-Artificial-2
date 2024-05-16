using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public TextMeshProUGUI collectedBallsText;

    private Rigidbody rb;
    private int redBallsCollected = 0;
    private int blueBallsCollected = 0;
    private List<GameObject> redBalls = new List<GameObject>();
    private List<GameObject> blueBalls = new List<GameObject>();

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
            redBallsCollected++;
            redBalls.Add(other.gameObject);
            CollectBall(other.gameObject);
        }
        else if (other.CompareTag("BlueBall"))
        {
            blueBallsCollected++;
            blueBalls.Add(other.gameObject);
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
        var allBallsCollected = redBalls.Concat(blueBalls).ToList();

        collectedBallsText.text = "Latas recogidas: " + redBallsCollected + "\n" +
                                  "Botellas recogidas: " + blueBallsCollected + "\n" +
                                  "Total de basura recogida: " + allBallsCollected.Count;
    }
}