using Godot;
using System;
using System.Text;
using System.Collections;
using Cobilas.Collections;
using Cobilas.GodotEngine.Utility.Runtime;

namespace Cobilas.GodotEngine.Utility {
    [RunTimeInitializationClass(nameof(CoroutineManager))]
    public class CoroutineManager : Node {
        private CoroutineItem[] waits;

        private static CoroutineManager _Coroutine;
        private static readonly char[] chars = { 'a', 'b', 'c', 'd', 'e', 'f', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public override void _Ready() {
            if (_Coroutine == null) {
                _Coroutine = this;
            }
        }

        public override void _Process(float delta) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++)
                if (!waits[I].IsPhysicsProcess)
                    if (!waits[I].Run()) {
                        ArrayManipulation.Remove(I, ref waits);
                        --I;
                    }
        }

        public override void _PhysicsProcess(float delta) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++)
                if (waits[I].IsPhysicsProcess)
                    if (!waits[I].Run()) {
                        ArrayManipulation.Remove(I, ref waits);
                        --I;
                    }
        }

        public static Coroutine StartCoroutine(IEnumerator enumerator) {
            Coroutine Coroutine = new Coroutine(enumerator, GenID());

            ArrayManipulation.Add(new CoroutineItem(Coroutine), ref _Coroutine.waits);

            return Coroutine;
        }

        public static void StopCoroutine(Coroutine Coroutine) {
            foreach (var item in _Coroutine.waits)
                if (item.ID == Coroutine.ID) {
                    item.Cancel();
                    break;
                }
        }

        public static void StopAllCoroutines() {
            foreach (var item in _Coroutine.waits)
                item.Cancel();
        }

        public static string GenID() {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int I = 0; I < 64; I++)
                builder.Append(random.Next(0, 50) > 25 ? char.ToUpper(chars[random.Next(0, 15)]) : chars[random.Next(0, 15)]);
            return builder.ToString();
        }

        private sealed class CoroutineItem {
            private bool init;
            private DateTime time;
            private Coroutine coroutine;

            public string ID => coroutine.ID;
            public bool IsPhysicsProcess {
                get {
                    object obj = (coroutine as IEnumerable).GetEnumerator().Current;
                    return obj is IYieldFixedUpdate || (obj is IYieldVolatile @volatile && @volatile.IsPhysicsProcess);
                }
            }

            public CoroutineItem(Coroutine coroutine) {
                this.coroutine = coroutine;
                time = DateTime.Now;
            }

            public void Cancel()
                => coroutine.Cancel();

            public bool Run() {
                if (coroutine.IsCancellationRequested) {
                    coroutine.SetStatus(false);
                    return coroutine.IsRunning;
                }
                bool res = true;
                IEnumerator enumerator = (coroutine as IEnumerable).GetEnumerator();
                TimeSpan delay = !(enumerator.Current is IYieldCoroutine wait) ? TimeSpan.Zero : wait.Delay;
                if (!init) {
                    res = enumerator.MoveNext();
                    init = true;
                } else if (DateTime.Now > time + delay)
                    if (res = enumerator.MoveNext())
                        time = DateTime.Now;
                coroutine.SetStatus(res);
                return res;
            }
        }
    }
}