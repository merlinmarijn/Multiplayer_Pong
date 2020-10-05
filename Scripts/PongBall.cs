using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PongBall : NetworkBehaviour
{
    float Speed = 5f;
    Vector3 Velocity = Vector3.forward.normalized;
    [HideInInspector]
    public CustomGameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        float sx = Random.Range(0, 2) == 0 ? -1 : 1;
        float sz = Random.Range(0, 2) == 0 ? -1 : 1;
        GetComponent<Rigidbody>().velocity = new Vector3(sx*Speed, 0,sz*Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.transform.tag == "Goal")
            {
            if (isServer)
            {
                GM.CmdGoal(other.GetComponent<Goal>().GetID());
            }
            }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.Scale(GetComponent<Rigidbody>().velocity, new Vector3(1.0004f, 0, 1.0004f));
    }
}
