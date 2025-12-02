using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using EtatsZombie;
using Unity.VisualScripting;

/// <summary>
/// Gère un zombie dans le jeu. Le zombie peut :
/// 
/// - avancer vers la base
/// </summary>
[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Animator))]
public class Zombie : MonoBehaviour
{
    /// <summary>
    /// NavMeshAgent qui gère le zombie
    /// </summary>
    private NavMeshAgent agent = null;

    // Contrôleur d'animation du zombie
    private Animator controleurAnimation;

    [SerializeField, Tooltip("Temps avant de faire disparaître le monstre après sa mort")]

    private float tempsMort;

    #region Gestion des deplacements

    public float zCible = 9.9f;

    /// <summary>
    /// La destination du zombie pour se déplacer
    /// </summary>
    private Vector3 destination;
    public Vector3 Destination { get => destination; set { destination = value; } }

    [SerializeField, Tooltip("Vitesse de déplacement de base du zombie")]
    private float vitesseNormale = 3.5f;

    [SerializeField, Tooltip("Vitesse du zombie blesser")]
    private float vitesseRalentie = 1.5f;

    [SerializeField, Tooltip("Vitesse actuelle du zombie")]
    private float vitesseActuelle;
    #endregion

    #region Gestion de la vie
    // Vie actuelle du zombie
    private float vie;

    [SerializeField, Tooltip("Vie maximale (et initiale) du zombie.")]
    private float vieMaximale;
    #endregion

    #region Gestion de l'état du zombie
    /// <summary>
    /// État actuel du zombie
    /// </summary>
    private EtatZombie etatPrecedent;

    /// <summary>
    /// L'état du Zombie à la prochaine frame
    /// </summary>
    private EtatZombie prochainEtat;

    /// <summary>
    /// Détermine si l'on doit exécuter la machine à état
    /// </summary>
    private bool executerEtat;
    #endregion

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        controleurAnimation = GetComponent<Animator>();

        vitesseActuelle = vitesseNormale;

        agent.speed = vitesseActuelle;

        etatPrecedent = null;
        prochainEtat = new EtatMarche();
        executerEtat = true;

        vie = vieMaximale;
    }

    private void Start()
    {
    }
    private void Update()
    {
        if (!executerEtat)
        {
            // On arrête l'état en cours, s'il n'est pas déjà arrêté
            if (prochainEtat != null && prochainEtat == etatPrecedent)
            {
                prochainEtat.SortirEtat(this);
                prochainEtat = null;
                etatPrecedent = null;
            }

            return;
        }

        // Prochain état représente l'état à exécuter
        if (etatPrecedent != prochainEtat)
        {
            prochainEtat.EntrerEtat(this);
        }
        // On n'a plus besoin de cette valeur, on fait donc la mise à jour
        etatPrecedent = prochainEtat;
        prochainEtat = prochainEtat.ExecuterEtat(this);      // Met à jour le prochain état à exécuter. 
                                                             // À partir de ce point, l'état de la frame est accessible dans etatPrecedent (voir ligne au-dessus).

        if (etatPrecedent != prochainEtat)
        {
            etatPrecedent.SortirEtat(this);
        }
    }


    public void Deplacement()
    {
        // On garde le même X et Y, on change seulement Z
        Vector3 pointCible = new Vector3(
            transform.position.x,
            transform.position.y,
            zCible
        );

        agent.SetDestination(pointCible);
    }

    public void DemarrerMarche()
    {
        controleurAnimation.SetBool("Marcher", true);
    }

    public void ChangerVitesse(float nouvelleVitesse)
    {
        vitesseActuelle = nouvelleVitesse;
        agent.speed = nouvelleVitesse;

        // Si tu as un blend tree pour la marche :
        controleurAnimation.SetFloat("Vitesse", nouvelleVitesse);
    }


    public void RecevoirDegats(float degatsRecu)
    {
        vie -= degatsRecu;

        if (vie <= vieMaximale * 0.5f)
        {
            // Zombie blessé : ralentir
            ChangerVitesse(vitesseRalentie);
        }

        if (vie <= 0)
        {
           // Mourir();
        }
    }


    /// <summary>
    /// Déclenche aléatoirement l'une des 2 animations de mort du monstre
    /// </summary>
    //public IEnumerator Mourir()
    //{
        ///int mortAleatoire = Random.Range(0, 2); // 0 ou 1
        //controleurAnimation.SetInteger("typeMort", mortAleatoire);
        //controleurAnimation.SetTrigger("Mourrir");

        // Arrête la machine à état
        //executerEtat = false;
        // Attend avant de supprimer le monstre
       // yield return new WaitForSeconds(tempsMort);
       // Destroy(gameObject);
    //}

}
