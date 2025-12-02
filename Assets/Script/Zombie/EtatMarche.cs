using UnityEngine;

public class EtatMarche : EtatsZombie.EtatZombie
{
    public override void EntrerEtat(Zombie zombie)
    {
        base.EntrerEtat(zombie);
        zombie.Deplacement();
        zombie.DemarrerMarche();
    }
    public override EtatsZombie.EtatZombie ExecuterEtat(Zombie zombie)
    {
        return this;
    }
}
