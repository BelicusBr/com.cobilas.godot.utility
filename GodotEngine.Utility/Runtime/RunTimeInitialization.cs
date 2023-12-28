using Godot;
using System;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Runtime {
    public class RunTimeInitialization : Node {

        public const float DeltaTime = .33333333f;
        public const float FixedDeltaTime = .02f;

        public override void _Ready() {
            Type[] components = TypeUtilitarian.GetTypes();
            Dictionary<Priority, PriorityList> pairs = new Dictionary<Priority, PriorityList> {
                { Priority.StartBefore, new PriorityList() },
                { Priority.StartLater, new PriorityList() }
            };
            foreach (var item in components) {
                RunTimeInitializationClassAttribute attri = item.GetAttribute<RunTimeInitializationClassAttribute>();
                if (attri != null) {
                    Node node = item.Activator<Node>();
                    if (!string.IsNullOrEmpty(attri.ClassName))
                        node.Name = attri.ClassName;
                    pairs[attri.BootPriority] = pairs[attri.BootPriority].Add(attri.SubPriority, node);
                }
            }
            pairs[Priority.StartBefore].Run(this);
            pairs[Priority.StartLater].Run(this);
        }

        public struct PriorityList : IDisposable {
            private Node[] nodes;

            public PriorityList Add(int index, Node node) {
                if (nodes == null) nodes = ArrayManipulation.Empty<Node>();
                if (index > ArrayManipulation.ArrayLength(nodes))
                    ArrayManipulation.Resize<Node>(ref nodes, index);
                ArrayManipulation.Insert<Node>(node, index, ref nodes);
                return this;
            }

            public void Run(Node root) {
                if (!ArrayManipulation.EmpytArray(nodes))
                    foreach (var item in nodes)
                        if (item != null)
                            root.AddChild(item);
            }

            public void Dispose() {
                ArrayManipulation.ClearArraySafe(ref nodes);
            }
        }
    }
}