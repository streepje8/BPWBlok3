using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : Singleton<PlayerStats>
{
    public List<RuntimeAbility> ActiveAbilities = new List<RuntimeAbility>();
    public float BaseDamage = 1f;
    public float HP = 20;
    public float shields = 0f;
    public float AddedDamage = 0f;
    public int stamina = 3;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        for(int i = ActiveAbilities.Count - 1; i >= 0; i--)
        {
            Ability ability = ActiveAbilities[i].ability;
            ability.AbilityUpdate();
            if(Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(1))
            {
                ability.AbilityUse();
            }
            ActiveAbilities[i].lifeTime -= Time.deltaTime;
            if(ActiveAbilities[i].lifeTime <= 0)
            {
                ability.AbilityEnd();
                ActiveAbilities.RemoveAt(i);
            }
        }
    }

    public void ActivateAbility(Ability a)
    {
        ActiveAbilities.Add(new RuntimeAbility(a));
        a.AbilityStart();
    }

    public void HealPlayer(float amount)
    {
        HP += amount;
    }

    public void DamagePlayer(float amount)
    {
        if(shields > 0)
        {
            if(shields < amount)
            {
                amount -= shields;
                shields = 0;
            } else
            {
                shields -= amount;
                amount = 0;
            }
        }
        HP -= amount;
        if(HP <= 0)
        {
            HP = 0;
            Die();
        }
    }

    public void Die()
    {
        if (File.Exists(Savedata.SaveFile))
        {
            File.Delete(Savedata.SaveFile);
        }
        SceneManager.LoadScene(2);
    }
}

public class RuntimeAbility
{
    public Ability ability;
    public float lifeTime = 0;
    private Ability a;

    public RuntimeAbility(Ability a)
    {
        ability = a;
        lifeTime = a.duration;
    }
}
