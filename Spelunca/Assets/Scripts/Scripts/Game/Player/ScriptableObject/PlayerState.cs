using UnityEngine;

namespace Data
{
    /// <summary>
    /// Cette classe est un ScriptableObject qui contient toutes les variables nécessaire pour définir l'état du joueur en temps réel.
    /// </summary>
    [CreateAssetMenu]
    public class PlayerState : ScriptableObject
    {
        /// <summary>
        /// Enumération de tous les états possible de la friction du joueur.
        /// </summary>
        public enum DragType
        {
            GROUND,
            AIR,
            WALL,
            NONE
        }

        /// <summary>
        /// Enumération de tous les pouvoirs que le joueur peut avoir.
        /// </summary>
        public enum Ability
        {
            SPIKE,
            CRYSTAL,
            NONE
        }

        [Header("General variables")]
        /// <value>
        /// Pouvoir que le joueur possède.
        /// </value>
        public Ability AbilityType = Ability.NONE;
        /// <value>
        /// Friction à appliquer au joueur.
        /// </value>
        public DragType linearDragType = DragType.NONE;
        /// <value>
        /// Côté où le joueur regarde.
        /// 1f : regarde à droite
        /// -1f : regarde à gauche
        /// </value>
        public float facing = 1f;
        /// <value>
        /// Axe x où le joueur regarde
        /// Higher than 0 : Aller vers la droite
        /// Lower than 0 : Aller vers la gauche
        /// 0f: Pas de mouvement
        /// </value>
        public float horDir = 0f;
        /// <value>
        /// Axe y où le joueur regarde
        /// > 0 : vers le haut
        /// < 0 : vers le bas
        /// 0 : Rien
        /// </value>
        public float verDir = 0f;

        [Header("Movement variables")]
        /// <value>
        /// Booléen qui défini si le joueur peut se déplacer.
        /// </value>
        public bool canMove = true;
        /// <value>
        /// Booléen qui défini si le joueur veut se déplacer.
        /// </value>
        public bool wantToMove = true;
        /// <value>
        /// Booléen qui défini si le joueur se déplace.
        /// </value>
        public bool isMoving = false;

        [Header("Jump variables")]
        /// <value>
        /// Booléen qui défini si le joueur peut sauter.
        /// </value>
        public bool canJump = true;
        /// <value>
        /// Booléen qui défini si le joueur veut sauter.
        /// </value>
        public bool wantToJump = false;
        /// <value>
        /// Booléen qui défini si le joueur saute.
        /// </value>
        public bool isJumping = false;

        [Header("Dash variables")]
        /// <value>
        /// Booléen qui défini si le joueur peut dash.
        /// </value>
        public bool canDash = true;
        /// <value>
        /// Booléen qui défini si le joueur veut dash.
        /// </value>
        public bool wantToDash = false;
        /// <value>
        /// Booléen qui défini si le joueur dash.
        /// </value>
        public bool isDashing = false;
        /// <value>
        /// Compteur pour la durée du dash.
        /// </value>
        public float currentDashTime = 0f;

        [Header("Wall slide variables")]
        /// <value>
        /// Booléen qui défini si le joueur peut wall slide.
        /// </value>
        public bool canWallSlide = true;
        /// <value>
        /// Entier qui défini si le joueur veut wall slide.
        /// 0 : pas de wall slide
        /// 1 : wall slide sur la droite
        /// -1 : wall slide sur la gauche
        /// 2 : wall slide les deux côtés
        /// </value>
        public int wallSlideSide = 0;
        /// <value>
        /// Booléen qui défini si le joueur est en train de wall slide.
        /// </value>
        public bool isWallSliding = false;

        [Header("Wall jump variables")]
        /// <value>
        /// Booléen qui défini si le joueur peut wall jump.
        /// </value>
        public bool canWallJump = false;
        /// <value>
        /// Entier qui défini si le joueur veut wall jump.
        /// 0 : pas de wall slide
        /// 1 : wall jump sur la droite
        /// -1 : wall slide sur la gauche
        /// </value>
        public int wallJumpSide = 0;
        /// <value>
        /// Booléen qui défini si le joueur est en train de wall jump.
        /// </value>
        public bool isWallJumping = false;

        /// <summary>
        /// Fonction qui initialise les variables de ce ScriptableObject.
        /// </summary>
        public void Initialize()
        {
            AbilityType = Ability.NONE;
            linearDragType = DragType.NONE;
            facing = 1f;
            horDir = 0f;
            verDir = 0f;

            canMove = true;
            wantToMove = true;
            isMoving = false;

            canJump = true;
            wantToJump = false;
            isJumping = false;

            canDash = true;
            wantToDash = false;
            isDashing = false;
            currentDashTime = 0f;

            canWallSlide = true;
            wallSlideSide = 0;
            isWallSliding = false;

            canWallJump = false;
            wallJumpSide = 0;
            isWallJumping = false;
        }
    }
}
