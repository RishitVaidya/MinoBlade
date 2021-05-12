using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Base_Enemy : MonoBehaviour
{

    [Header("Enemy Properties")]

    public Vector2 hitPointsRange;
    public int hitPoints;
    public TextMesh tm_hitPoints;

    public int crystals;

    [Header("Visual Properties")]
    public GameObject go_Visual;
    public SpriteRenderer sr_this;

    [Header("Effects")]
    public ParticleSystem ps_Hit;
    public ParticleSystem ps_Dead;

    public AudioSource as_hit;
    public AudioSource as_Dead;

    private Tween tween_InflateEffect;
    private Tween tween_HitEffect;


    private void Start()
    {
        Set_TextMest_HitPoints();
    }

    private void OnDisable()
    {
        tween_InflateEffect.Kill();
        tween_HitEffect.Kill();
    }


    #region ENEMY_PROPERTIES

    public void Set_TextMest_HitPoints()
    {
        if (hitPoints < 1000)
        {
            tm_hitPoints.text = hitPoints.ToString();
        }
        else
        {
            int thousandthPlace = hitPoints / 1000;
            int hundredthPlace = (hitPoints % 1000)/100;

            string finalText = thousandthPlace.ToString();

            if(hundredthPlace != 0)
            {
                finalText += "." + hundredthPlace.ToString();
            }

            finalText += "K";

            tm_hitPoints.text = finalText;
        }
        
    }

    Tween tween_shake;
    private void Shake()
    {
        if (tween_shake != null)
        {
            tween_shake.Kill();
        }
        transform.GetChild(0).DOShakePosition(0.5f, 0.15f, 50, 90);
    }

    public void Trigger_MainBlade()
    {
        as_hit.Play();
        Shake();
        ps_Hit.Stop();
        ps_Hit.Play();
        ReduceHitPoints(GameManager.Instance.playerController.bladeDamage);
        HitEffect();

        if (GameManager.Instance.isPoisonEffect)
        {
            EnemyDead();

        }
        else
        {
            if (hitPoints <= 0)
            {
                EnemyDead();
            }
            else
            {
                //as_hit.Play();
                Set_TextMest_HitPoints();
            }
        }




    }

    public void EnemyDead()
    {

        as_Dead.Play();
        ps_Dead.Play();
        GameManager.Instance.CollectCrystals(crystals);
        

        DisableEnemy();
        if (GetComponent<SnakeNode>() != null)
        {
            SnakeNode snakeNode = GetComponent<SnakeNode>();

            if (snakeNode.snakeBoss.list_bodyparts.Count == 1)
            {
                GameManager.Instance.EnemyKilled();
            }
            else
            {
                snakeNode.snakeBoss.list_bodyparts.Remove(snakeNode);
            }
        }

        else if(GetComponent<RotorWings>() != null)
        {
            RotorWings rotorWings = GetComponent<RotorWings>();
            rotorWings.RotorBoss.list_allChild.Remove(rotorWings);
        }

 

        else
        {
            if(GetComponent<Spit>() == null)
            {
                GameManager.Instance.EnemyKilled();
            }
            
               
        }
    }

    public void DisableEnemy()
    {
        go_Visual.SetActive(false);
        this.GetComponent<Collider>().enabled = false;
        StartCoroutine(DestroySelf_());
    }

    public void ReduceHitPoints(int value)
    {
        hitPoints -= value;
    }


    #endregion

    #region ANIMATION_PROPERTIES

 
    public void Inflate()
    {
        tween_InflateEffect = go_Visual.transform.DOScale(new Vector3(0.95f, 1, 1.05f), 0.5f).OnComplete(Deflate);
    }

    public void Deflate()
    {
        tween_InflateEffect = go_Visual.transform.DOScale(new Vector3(1.05f, 1, 0.95f), 0.5f).OnComplete(Inflate);
    }
    #endregion



    public void HitEffect()
    {
        sr_this.DOColor(Color.grey, 0.1f).OnComplete(BackToOrignalColor);
    }

    private void SetRandomHitPointsInRange()
    {
        //float multiplier = AllLevels.Instance.allLevelsData[LevelManager.Instance.currentLevelIndex].HitPointsMultiplier;

        //hitPoints = (int)Random.Range(hitPointsRange.x *multiplier , hitPointsRange.y *multiplier);
        hitPoints = 50;
    }

    private void BackToOrignalColor()
    {
        sr_this.DOColor(Color.white, 0.1f);
    }

    protected void Dead()
    {
        
        Destroy(gameObject);
    }

    IEnumerator DestroySelf_()
    {
        yield return new WaitForSeconds(3);
        Dead();
    }

}
