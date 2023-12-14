﻿using MPowerKit.Popups.Animations.Base;

namespace MPowerKit.Popups.Animations;

public abstract class FadeBackgroundAnimation : BaseAnimation
{
    private Color? _backgroundColor;

    public bool HasBackgroundAnimation { get; set; } = true;

    public override void Preparing(View content, PopupPage page)
    {
        if (!HasBackgroundAnimation || page.BackgroundColor is null) return;

        _backgroundColor = page.BackgroundColor;

        page.BackgroundColor = GetColor(0);
    }

    public override void Disposing(View content, PopupPage page)
    {
        if (!HasBackgroundAnimation || _backgroundColor is null) return;

        page.BackgroundColor = _backgroundColor;
    }

    public override async Task Appearing(View content, PopupPage page)
    {
        if (!HasBackgroundAnimation || _backgroundColor is null) return;

        var tcs = new TaskCompletionSource();

        page.Animate("backgroundFade",
            d =>
            {
                page.BackgroundColor = GetColor((float)d);
            }, 0, _backgroundColor.Alpha, length: (uint)DurationIn.TotalMilliseconds,
            finished: (d, b) =>
            {
                tcs.SetResult();
            });

        await tcs.Task;
    }

    public override async Task Disappearing(View content, PopupPage page)
    {
        if (!HasBackgroundAnimation || page.BackgroundColor is null) return;

        var tcs = new TaskCompletionSource();

        _backgroundColor = page.BackgroundColor;

        page.Animate("backgroundFade",
            d =>
            {
                page.BackgroundColor = GetColor((float)d);
            }, _backgroundColor.Alpha, 0, length: (uint)DurationOut.TotalMilliseconds,
            finished: (d, b) =>
            {
                tcs.SetResult();
            });

        await tcs.Task;
    }

    private Color? GetColor(float transparent)
    {
        return _backgroundColor?.WithAlpha(transparent);
    }
}