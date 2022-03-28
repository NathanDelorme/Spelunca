using Data;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    /// <summary>
    /// Cette classe r�cup�re et interpr�te les inputs du joueur (touches appuy�es).
    /// Cela permet de savoir ce que le joueur souhaite faire comme actions.
    /// </summary>
    public class NewInputController : MonoBehaviour
    {
        /// <value>
        /// Cette propri�t� (<see cref="PlayerState"/>) est un ScriptableObject.
        /// Cet attribut stocke toutes les variables utiles pour savoir ce que le joueur veut faire,
        /// ce qu'il peut faire, ainsi que ce qu'il est en train de faire.
        /// </value>
        public PlayerState playerState;
        /// <value>
        /// Cette propri�t� (<see cref="MovementSettings"/>) est un ScriptableObject.
        /// Cet attribut stocke toutes les variables utiles pour les mouvements (exemple : force du saut ou duration du dash).
        /// </value>
        public MovementSettings movementSettings;
        /// <value>
        /// Composant permettant de mettre le jeu en pause.
        /// </value>
        private PauseUI pauseUI;

        /// <summary>
        /// Fonction ex�cut� avant la premi�re frame du programme, donc avant le premier appel � Update.
        /// </summary>
        public void Start()
        {
            pauseUI = FindObjectOfType<PauseUI>();
            playerState.wantToMove = true;
        }

        /// <summary>
        /// Fonction ex�cut� lorsque le joueur appuis sur une touche de d�placement.
        /// </summary>
        /// <param name="context">Informations sur la touches appuy�e</param>
        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.started || context.performed)
            {
                if (context.ReadValue<Vector2>().x > 0f)
                    playerState.facing = 1f;
                else
                    playerState.facing = -1f;
            }
            playerState.horDir = context.ReadValue<Vector2>().x;
            playerState.verDir = context.ReadValue<Vector2>().y;
        }

        /// <summary>
        /// Fonction ex�cut� lorsque le joueur appuis sur une touche de saut.
        /// </summary>
        /// <param name="context">Informations sur la touches appuy�e</param>
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                playerState.wantToJump = true;
            }
            if (context.canceled)
            {
                playerState.wantToJump = false;
            }
        }

        /// <summary>
        /// Fonction ex�cut� lorsque le joueur appuis sur une touche de dash.
        /// </summary>
        /// <param name="context">Informations sur la touches appuy�e</param>
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                playerState.wantToDash = true;
            }
            if (context.canceled)
            {
                playerState.wantToDash = false;
            }
        }

        /// <summary>
        /// Fonction ex�cut� lorsque le joueur appuis sur une touche de mise en pause du jeu.
        /// </summary>
        /// <param name="context">Informations sur la touches appuy�e</param>
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                pauseUI.OnPause();
            }
        }
    }
}

