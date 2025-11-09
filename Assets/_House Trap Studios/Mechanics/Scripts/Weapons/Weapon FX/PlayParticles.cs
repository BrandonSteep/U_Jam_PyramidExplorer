using UnityEngine;

public class PlayParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    public void PlayParticleEffect(){
        particles.Play();
    }
}
