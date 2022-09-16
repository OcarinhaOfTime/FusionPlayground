using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class AsyncTweener {
    public static async Task Tween(float d, Action<float> step_fn) {
        float t = 0;
        while (t < d && Application.isPlaying) {
            step_fn(t / d);
            t += Time.deltaTime;
            await Task.Yield();
        }
        step_fn(1);
    }

    public static async Task SmoothTween(float d, Action<float> step_fn) {
        float t = 0;
        while (t < d && Application.isPlaying) {
            step_fn(Smoothstep(t / d));
            t += Time.deltaTime;
            await Task.Yield();
        }
        step_fn(1);
    }

    public static async Task Wait(float d) {
        float t = 0;
        while (t < d && Application.isPlaying) {
            t += Time.deltaTime;
            await Task.Yield();
        }
    }

    public static async Task Wait(Func<bool> fn) {
        while (!fn() && Application.isPlaying) {
            await Task.Yield();
        }
    }

    public static async Task Until(Func<bool> fn, Action<float> until_fn) {
        float t = 0f;
        while (!fn() && Application.isPlaying) {
            await Task.Yield();
            t += Time.deltaTime;
            until_fn(t);
        }
    }

    public static float Smoothstep(float x) {
        return Smoothstep(0, 1, x);
    }

    public static float Smoothstep(float a, float b, float x) {
        float t = Mathf.Clamp01((x - a) / (b - a));
        return t * t * (3.0f - (2.0f * t));
    }
}
