﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartPivotController : MonoBehaviour {
    Sprite dartSprite;
    Transform dart;
    Transform player;
    float dartWidth;
    float dartHeight;
    public float initialDegree;
    public float dartSpeed;
    // in degree
    public float rotationSpeed;
    public float rotationAngleLimit;
    // for play to check
    public bool finishToThrow;
    public bool finishThrow;

    // Use this for initialization
    void Start()
    {
        dartSprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        dart = transform.GetChild(0);
        player = FindObjectOfType<PlayerController>().transform;

        dartWidth = dartSprite.bounds.size.x * dart.localScale.x;
        dartHeight = dartSprite.bounds.size.y * dart.localScale.y;
        dart.localRotation = Quaternion.Euler(0,0,0);
        dart.localPosition = new Vector2(dartWidth / 2, 0);
        dart.GetComponent<Rigidbody2D>().gravityScale = 0f;
        finishThrow = true;
        InitDart();
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector2.zero;
        dart.localRotation = Quaternion.identity;
    }

    void InitDart()
    {       
        transform.localRotation = Quaternion.Euler(0, 0, initialDegree);
        dart.localRotation = Quaternion.identity;
        dart.gameObject.SetActive(false);
    }

    // player调用的函数们
    public void setThrowParameters()
    {
        finishThrow = false;
        finishToThrow = false;
    }
    public void StartRotationSelection()
    {
        dart.gameObject.SetActive(true);
    }

    public void AdjustDart(float xInput)
    {
        float tiltAroundZ = transform.eulerAngles.z + xInput * rotationSpeed;
        if (Mathf.Abs(transform.localEulerAngles.z + xInput * rotationSpeed * Time.deltaTime - initialDegree) < rotationAngleLimit || Mathf.Abs(360 - (transform.localEulerAngles.z + xInput * rotationSpeed * Time.deltaTime - initialDegree)) < rotationAngleLimit)
        {
            Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
        }

    }

    public void StartDart()
    {
        GameObject newDart = Instantiate(dart.gameObject, dart.transform.position, dart.rotation);
        newDart.GetComponent<Rigidbody2D>().velocity = dart.transform.TransformDirection(new Vector2(-dartSpeed, 0));
        newDart.transform.parent = null;
        InitDart();
    }

    // animator调用的函数
    public void FinishToThrow()
    {
        finishToThrow = true;
    }

    public void FinishThrow()
    {
        finishThrow = true;
    }
}