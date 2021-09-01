using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class CollectMeat : MonoBehaviour
{
    public AudioSource munch;
    public Text txtScore;
   void Start()
   {
        Text txtScore = GameObject.Find("Canvas/Score").GetComponent<Text>();
   }
    void OnTriggerEnter(Collider other)
    {
            int init = int.Parse(txtScore.text);
            munch.Play();
            this.gameObject.SetActive(false);
            init = init + 5;
            txtScore.text = "" + init;
    }
    private readonly float FullRotation = 360f;
        [SerializeField]
        private float speed = 1f;
        private float currentRotation = 0f;

        public event EventHandler FullyRotated;
        private bool IsFullRotation
        {
            get { return Mathf.Abs(currentRotation) >= FullRotation; }
        } 

    // Update is called once per frame
    void Update()
    {
        Rotate();
        RaiseEventOnFullRotation();
        ResetRotationOnFullRotation();
    }
    private void Rotate()
    {
        transform.Rotate(0, speed, 0);
        currentRotation += speed;
    }
    private void RaiseEventOnFullRotation()
    {
    if (IsFullRotation)
    {
        if (FullyRotated!=null)
        {
            FullyRotated(this, new EventArgs());
        }
    }
    }
    private void ResetRotationOnFullRotation()
    {
        if (IsFullRotation)
        {
        currentRotation -= FullRotation * Mathf.Sign(currentRotation);
        }
    }
}
