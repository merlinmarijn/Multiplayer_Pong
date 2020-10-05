using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public float speed;
    private Rigidbody rb;
    public float HeightClamp = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Translate(0, 1, 0);
        }
        float moveVertical = Input.GetAxis("Vertical");
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (moveVertical * speed));
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Mathf.Clamp(this.transform.position.z, -HeightClamp, HeightClamp));
    }

    public void setSpawn(Vector3 v3)
    {
        this.transform.position = v3;
        if (v3.x == 16)
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
