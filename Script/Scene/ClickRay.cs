using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRay : MonoBehaviour
{

    public string endTag = "end";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag(endTag))
                {
                    hit.collider.gameObject.GetComponent<SampleClick>().OnClickFade();
                }
            }
        }

    }
}
