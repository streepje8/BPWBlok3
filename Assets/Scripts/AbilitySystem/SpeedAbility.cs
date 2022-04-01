using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * SpeedAbility
 * Wessel Roelofse
 * 01/04/2022
 * 
 * An Ability that increases the speed of the player
*/
[CreateAssetMenu(menuName = "Abilities/Speed", order = 100)]
public class SpeedAbility : Ability
{
    public override void AbilityEnd()
    {
        GameController.Instance.player.timeBetweenSteps += 0.1f;
    }

    public override void AbilityStart()
    {
        GameController.Instance.player.timeBetweenSteps -= 0.1f;
    }

    public override void AbilityUpdate()
    {
        
    }

    public override void AbilityUse()
    {
        
    }
}
