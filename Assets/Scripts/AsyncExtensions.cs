using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public static class AsyncExtensions {
    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp) {
        var tcs = new TaskCompletionSource<Task>();
        asyncOp.completed += _ => tcs.SetResult(null);
        return ((Task)tcs.Task).GetAwaiter();
    }

    // public static TaskAwaiter GetAwaiter(this SceneEventProgressStatus status) {
    //     return Task.Run(() => {
    //         while(status != SceneEventProgressStatus.None){
    //             Task.Yield();
    //         }
    //     }).GetAwaiter();
    // }

    // public static TaskAwaiter GetAwaiter(this SceneSwitchProgress ssp){
    //     var tcs = new TaskCompletionSource<SceneSwitchProgress>();
    //     ssp.OnComplete += _ => tcs.SetResult(null);
    //     return ((Task)tcs.Task).GetAwaiter();
    // }
}