using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    void Update()
    {
        if (GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().enabled == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3) && hit.transform.name == "RndPickaxe")
            {
                Game.pickaxesLeft++;
                GameObject.Find("RndPickaxe").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("RndPickaxe").GetComponent<Transform>().position = new Vector3(100f, 100f, 100f);
            }
        }
    }
}
