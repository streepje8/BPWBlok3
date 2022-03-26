using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
