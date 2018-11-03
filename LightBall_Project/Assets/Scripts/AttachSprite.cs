using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachSprite : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite sprite;
    [Header("Position Offsets   [-10    ->    10]")]
    [Range(-10.0f, 10.0f)]
    public float xOffset;
    [Range(-10.0f, 10.0f)]
    public float yOffset;
    [Range(-10.0f, 10.0f)]
    public float zOffset;
    [Header("Scale                    [0x    ->   10x]")]
    [Range(0.1f, 10.0f)]
    public float scale;
    [Header("Interpolation %   [0% -> 100%]")]
    [Range(0.0f, 100.0f)]
    public float Smoothing;

    private GameObject spriteObject;

	void Start ()
    {
        spriteObject = new GameObject();
        spriteObject.AddComponent<SpriteRenderer>();
        spriteObject.GetComponent<SpriteRenderer>().sprite = this.sprite;
        spriteObject.name = this.name + "_Sprite";
	}

	void Update ()
    {
        spriteObject.transform.position = Vector3.Lerp(
            spriteObject.transform.position,
            this.transform.position +
            new Vector3(xOffset, 0.0f, 0.0f) +
            new Vector3(0.0f, yOffset, 0.0f),
            1.0f - (Smoothing / 100f)
        );

        spriteObject.transform.position = new Vector3(
            spriteObject.transform.position.x,
            spriteObject.transform.position.y,
            zOffset
        );

        spriteObject.transform.localScale = new Vector3(scale, scale, scale);
	}
}
