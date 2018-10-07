using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VanillaPlayer : MonoBehaviour {

    public Animator anim;

    public void GoCrazy() {
        anim.SetTrigger("Crazy");
    }

    public void GoodEnd() {
        anim.SetTrigger("GoodEnd");

    }

    public void BadEnd() {
        anim.SetTrigger("BadEnd");

    }

    public void Walk() {
        anim.SetBool("isWalking", true);

    }

    public void DontWalk() {
        anim.SetBool("isWalking", false);
    }

}
