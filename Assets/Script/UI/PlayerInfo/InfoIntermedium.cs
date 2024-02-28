using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoIntermedium : MonoBehaviour
{
    public PlayerController playerScript;
    public GaugeManager gaugeScript;
    public TextDisplayScript attackspeedScript;
    public TextDisplayScript damageMultiplierScript;
    public float playermaxhp;
    // Start is called before the first frame update
    void Start()
    {
        if (playerScript == null)
            playerScript = GameObject.Find("Main Character").GetComponent<PlayerController>();

        if (gaugeScript == null)
            gaugeScript = gameObject.GetComponent<GaugeManager>();
        gaugeScript.SetMaxValue(playerScript._maxHP);

        attackspeedScript._displayType = TextDisplayScript.displaytype.AttackSpeed;
        damageMultiplierScript._displayType = TextDisplayScript.displaytype.DamageMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        gaugeScript.SetCurrentValue(playerScript.HealthPointManager());
        attackspeedScript.setValue(playerScript.AttackSpeedManager());
        damageMultiplierScript.setValue(playerScript.AttackDamageManager());
    }
}
