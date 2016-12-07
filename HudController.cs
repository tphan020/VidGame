using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    public Fighter player1;
    public Fighter player2;

    public Scrollbar leftBar;
    public Scrollbar rightBar;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (leftBar.size> (player1.healthPercentage))
        {
            leftBar.size -= (0.01f);
        }
        if (rightBar.size> (player2.healthPercentage))
        {
            rightBar.size -= 0.01f;
        }
	}
}
