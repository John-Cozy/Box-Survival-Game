using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    public CanvasGroup Group;
    private bool HideGroup;
    private bool ShowGroup;

    private void FixedUpdate() {
        if (HideGroup) {
            if (Group.alpha >= 1) {
                Group.blocksRaycasts = false;
                Group.interactable = false;
                Group.alpha -= 0.05f;
            } else if (Group.alpha <= 0) {
                HideGroup = false;
            } else {
                Group.alpha -= 0.05f;
            }
        } else if (ShowGroup) {
            if (Group.alpha <= 0) {
                Group.blocksRaycasts = true;
                Group.interactable = true;
                Group.alpha += 0.05f;
            } else if (Group.alpha >= 1) {
                ShowGroup = false;
            } else {
                Group.alpha += 0.05f;
            }
        }
    }

    public void Show() {
        ShowGroup = true;
    }

    public void Hide() {
        HideGroup = true;
    }
}
