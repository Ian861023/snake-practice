using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Snake2 : MonoBehaviour
{
    public gameaudio gameaudio;
    public UI gameUI;
    Vector3 direct;
    public float speeds;
    public Transform BodyPref;
    public List<Transform> bodies = new List<Transform>();
    void Start()
    {
        Time.timeScale = speeds;
        ResetStage();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direct!=Vector3.down)
        {
            direct = Vector3.up;
            
        }
        if (Input.GetKeyDown(KeyCode.A) && direct != Vector3.right)
        {
            direct = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.S) && direct != Vector3.up)
        {
            direct = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.D) && direct != Vector3.left)
        {
            direct = Vector3.right;
        }
    }
    private void FixedUpdate()
    {for(int i = bodies.Count - 1; i > 0; i--)
        {
            bodies[i].position = bodies[i - 1].position;
        }
        transform.Translate(direct);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {if(collision.CompareTag("food"))
        { bodies.Add(Instantiate(BodyPref,
            transform.position,
            Quaternion.identity));
            gameUI.AddScore();
        }
    
        if (collision.CompareTag("wall"))
        {
            ResetStage();
            gameaudio.Replay();
        }
    }
    void ResetStage()
    {
        transform.position = Vector3.zero;
        direct = Vector3.zero;
        for (int i = 1; i < bodies.Count; i++)
        {
            Destroy(bodies[i].gameObject);
        }
        bodies.Clear();
        bodies.Add(transform);
        gameUI.ResetScore();
    }
}

