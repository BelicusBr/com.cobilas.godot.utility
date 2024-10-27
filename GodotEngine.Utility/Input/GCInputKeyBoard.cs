using Godot;
using System;
using System.Collections.Generic;

namespace Cobilas.GodotEngine.Utility.Input;

internal class GCInputKeyBoard : Node {
    public enum GCStatus : byte {
        Standby = 0,
        Process = 1,
        PhyProcess = 2
    }

    public event Action? GCEvent;
    public GCStatus status = GCStatus.Standby;
    public List<PeripheralItem> periferics = [];

    public override void _Process(float delta) {
        if (status == GCStatus.Standby) status = GCStatus.Process;
        if (status == GCStatus.Process) {
            for (int I = 0; I < periferics.Count; I++) {
                PeripheralItem item = periferics[I];
                if (item.OnDestroy) item.Status = KeyStatus.None;
                else if (item.Status == KeyStatus.Down) item.Status = KeyStatus.Press;
                periferics[I] = item;
            }
            GCEvent?.Invoke();
            status = GCStatus.Standby;
        }
    }

    public override void _PhysicsProcess(float delta) {
        if (status == GCStatus.Standby) status = GCStatus.PhyProcess;
        if (status == GCStatus.PhyProcess) {
            for (int I = 0; I < periferics.Count; I++) {
                PeripheralItem item = periferics[I];
                if (item.OnDestroy) item.Status = KeyStatus.None;
                else if (item.Status == KeyStatus.Down) item.Status = KeyStatus.Press;
                periferics[I] = item;
            }
            GCEvent?.Invoke();
            status = GCStatus.Standby;
        }
    }
}
