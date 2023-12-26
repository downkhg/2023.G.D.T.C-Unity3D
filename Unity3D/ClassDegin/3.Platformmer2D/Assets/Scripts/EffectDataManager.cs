using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void Use(GameObject obj);
    //virtual void Use(GameObject obj);//인터페이스에서는 virtual키워드가 없어도 모든 멤버함수가 virtual된다.
}

public class SuperModeEffect : IEffect
{
    SuperMode superMode;

    public void Use(GameObject obj)
    {
        superMode = obj.GetComponent<SuperMode>();
        superMode.OnMode();
    }
}

public class RecoryEffect : IEffect
{
    Player player;

    public void Use(GameObject obj)
    {
        player = obj.GetComponent<Player>();
        player.Recovery();
    }
}

public class BulletEffect : IEffect
{
    Gun gun;

    public void Use(GameObject obj)
    {
        Dynamic dynamic = obj.GetComponent<Dynamic>();
        if (dynamic)
        {
            gun = dynamic.gun;
            gun.eGunState = Gun.E_GUN_STATE.BULLET_GUN;
        }
    }
}

public class LaserEffect : IEffect
{
    Gun gun;

    public void Use(GameObject obj)
    {
        Dynamic dynamic = obj.GetComponent<Dynamic>();
        if (dynamic)
        {
            gun = dynamic.gun;
            gun.eGunState = Gun.E_GUN_STATE.LASER_GUN;
        }
    }
}


public class EffectDataManager : MonoBehaviour
{
    public enum EffectFucntion { SUPER_MODE, RECOVERY, BULLET, LASER, GRENADE, MAX }
    List<IEffect> listEffects;


    public IEffect GetEffect(int idx)
    {
        return listEffects[idx];
    }

    public IEffect GetEffect(EffectFucntion enffect)
    {
        return listEffects[(int)enffect];
    }

    public void InitEffectData()
    {
        listEffects = new List<IEffect>((int)EffectFucntion.MAX);
        listEffects.Add(new SuperModeEffect());
        listEffects.Add(new RecoryEffect());
        listEffects.Add(new BulletEffect());
        listEffects.Add(new LaserEffect());
    }

    public void Release()
    {
        listEffects.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
