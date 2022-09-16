using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody m_Rigidbody;

    [SerializeField]
    public Configurations Configuration;

    private AudioSource[] ColisionSound;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        ColisionSound = GameObject.Find("MainManager").GetComponents<AudioSource>();
    }

    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;

        if (Configuration.AppConfig.EnableSound)
            ColisionSound[5].Play();

        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;

        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;

        //max velocity
        if (velocity.magnitude > Configuration.AppConfig.MaxballSpeed)
            velocity = velocity.normalized * Configuration.AppConfig.MaxballSpeed;

        m_Rigidbody.velocity = velocity;
    }
}
