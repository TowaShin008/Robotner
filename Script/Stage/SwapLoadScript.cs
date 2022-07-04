using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLoadScript : MonoBehaviour
{

    [SerializeField, TooltipAttribute("移動先")] GameObject loadA;
    [SerializeField, TooltipAttribute("移動先")] GameObject loadB;
    [SerializeField, TooltipAttribute("元のモデル")] GameObject baseloadA;
    [SerializeField, TooltipAttribute("元のモデル")] GameObject baseloadB;

    // Start is called before the first frame update
    void Start()
    {

        if (!baseloadA.activeSelf)
        {
            baseloadA.SetActive(true);
            baseloadB.SetActive(true);
        }
        GameObject parent = GameObject.Find("LoadParent");
        GameObject mainLoadA = Instantiate(baseloadA);
        GameObject mainLoadB = Instantiate(baseloadB);
        mainLoadA.transform.parent = parent.transform;
        mainLoadB.transform.parent = parent.transform;


        if (Random.Range(0, 2) == 0)
        {
            mainLoadA.transform.position = loadA.transform.position;
            mainLoadB.transform.position = loadB.transform.position;
            mainLoadA.transform.rotation = loadA.transform.rotation;
            mainLoadB.transform.rotation = loadB.transform.rotation;
            mainLoadA.transform.localScale = loadA.transform.localScale;
            mainLoadB.transform.localScale = loadB.transform.localScale;
        }   
        else
        {
            mainLoadA.transform.position = loadB.transform.position;
            mainLoadB.transform.position = loadA.transform.position;
            mainLoadA.transform.rotation = loadB.transform.rotation;
            mainLoadB.transform.rotation = loadA.transform.rotation;
            mainLoadA.transform.localScale = loadB.transform.localScale;
            mainLoadB.transform.localScale = loadA.transform.localScale;
        }

        baseloadA.SetActive(false);
        baseloadB.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
