using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnBeatComp : BeatComponent
{
    // Start is called before the first frame update


    public float TimeToScale = 0.5f;

    public float Scale = 3.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnBeat()
    {
        StartCoroutine(ScaleUpAndDown());
    }

    private IEnumerator ScaleUpAndDown()
    {
        Vector3 initialScale = transform.localScale;

        for (float time = 0; time < TimeToScale * 2; time += Time.deltaTime)
        {
            float progress = Mathf.PingPong(time, TimeToScale) / TimeToScale;
            transform.localScale = Vector3.Lerp(initialScale, new Vector3(Scale, Scale, Scale), progress);
            yield return null;
        }
        transform.localScale = initialScale;
    }

}
