using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum Speed { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 }
public enum Gamemodes { Cube = 0, Ship = 1 }

public class newPlayerMoveFix : MonoBehaviour
{
    public float jumpForce = 27f;        // 점프하는 힘
    private Rigidbody2D player;

    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    public Transform Sprite;
    public ParticleSystem dieParticle;
    
    public AudioSource playerSfx;
    public AudioClip jumpSfx;

    public Speed currentSpeed;
    public Gamemodes currentGamemode;
    public float[] SpeedValues = { 9f, 10f, 13f, 16f, 19f };
    int gravity = 1;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        player.gravityScale = 6f;
    }

    private void Update()
    {
        // 오른쪽으로 이동
        Move();

        Invoke(currentGamemode.ToString(), 0);

        
    }

    private void Cube()
    {
        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation * gravity);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.velocity = Vector2.zero;
                player.AddForce(Vector2.up * jumpForce * gravity, ForceMode2D.Impulse);
                JumpSound();
            }
        }
        else
        {
            Sprite.Rotate(Vector3.back, 500f * Time.deltaTime);
        }
    }

    private void Ship()
    {
        Sprite.rotation = Quaternion.Euler(0, 0, player.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.AddForce(Vector2.up * jumpForce * gravity, ForceMode2D.Impulse);
            player.gravityScale = -2f;
        }
        else
        {
            player.gravityScale = 2f;
        }

        player.gravityScale *= gravity;
    }

    private void Move()
    {
        player.velocity = new Vector2(SpeedValues[(int)currentSpeed], player.velocity.y);
    }

    private bool OnGround()
    {
        return Physics2D.OverlapBox(transform.position + Vector3.down * gravity * 0.5f,
            Vector2.right * 1.1f + Vector2.up * GroundCheckRadius, 0, GroundMask);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Dead();
        }
    }

    private void Dead()
    {
        Instantiate(dieParticle, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
        playerSfx.Stop();
        SceneManager.LoadScene("GameOverScene0");
    }

    public void ThroughPortal(Gamemodes Gamemode, Speed Speed, int Gravity, int State)
    {
        switch (State)
        {
            case 0:
                currentSpeed = Speed;
                break;
            case 1:
                currentGamemode = Gamemode;
                if (currentGamemode == Gamemodes.Cube)
                    player.gravityScale = 6f;
                if(currentGamemode == Gamemodes.Ship)
                    player.AddForce(Vector2.up * jumpForce * gravity, ForceMode2D.Impulse);
                break;
            case 2:
                gravity = Gravity;
                player.gravityScale = Mathf.Abs(player.gravityScale) * Gravity;
                break;
        }
    }

    public void JumpSound()
    {
        playerSfx.PlayOneShot(jumpSfx);
    }
}
