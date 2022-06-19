using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterMove : MonoBehaviour
{
    float up = 0.1f;
    Vector3 direction = new Vector3(0.5f, 5f, 2f);
    Vector3 direction2 = new Vector3(0.5f, 0.5f, 2f);
    float speed = 1.0f;

    // é´èëå^ÇÃïœêîÇégÇ¡ÇƒÇ‹Ç∑ÅB
    Dictionary<string, bool> move = new Dictionary<string, bool>
    {
        {"up", false },
        {"down", false },
    };

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move["up"] = Input.GetKey(KeyCode.UpArrow);
        move["down"] = Input.GetKey(KeyCode.DownArrow);

    }

    void FixedUpdate()
    {
        if (move["up"])
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, direction, step);
        }
        if (move["down"])
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, direction2, step);
        }
    }
}
