using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameVisualizer : MonoBehaviour
{
    [SerializeField] private Renderer head;
    [SerializeField] private Renderer body;
    [SerializeField] private Renderer leg;
    [SerializeField] private Renderer backpack;
    [SerializeField] private Renderer assaultRifle;
    [SerializeField] private Renderer arm;

    [SerializeField] Color dameColor;
    [SerializeField] private float blinkSpeed;
    [SerializeField] string emissionAddColor = "_Addition";

    Color originalColor;

    private void Start()
    {
        //Material mat = mesh.material;
        //mesh.material = new Material(mat);

        originalColor = head.material.GetColor(emissionAddColor);
    }

    public void BlinkColor()
    {
        Color currentColor = head.material.GetColor(emissionAddColor);
        if(Mathf.Abs((currentColor - originalColor).grayscale) < 0.1f)
        {
            head.material.SetColor(emissionAddColor, dameColor);
            body.material.SetColor (emissionAddColor, dameColor);
            leg.material.SetColor(emissionAddColor, dameColor);
            backpack.material.SetColor(emissionAddColor, dameColor);
            arm.material.SetColor(emissionAddColor, dameColor);
            assaultRifle.material.SetColor(emissionAddColor, dameColor);
        }
    }

    private void Update()
    {
        Color currentColor = head.material.GetColor(emissionAddColor);
        Color newColor = Color.Lerp(currentColor, originalColor, blinkSpeed*Time.deltaTime);
        head.material.SetColor(emissionAddColor , newColor);
        body.material.SetColor(emissionAddColor, newColor);
        leg.material.SetColor(emissionAddColor, newColor);
        backpack.material.SetColor(emissionAddColor, newColor);
        arm.material.SetColor(emissionAddColor, newColor);
        assaultRifle.material.SetColor(emissionAddColor, newColor);
    }
}
