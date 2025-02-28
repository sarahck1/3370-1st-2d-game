using UnityEngine;
using UnityEngine.SceneManagement;  // For loading the scene
using UnityEngine.UI;  // For UI elements like Text

public class player_dead : MonoBehaviour
{
    public ParticleSystem collisionParticleSystem;
    public bool once = true;
    public GameObject gameOverText;  
        private void Start()
    {
        // hide game over text at start
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bomb_enemy"))
        {
            // Destroy the player after the collision
            Destroy(this.gameObject);

            // show the Game Over message
            ShowGameOver();
        }

        if (collision.gameObject.CompareTag("power_up"))
        {
            var em = collisionParticleSystem.emission;
            Destroy(collision.gameObject); 
            score_manager.instance.addPoints();  
            if(once)
            {
                em.enabled = true;
                collisionParticleSystem.Play();
                once = false;
            }
        }
    }

    void ShowGameOver()
    {
        if (gameOverText != null)
        {
            // Show the Game Over message
            gameOverText.SetActive(true);
        }

        // Pause the game 
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        // Unpause the game and reload the current scene to restart
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
