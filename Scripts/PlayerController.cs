using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public int bladeDamage;
    public int PlayerPrice;

    public Rigidbody this_rb;
    private Vector3 mouseDownPosition;
    private Vector3 playerPositionOnMouseDown;
    private Vector3 mousePosition;

    public Transform mainWeapon;
    public Transform[] allBlades;
    public int nextBladeIndex;
    public bool hasEatenPoison = false;
    public bool isGameStart =false;

    public AudioSource as_Dead;

    private void Awake()
    {
        this_rb = this.GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mainWeapon.Rotate(Vector3.forward * Time.deltaTime * 300);

        if (Input.GetMouseButtonDown(0) && isGameStart)
        {
            mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerPositionOnMouseDown = transform.position;
        }

        if (Input.GetMouseButton(0) && isGameStart)
        {
            
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 positionDiff = mousePosition - mouseDownPosition;
            Vector3 playerTargetPosition = playerPositionOnMouseDown + positionDiff;

            playerTargetPosition.x = Mathf.Clamp(playerTargetPosition.x, GameManager.Instance.boundaries[2].position.x + 0.45f, GameManager.Instance.boundaries[3].position.x - 0.45f);
            playerTargetPosition.y = 0;
            playerTargetPosition.z = Mathf.Clamp(playerTargetPosition.z, GameManager.Instance.boundaries[0].position.z + 0.45f, GameManager.Instance.boundaries[1].position.z - 0.45f);

            this_rb.velocity = (playerTargetPosition - transform.position) * 20;
        }
        else
        {
            this_rb.velocity = Vector3.zero;
        }

        if(Input.GetMouseButtonUp(0) && isGameStart)
        {
            this_rb.velocity = Vector3.zero;
        }

   
    }


    public void SetPlayerPostion(Transform spawnPosition)
    {
        StartCoroutine(movePlayerToSpawn_(spawnPosition.position));
    }


    Vector3 previousPos;
    private void LateUpdate()
    {
        previousPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance.playerController.isGameStart)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Dead();
            }

            else if (collision.gameObject.CompareTag("Boss"))
            {
                Dead();
            }         
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.playerController.isGameStart)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Dead();
            }

            else if (other.gameObject.CompareTag("Boss"))
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        this_rb.velocity = Vector3.zero;
        //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        //Destroy(ActiveLevel.Instance.boss.gameObject);
        //ActiveLevel.Instance.List_BossReset.Clear();
        //GameManager.Instance.playerController.isGameStart = false;
         PlayerDeadEffect();

        DDOL.Instance.allWorlds[DDOL.Instance.currentWorldIndex].stats.fails++;

        DataManager.Instance.SetTotalFailsForCurrentWorld();

    }

    private void PlayerDeadEffect()
    {
        as_Dead.Play();
        transform.Translate(transform.up);
        Expand();

        AdManager.Instance.ShowInterstitial();
    }

    private void Expand()
    {
        transform.DOScaleX(0.7f, 0.15f).SetEase(Ease.InOutFlash);
        transform.DOScaleY(0.7f, 0.15f).SetEase(Ease.InOutFlash).OnComplete(Shrink);
    }

    private void Shrink()
    {
        transform.DOScaleX(0, 0.15f).SetEase(Ease.InOutFlash);
        transform.DOScaleY(0, 0.15f).SetEase(Ease.InOutFlash).OnComplete(AfterPlayerDead);
    }

    private void AfterPlayerDead()
    {

        //GameView.Instance.ShowReviveView();
        GameView.Instance.ShowGameOverView();
        GameManager.Instance.playerController.gameObject.SetActive(false);
        transform.localScale = Vector3.one * 0.4f;
        transform.Translate(-transform.up);
    }



    public void AddBlade()
    {
        allBlades[nextBladeIndex].gameObject.SetActive(true);

        float angleBetween2Blades = 360f / (nextBladeIndex + 1);
        float bladeAngle = 0;

        for (int i = 0; i < allBlades.Length; i++)
        {
            allBlades[i].localEulerAngles = new Vector3(0, 0, bladeAngle);
            bladeAngle += angleBetween2Blades;
            allBlades[i].Translate(Vector3.up * 0 , Space.Self);
        }

        nextBladeIndex++;
    }

    private IEnumerator movePlayerToSpawn_(Vector3 spawnPosition)
    {
        float time = 0;
        float rate = 1f;
        Vector3 initialPosition = transform.position;
        Vector3 finalPosition = spawnPosition;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(initialPosition, finalPosition, time);
            time += Time.deltaTime / rate;
            yield return null;
        }
        GameManager.Instance.playerController.isGameStart = true;
        GameManager.Instance.playerController.this_rb.velocity = Vector3.zero;
    }
    
    public void Revive()
    {
        GameManager.Instance.playerController.gameObject.SetActive(true);
        GameManager.Instance.playerController.isGameStart = true;

        StartCoroutine(PlayerRevived());
    }

    IEnumerator PlayerRevived()
    {
        GameManager.Instance.playerController.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(5);

        GameManager.Instance.playerController.GetComponent<Collider>().enabled = true;
    }
}
