using UnityEngine;
using System.Collections;

public class BannerWins : MonoBehaviour {
    protected Animator animator;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        animator = GetComponent<Animator>();
    }

    public void triggerWin1()
    {
        animator.SetTrigger("P1Wins");
    }
    public void triggerWin2()
    {
        animator.SetTrigger("P2Wins");
    }
}
