using UnityEngine;
using System.Collections;

public class animController : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.H)) && Game.pickaxesLeft>0)
        {
            anim.Play("pickaxe");
        }
    }
}