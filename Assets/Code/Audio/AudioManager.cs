using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    //object sounds
    public AudioClip Player_Death;
    public AudioClip Enemy_Death;
    public AudioClip EnemyBuilding_Death;
    public AudioClip EnemyTank_Death;
    public AudioClip Neutral_Death;
  

    // bullet sounds
    public AudioClip BulletFire_MachineGun;
    public AudioClip BulletFire_Rocket;
    public AudioClip BulletFire_Missile;
    public AudioClip BulletImpact;
    public AudioClip EnemyBullet_Normal;
    public AudioClip EnemyBullet_Building;
    public AudioClip EnemyBullet_Tank;

    // explosion sounds
    public AudioClip Explosion_Rocket;
    public AudioClip Explosion_Missile;

    //UI sounds
    public AudioClip UI_NoAmmo;

    public AudioSource OneShotAudioSource;
 
    #region MonoBehaviour

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
    }

    void Update()
    {

    }

    #endregion

    private bool ValidClip(AudioClip clip)
    {
        if (clip)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Play a single SFX.  Put in a better way to adjust volume
    /// </summary>
    /// <param name="clip">The Audio Clip you want to play</param>
    /// <param name="volume">The volume you want to play your Audio Clip</param>
    public void PlaySFX(AudioClip clip, float volume)
    {
        if (ValidClip(clip))
        {
            OneShotAudioSource.PlayOneShot(clip, volume);
        }
    }
}
