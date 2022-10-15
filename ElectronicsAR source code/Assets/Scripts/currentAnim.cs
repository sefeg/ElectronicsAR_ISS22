using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class to manage the current animations of an object
public class currentAnim : MonoBehaviour
{
    Animator[] anim;
    bool animationIsOn;
    // Start is called before the first frame update
    void Start()
    {
        // get alle Animators of this object
        anim=(GetComponentsInChildren<Animator>()); 
        Debug.Log("ÁNIMS:" + anim.Length);

            foreach (var animator in anim)
            {
                animator.gameObject.SetActive(analyOptions.currentAnimation);
            }
        animationIsOn = analyOptions.currentAnimation;

    }

    // Update is called once per frame
    void Update()
    {
        if (analyOptions.currentAnimation != animationIsOn)
        {
            foreach (var animator in anim)
            {
                animator.gameObject.SetActive(analyOptions.currentAnimation);
            }
            animationIsOn = analyOptions.currentAnimation;
        }
    }

    public void startAnimForward()
    {
        foreach (var animator in anim)
        {
            animator.SetInteger("Modus", 1);
        }
    }

    public void startAnimBw()
    {
        foreach (var animator in anim)
        {
            animator.SetInteger("Modus", 2);
        }
    }

    public void stopAnim()
    {
        foreach (var animator in anim)
        {
            animator.SetInteger("Modus", 0);
            
        }
    }



}
