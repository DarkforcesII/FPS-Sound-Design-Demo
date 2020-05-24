using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private GameObject mEnemy;

    [SerializeField]
    float UpTime = 3.0f;
    [SerializeField]
    float ShootTime = 2.0f;
    [SerializeField]
    float DownTime = 2.0f;

    // height
    [SerializeField]
    float height = 0.1967f;

    [SerializeField]
    float sfxTime;

    private bool mIsActive = false;

    private Animator mAnimator = null;

    // referencing audio script
    public SfxScript sfxScript;

    // Start is called before the first frame update
    void Awake()
    {
        mEnemy = transform.GetChild(0).gameObject;
        mAnimator = mEnemy.GetComponent<Animator>();
    }

    public void Activate()
    {
        mIsActive = true;
        
        MoveUpwards();

        // Start shooting
        mAnimator.SetBool("Shoot", true);
        StartCoroutine(ShotGunSound());

        Invoke("MoveDownwards", ShootTime);
    }

    IEnumerator ShotGunSound()
    {
        yield return new WaitForSeconds(sfxTime);
        sfxScript.ShootSound();
    }

    private void MoveUpwards()
    {
        // Move upwards
        Vector3 enemyPos = mEnemy.transform.position;
        //enemyPos.y = enemyPos.y + 0.1967f;
        enemyPos.y = enemyPos.y + height;

        iTween.MoveTo(mEnemy, enemyPos, UpTime);
    }

    private void MoveDownwards()
    {
        // Stop shooting
        mAnimator.SetBool("Shoot", false);

        // Move downwards
        Vector3 enemyPos = mEnemy.transform.position;
        //enemyPos.y = enemyPos.y - 0.1967f;
        enemyPos.y = enemyPos.y - height;

        iTween.MoveTo(mEnemy, enemyPos, DownTime);
        iTween.MoveTo(mEnemy, iTween.Hash("y", enemyPos.y, "time", DownTime, "onComplete",
            "OnDownComplete", "onCompleteTarget", gameObject));
    }

    void OnDownComplete()
    {
        mIsActive = false;
    }

    public bool IsActive
    {
        get { return mIsActive; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
