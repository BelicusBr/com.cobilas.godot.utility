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
        private CoroutineItem[] f_waits;

        private static CoroutineManager _Coroutine;
        private static readonly char[] chars = { 'a', 'b', 'c', 'd', 'e', 'f', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public override void _Ready() {
            if (_Coroutine == null) {
                _Coroutine = this;
            }
        }

        public override void _Process(float delta) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(waits); I++)
                if (!waits[I].Run()) {
                    ArrayManipulation.Remove(I, ref waits);
                    --I;
                }
        }

        public override void _PhysicsProcess(float delta) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(f_waits); I++)
                if (!f_waits[I].Run()) {
                    ArrayManipulation.Remove(I, ref f_waits);
                    --I;
                }
        }

        public static Coroutine StartCoroutine(IEnumerator enumerator) {
            IYieldCoroutine wait = (IYieldCoroutine)enumerator.Current;
            Coroutine Coroutine = new Coroutine(enumerator, GenID());

            if (wait is IYieldUpdate)
                ArrayManipulation.Add(new CoroutineItem(Coroutine), ref _Coroutine.waits);
            else if (wait is IYieldFixedUpdate)
                ArrayManipulation.Add(new CoroutineItem(Coroutine), ref _Coroutine.f_waits);
            else if (wait is IYieldVolatile @volatile) {
                if (@volatile.IsPhysicsProcess)
                    ArrayManipulation.Add(new CoroutineItem(Coroutine), ref _Coroutine.f_waits);
                else ArrayManipulation.Add(new CoroutineItem(Coroutine), ref _Coroutine.waits);
            } else ArrayManipulation.Add(new CoroutineItem(Coroutine), ref _Coroutine.waits);

            return Coroutine;
        }

        public static void StopCoroutine(Coroutine Coroutine) {
            foreach (var item in ArrayManipulation.Add(_Coroutine.f_waits, _Coroutine.waits))
                if (item.ID == Coroutine.ID) {
                    item.Cancel();
                    break;
                }
        }

        public static void StopAllCoroutines() {
            foreach (var item in ArrayManipulation.Add(_Coroutine.f_waits, _Coroutine.waits))
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
            private Coroutine Coroutine;

            public string ID => Coroutine.ID;

            public CoroutineItem(Coroutine Coroutine) {
                this.Coroutine = Coroutine;
                time = DateTime.Now;
            }

            public void Cancel()
                => Coroutine.Cancel();

            public bool Run() {
                if (Coroutine.IsCancellationRequested) {
                    Coroutine.SetStatus(false);
                    return Coroutine.IsRunning;
                }
                bool res = true;
                IEnumerator enumerator = (Coroutine as IEnumerable).GetEnumerator();
                TimeSpan delay = !(enumerator.Current is IYieldCoroutine wait) ? TimeSpan.Zero : wait.Delay;
                if (!init) {
                    res = enumerator.MoveNext();
                    init = true;
                } else if (DateTime.Now > time + delay)
                    if (res = enumerator.MoveNext())
                        time = DateTime.Now;
                Coroutine.SetStatus(res);
                return res;
            }
        }
    }
}