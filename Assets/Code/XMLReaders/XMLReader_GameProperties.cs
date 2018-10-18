using UnityEngine;
using System.Xml;
using System;

public class XMLReader_GameProperties : MonoBehaviour {

    public static int EnemyHealth = 0;
    public static int EnemyBuildingHealth = 0;
    public static int NeutralHealth = 0;
    public static float BulletFireRate_MachineGun = 0;
    public static float BulletFireRate_Rocket = 0;
    public static float BulletFireRate_Missile = 0;
    public static int BulletFireForce_MachineGun = 0;
    public static int BulletFireForce_Rocket = 0;
    public static int BulletFireForce_Missile = 0;
    public static int BulletLifeSpan_MachineGun = 0;
    public static int BulletLifeSpan_Rocket = 0;
    public static int BulletLifeSpan_Missile = 0;
    public static int BulletStrength_MachineGun = 0;
    public static int BulletStrength_Rocket = 0;
    public static int BulletStrength_Missile = 0;
    public static int ExplosionStrength_Rocket = 0;
    public static int ExplosionStrength_Missile = 0;
    public static int ExplosionRadius_Rocket = 0;
    public static int ExplosionRadius_Missile = 0;

    void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/gameProperties", typeof(TextAsset));
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        XmlNodeList items = xmldoc.GetElementsByTagName("Enemy");
        if (items != null && items.Count > 0)
        {
            EnemyHealth = int.Parse(items[0].Attributes["health"].Value);
        }

        items = xmldoc.GetElementsByTagName("EnemyBuilding");
        if (items != null && items.Count > 0)
        {
            EnemyHealth = int.Parse(items[0].Attributes["health"].Value);
        }

        items = xmldoc.GetElementsByTagName("Neutral");
        if (items != null && items.Count > 0)
        {
            EnemyHealth = int.Parse(items[0].Attributes["health"].Value);
        }

        items = xmldoc.GetElementsByTagName("Bullet_MachineGun");
        if (items != null && items.Count > 0)
        {
            BulletStrength_MachineGun = int.Parse(items[0].Attributes["strength"].Value);
            BulletFireRate_MachineGun = float.Parse(items[0].Attributes["fireRate"].Value);
            BulletFireForce_MachineGun = int.Parse(items[0].Attributes["fireForce"].Value);
            BulletLifeSpan_MachineGun = int.Parse(items[0].Attributes["lifeSpan"].Value);
        }

        items = xmldoc.GetElementsByTagName("Bullet_Rocket");
        if (items != null && items.Count > 0)
        {
            BulletStrength_Rocket = int.Parse(items[0].Attributes["strength"].Value);
            BulletFireRate_Rocket = float.Parse(items[0].Attributes["fireRate"].Value);
            BulletFireForce_Rocket = int.Parse(items[0].Attributes["fireForce"].Value);
            BulletLifeSpan_Rocket = int.Parse(items[0].Attributes["lifeSpan"].Value);
        }

        items = xmldoc.GetElementsByTagName("Bullet_Missile");
        if (items != null && items.Count > 0)
        {
            BulletStrength_Missile = int.Parse(items[0].Attributes["strength"].Value);
            BulletFireRate_Missile = float.Parse(items[0].Attributes["fireRate"].Value);
            BulletFireForce_Missile = int.Parse(items[0].Attributes["fireForce"].Value);
            BulletLifeSpan_Missile = int.Parse(items[0].Attributes["lifeSpan"].Value);
        }

        items = xmldoc.GetElementsByTagName("Explosion_Rocket");
        if (items != null && items.Count > 0)
        {
            ExplosionStrength_Rocket = int.Parse(items[0].Attributes["strength"].Value);
            ExplosionRadius_Rocket = int.Parse(items[0].Attributes["radius"].Value);
        }

        items = xmldoc.GetElementsByTagName("Explosion_Missile");
        if (items != null && items.Count > 0)
        {
            ExplosionStrength_Missile = int.Parse(items[0].Attributes["strength"].Value);
            ExplosionRadius_Missile = int.Parse(items[0].Attributes["radius"].Value);
        }
    }
}
