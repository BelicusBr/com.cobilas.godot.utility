using Godot;
using System;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Runtime;
    /// <summary>Represents a list of <seealso cref="RunTimeInitialization"/> priorities.</summary>
    public struct PriorityList : IDisposable {
        private KeyValuePair<int, Node>[]? nodes;

        /// <summary>Adds items to the priority list.</summary>
        /// <param name="priority">Object execution priority.</param>
        /// <param name="node">The object to be added to the list.</param>
        /// <returns>The method will return a <seealso cref="PriorityList"/> object with its modified priority list.</returns>
        public PriorityList Add(int priority, Node node) {
            nodes ??= Array.Empty<KeyValuePair<int, Node>>();
            ArrayManipulation.Add(new KeyValuePair<int, Node>(priority, node), ref nodes);
            return this;
        }

        /// <summary>Execute your priority list.</summary>
        /// <param name="root">The parent node where nodes will be added to start their priority execution.</param>
        public readonly void Run(Node root) {
            if (!ArrayManipulation.EmpytArray(nodes))
                foreach (KeyValuePair<int, Node> item in nodes)
                    if (item.Value != null)
                        root.AddChild(item.Value);
        }

        /// <summary>Sort the priority list according to the priority of the list items.</summary>
        public void ReorderList() {
        if (ArrayManipulation.EmpytArray(nodes)) return;
            KeyValuePair<int, Node>[]? temp = ReorderList(nodes);
            ArrayManipulation.ClearArray(ref nodes);
            nodes = temp;
        }

        /// <inheritdoc/>
        public void Dispose() => ArrayManipulation.ClearArraySafe(ref nodes);

        private static KeyValuePair<int, Node>[]? ReorderList(KeyValuePair<int, Node>[]? list) {
            KeyValuePair<int, Node>[]? result = (KeyValuePair<int, Node>[]?)null;
            
            for (long A = 0; A < ArrayManipulation.ArrayLength(list); A++) {
                if (result is null) {
                    ArrayManipulation.Add(list[A], ref result);
                    continue;
                }
                bool addInResult = true;
                for (long B = 0; B < result.LongLength; B++)
                    if (list![A].Key < result[B].Key) {
                        ArrayManipulation.Insert(list[A], B, ref result);
                        addInResult = false;
                        break;
                    }
                if (addInResult) ArrayManipulation.Add(list![A], ref result);
            }
            return result;
        }
    }