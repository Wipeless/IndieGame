using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen : MonoBehaviour {

    public Text m_IntelScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetIntelScore(int val)
    {
        m_IntelScore.text = val.ToString();
    }
}
