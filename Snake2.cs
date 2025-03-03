using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using MySql.Data.MySqlClient;
using TMPro;
using UnityEngine.UI;
public class Snake2 : MonoBehaviour
{   //public MySQLTest mySqLTEST;
    public gameaudio gameaudio;
    public UI gameUI;
    public name inputsql;
    public TMP_InputField nameInputField;
    public TMP_Text scoreText;
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
            sqlinput(nameInputField.text, scoreText.text);
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
    void sqlinput(string name, string score)
    {
        string server = "localhost";
        string database = "sql_tutorial";
        string user = "root";
        string password = "@9861023";
        string connString = "Server=" + server + ";Database=" + database + ";User ID=" + user + ";Password=" + password + ";SslMode=None;";


        using (MySqlConnection conn = new MySqlConnection(connString))
        {
            conn.Open();
            Debug.Log("✅ 成功連接 MySQL！");

            string query = "INSERT INTO snakescore (snakename, score) VALUES (@snakename, @score)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@snakename", name);  // 玩家名稱
                cmd.Parameters.AddWithValue("@score", score);  // 玩家得分

                // 執行查詢
                cmd.ExecuteNonQuery();
            }
        }


    }
}

