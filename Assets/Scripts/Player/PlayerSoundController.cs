using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundController : PlayerBase
{
    public AudioClip gunShot;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        // Player.Jumped += OnJumped;
        // Player.Attacked += OnAttacked;
        // _player.GroundedChanged += OnGroundedChanged;
        //
        // _moveParticles.Play();
    }

    private void OnDisable()
    {
        // Player.Jumped -= OnJumped;
        // Player.Attacked -= OnAttacked;
        // _player.GroundedChanged -= OnGroundedChanged;

        // _moveParticles.Stop();
    }

    private void OnJumped()
    {
        Debug.Log("Jumped Sound Controller");
    }

    private void OnAttacked()
    {
        _audioSource.clip = gunShot;
        _audioSource.Play();
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (gunShot == null)
            Debug.LogWarning("Please assign a GunShot Audio Clip to the Player Sound Controller in the Gun Shot slot",
                this);
    }
#endif
}