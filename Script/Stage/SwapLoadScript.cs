using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLoadScript : MonoBehaviour
{

    [SerializeField, TooltipAttribute("à⁄ìÆêÊ")] GameObject loadA;
    [SerializeField, TooltipAttribute("à⁄ìÆêÊ")] GameObject loadB;
    [SerializeField, TooltipAttribute("å≥ÇÃÉÇÉfÉã")] GameObject baseloadA;
    [SerializeField, TooltipAttribute("å≥ÇÃÉÇÉfÉã")] GameObject baseloadB;

    // Start is called before the first frame update
    void Start()
    {

        if (!baseloadA.activeSelf)
        {
            loadA.SetActive(true);
            loadB.SetActive(true);
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

        loadA.SetActive(false);
        loadB.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
