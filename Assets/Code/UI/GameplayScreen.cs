using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen : MonoBehaviour {

    public Text m_IntelScore;
    public Text m_RemainingTimeScore;
    public Text m_AmmoScore;
    public Text m_FuelScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetIntelScore(int val)
    {
        m_IntelScore.text = val.ToString();
    }

    public void SetAmmoScore(int val)
    {
        m_AmmoScore.text = val.ToString();
    }

    public void SetFuelScore(int val)
    {
        m_FuelScore.text = val.ToString();
    }

    public void SetRemainingTime(int valMin, int valSec)
    {
        m_RemainingTimeScore.text = valMin.ToString() + ":" + valSec.ToString();
    }
}
