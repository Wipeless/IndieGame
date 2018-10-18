using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen : MonoBehaviour {

    public Text m_IntelScore;
    public Text m_RemainingTimeScore;
    public Text m_AmmoScore;
    public Text m_FuelScore;
    public Text m_PlayerGunName;
    public Text m_PlayerGunValues;

	// Use this for initialization
	void Start () {
        SetInitBulletText();
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

    public void SetPlayerGunName(PlayerObject.GunSelection selection)
    {
        // TODO: Fix Color object to not be hardcoded
        m_PlayerGunName.text = selection.ToString();
        switch (selection)
        {
            case PlayerObject.GunSelection.MACHINEGUN:
                m_PlayerGunName.color = new Color(24f/255f, 209f/255f, 19f/255f);
                break;
            case PlayerObject.GunSelection.ROCKET:
                m_PlayerGunName.color = new Color(93f/255f, 109f/255f, 255f/255f);
                break;
            case PlayerObject.GunSelection.MISSILE:
                m_PlayerGunName.color = new Color(255f/255f, 0f/255f, 197f/255f);
                break;
        }
    }

    private void SetInitBulletText()
    {
        // TODO: Fix this so there isn't so many hardcoded values
        string bulletText = "<color=#18d113>1</color> | <color=#5d6dff>2</color> | <color=#ff00c5>3</color>";

        m_PlayerGunValues.text = bulletText;
    }
}
