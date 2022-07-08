using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSensitivePlate : MonoBehaviour
{
    bool hitFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        hitFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        hitFlag = false;
    }

    //降れている間呼ばれている処理
    private void OnTriggerStay(Collider other)
    {
        //ぷれいやーまたはロボットだった場合
        if (other.gameObject.name == "Player" || 
            other.gameObject.name == "Robot")
        {
            hitFlag = true;
        }
    }
    public bool GetHitFlag()
    {
        return hitFlag;
    }
}
