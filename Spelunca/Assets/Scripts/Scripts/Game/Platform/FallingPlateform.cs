using System.Collections;
using UnityEngine;

namespace Platform
{
    /// <summary>
    /// Classe permettant le fonctionnement des FallingPlateforms.
    /// </summary>
    public class FallingPlateform : MonoBehaviour
    {
        /// <value>
        /// Stocke le rigidbody de la plateforme.
        /// </value>
        public Rigidbody2D rb;

        /// <value>
        /// Stocke la durée après laquelle la plateforme chute.
        /// </value>
        public float timeToFall = 1f;

        /// <value>
        /// Stocke la durée après laquelle la plateforme respawn.
        /// </value>
        public float timeToRespawn = 4f;

        /// <value>
        /// Stocke la position de départ de la plateforme.
        /// </value>
        private Vector2 _startPos;

        /// <value>
        /// Permet de savoir si le joueur est rentré en contact avec la plateforme.
        /// </value>
        private bool _plateformIsTouch;

        /// <value>
        /// Définit si la plateforme respawn. Par défault respaws a pour valeur true.
        /// </value>
        public bool respawns = true;

        /// <summary>
        /// Function executed at the start of the program.
        /// Used to get components (<c>_startPos</c>, <c>_plateformIsTouch</c>) from the parent of the current <c>GameObject</c>.
        /// </summary>
        private void Start()
        {
            _startPos = gameObject.transform.position;
            _plateformIsTouch = false;
        }

        /// <summary>
        /// Vérifie si le joueur est entré en collision avec la plateforme.
        /// </summary>
        /// <value>
        /// Si la plateforme est touché, alors la Coroutine Fall est exécutée.
        /// Et la valeur de _plateformIsTouch devient true.
        /// </value>
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player") && respawns && !_plateformIsTouch)
            {
                StartCoroutine(Fall(timeToFall));
                _plateformIsTouch = true;
            }
        }

        /// <summary>
        /// Fait chuter la plateforme.
        /// </summary>
        /// <returns>
        /// Un objet WaitForSeconds qui temporise de la
        /// valeur de respawnDelay puis fait chuter la plateforme.
        /// </returns>
        private IEnumerator Fall(float timeUntilFallDelay)
        {
            yield return new WaitForSeconds(timeUntilFallDelay);
            rb.isKinematic = true;
            rb.velocity = new Vector2(0, -5);
            StartCoroutine(Respawn(timeToRespawn));
        }

        /// <summary>
        /// Fait respawn la plateforme à son point de départ.
        /// </summary>
        /// <returns>
        /// Un objet WaitForSeconds qui temporise de la
        /// valeur de respawnDelay puis fait respawn la plateforme.
        /// </returns>
        private IEnumerator Respawn(float respawnDelay)
        {
            yield return new WaitForSeconds(respawnDelay);

            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
            transform.position = _startPos;
            _plateformIsTouch = false;
        }

        /// <summary>
        /// Fait respawn instantanémment la plateforme à son point de départ.
        /// </summary>
        public void InstantRespawn()
        {
            StopAllCoroutines();
            rb.isKinematic = true;
            rb.velocity = new Vector2(0, 0);
            this.transform.position = _startPos;
            _plateformIsTouch = false;
        }
    }
}