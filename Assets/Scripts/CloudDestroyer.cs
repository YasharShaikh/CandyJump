using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyer : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Cloud")
        {
            Destroy(coll.gameObject);
        }
    }
}
