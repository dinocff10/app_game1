using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winbox : MonoBehaviour {

	private void OnTriggerEnter(Collider col)
    {
        if(col.tag=="Player")
        {
            //Victory
            Levelmanage.Instance.victory();
        }
    }
}
