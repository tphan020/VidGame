using UnityEngine;
using System.Collections;

public class Supers : MonoBehaviour {
    protected Animator animator;
    // Use this for initialization

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        animator = GetComponent<Animator>();
    }
public void triggerSuper1()
        {
            animator.SetTrigger("Super_Call");
        }
public void triggerSuper2()
        {
            animator.SetTrigger("Super_Call2");
        }
}
