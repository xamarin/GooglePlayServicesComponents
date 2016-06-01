using System;
using Android.Runtime;

namespace Android.Gms.Tasks
{
    public abstract partial class Task<TResult> : Task where TResult : Java.Lang.Object
    {
        public TResult Result { get { return this.RawResult.JavaCast<TResult>(); } }

        System.Threading.Tasks.TaskCompletionSource<TResult> tcs;
        InternalTaskCompletionListener completeListener;

        public Task () : base ()
        {
            init();
        }

        protected Task (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) 
        {
            init();
        }

        void init ()
        {
            tcs = new System.Threading.Tasks.TaskCompletionSource<TResult> ();

            completeListener = new InternalTaskCompletionListener
            {
                OnCompleteHandler = completedTask => {
                    if (this.IsSuccessful)
                        tcs.SetResult(this.Result);
                    else
                        tcs.SetException(this.Exception);
                }
            };
            this.AddOnCompleteListener(completeListener);
        }

        public System.Runtime.CompilerServices.TaskAwaiter<TResult> GetAwaiter ()
        {
            return tcs.Task.GetAwaiter ();
        }
    }

    internal class InternalTaskCompletionListener : Java.Lang.Object, IOnCompleteListener
    {
        public Action<Task> OnCompleteHandler { get; set; }

        public void OnComplete (Task task)
        {
            if (OnCompleteHandler != null)
                OnCompleteHandler (task);
        }
    }
}
