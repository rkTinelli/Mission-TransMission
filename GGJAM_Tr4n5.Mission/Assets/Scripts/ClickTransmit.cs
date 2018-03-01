using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTransmit : MonoBehaviour {

    public Sprite Transmission;
    public Sprite Abort;
    
    public Move move;


    void OnMouseDown()
    {
        move = GameObject.Find("Aranha(Clone)").GetComponent<Move>();
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite == Transmission)
        {
            //Debug.Log("TRANSMISSION");
            move.transmitClicked();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Abort;

        } else if (this.gameObject.GetComponent<SpriteRenderer>().sprite == Abort)
        {
            //Debug.Log("ABORT");
            move.abortExec();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Transmission;
        }

    }
}
