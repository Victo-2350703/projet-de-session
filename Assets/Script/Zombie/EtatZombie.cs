using UnityEngine;

namespace EtatsZombie
{
    /// <summary>
    /// Gère le comportement d'un zombie
    /// </summary>
    public abstract class EtatZombie
    {
        /// <summary>
        /// Actions à accomplir lorsqu'un zombie entre dans l'état
        /// </summary>
        /// <param name="zombie">Le zombie dont on gère le comportement</param>
        public virtual void EntrerEtat(Zombie zombie)
        {
            Debug.Log($"Zombie entre dans l'état {this.GetType().Name}");
        }

        /// <summary>
        /// Actions à accomplir lorsqu'un zombie est dans cet état. Comprends aussi la logique de 
        /// passage aux autres états.
        /// </summary>
        /// <param name="zombie">Le zombie dont on gère le comportement</param>
        /// <returns>L'état à exécuter à la prochaine frame.</returns>
        public abstract EtatZombie ExecuterEtat(Zombie zombie);

        /// <summary>
        /// Actions à accomplir lorsqu'un zombie sort de l'état
        /// </summary>
        /// <param name="zombie">Le zombie dont on gère le comportement</param>
        public virtual void SortirEtat(Zombie Zombie)
        {
            Debug.Log($"Zombie sort de l'état {this.GetType().Name}");
        }
    }
}
