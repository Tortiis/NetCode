using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using System.Data;

public class PlayerControler : NetworkBehaviour
{
    float speed = 4;

    [SerializeField]
    private Transform rocksp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;

        Vector3 mover = Vector3.zero;
        if(Input.GetKey(KeyCode.A))
        {
            mover.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mover.z -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mover.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            mover.z = 1;
        }

        transform.position += mover * speed * Time.deltaTime;
    }

    public override void OnNetworkSpawn()
    {
        StartCoroutine(llueve());
    }

    IEnumerator llueve()
    {
        while (true)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(0, 10), 10, (UnityEngine.Random.Range(0, 10)));
            Transform rockaI = Instantiate(rocksp,pos,Quaternion.identity);

            rocksp.GetComponent<NetworkObject>().Spawn(true);
            Destroy(rockaI.gameObject, 3);
            yield return new WaitForSeconds(2);
        }
    }
}
