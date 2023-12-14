﻿namespace MPowerKit.Popups.Interfaces;

public interface IPopupAnimation
{
    void Preparing(View content, PopupPage page);
    void Disposing(View content, PopupPage page);
    Task Appearing(View content, PopupPage page);
    Task Disappearing(View content, PopupPage page);
}