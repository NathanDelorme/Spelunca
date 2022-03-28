using UnityEngine;

namespace Platform
{
    /// <summary>
    /// Classe permettant à une plateforme de se déplacer sur une liste de points.
    /// </summary>
    public class Movement : MonoBehaviour
    {
        /// <value>
        /// Liste des points.
        /// </value>
        [SerializeField] private GameObject[] pos;
        /// <value>
        /// Plateforme à déplacer.
        /// </value>
        [SerializeField] private GameObject platform;
        /// <value>
        /// Index sur lequel la plateforme doit aller.
        /// </value>
        private int _currentPositionIndex = 0;
        /// <value>
        /// Vitesse de la plateforme.
        /// </value>
        [SerializeField] private float speed = 2f;

        /// <summary>
        /// Fonction exécuté un nombre déterminé de fois par secondes.
        /// </summary>
        private void FixedUpdate()
        {
            if (Vector2.Distance(pos[_currentPositionIndex].transform.position, platform.transform.position) < .1f)
            {
                _currentPositionIndex++;
                if (_currentPositionIndex >= pos.Length)
                    _currentPositionIndex = 0;
            }
            platform.transform.position = Vector2.MoveTowards(platform.transform.position, pos[_currentPositionIndex].transform.position, Time.deltaTime * speed);
        }

        /// <summary>
        /// Permet de faire réapparaître la plateforme à son point initial.
        /// </summary>
        public void Respawn()
        {
            _currentPositionIndex = 0;
            platform.transform.position = pos[_currentPositionIndex].transform.position;
        }
    }
}

