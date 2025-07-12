using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSection : GameSectionBase<EndWidget>
{
    [SerializeField] private float waitDuration;
    private WaitForSeconds waitForSecondYeld;
    //in this section we only wait for some time then go back to the app start
    public override event Action<bool> OnSectionEnd;
    protected override void Start()
    {
        waitForSecondYeld = new WaitForSeconds(waitDuration);
    }
    public override void DisableSection()
    {
        sectionWidget.DisableSection();
    }

    public override void EnableSection()
    {
        sectionWidget.EnableSection();
        AudioManager.Instance.PlayGameEnd();
        StartWaitTimer();
    }
    private void StartWaitTimer()
    {
        StartCoroutine(WaitTimer());
    }
    private IEnumerator WaitTimer()
    {
        yield return waitForSecondYeld;
        OnSectionEnd?.Invoke(true);
    }
}
