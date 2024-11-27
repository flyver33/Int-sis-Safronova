using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Lab2;

class ElevatorLogic() : ILogicable
{
    private TakenCallsManager callsManager = new();
    public void dn(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        elevator.GoDown();
    }

    public void updns(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        elevator.OpenDoors();

        callsManager.AddRangeTaken(awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()), elevator.GetTaken());
        callsManager.AddRangeTaken(callsManager.FindAllOperated(x => x.Item1 == elevator.GetCurrentFloor(), elevator.GetOperated()), elevator.GetTaken());

        awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()).ForEach(floors => {
            elevator.AddFloor(floors.Item2);
        });

        callsManager.FindAllOperated(x => x.Item1 == elevator.GetCurrentFloor(), elevator.GetOperated()).ForEach(floors => {
            elevator.AddFloor(floors.Item2);
        });
        
        int passengersTaken = awaitingCalls.RemoveAll(x => x.Item1 == elevator.GetCurrentFloor());
        passengersTaken += callsManager.RemoveAllOperated(x => x.Item1 == elevator.GetCurrentFloor(), elevator.GetOperated());

        int passengersDroppedOff = callsManager.RemoveAllTaken(x => x.Item2 == elevator.GetCurrentFloor(), elevator.GetTaken());

        Trace.TraceInformation("Elevator " + elevator.GetID() + " has dropped off " + passengersDroppedOff + " passengers");
        Trace.TraceInformation("Elevator " + elevator.GetID() + " has taken " + passengersTaken + " passengers");

        elevator.RemoveFloor(elevator.GetCurrentFloor());

        floorsButtons[elevator.GetCurrentFloor()] = "_";

        elevator.CloseDoors();
    }

    public void dns(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        updns(awaitingCalls, elevator, floorsButtons);
    }

    public void ss(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons) {
        string curButton = floorsButtons[elevator.GetCurrentFloor()];

        elevator.SetDirNo();
        GetType().GetMethod("ss" + curButton)?.Invoke(this, [awaitingCalls, elevator, floorsButtons]);
    }

    public void ssupdn(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        callsManager.AddRangeTaken(callsManager.FindAllOperated(x => x.Item1 == elevator.GetCurrentFloor(), elevator.GetOperated()), elevator.GetTaken());
        callsManager.AddRangeTaken(awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()), elevator.GetTaken());

        callsManager.FindAllOperated(x => x.Item1 == elevator.GetCurrentFloor(), elevator.GetOperated()).ForEach(floors => {
            elevator.AddFloor(floors.Item2);
        });

        awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()).ForEach(floors => {
            elevator.AddFloor(floors.Item2);
        });

        elevator.OpenDoors();
        int passengersTaken = callsManager.RemoveAllOperated(x => x.Item1 == elevator.GetCurrentFloor(), elevator.GetOperated());
        passengersTaken += awaitingCalls.RemoveAll(x => x.Item1 == elevator.GetCurrentFloor());
        Trace.TraceInformation("Elevator " + elevator.GetID() + " has taken " + passengersTaken + " passengers");

        int passengersDroppedOff = callsManager.RemoveAllTaken(x => x.Item2 == elevator.GetCurrentFloor(), elevator.GetTaken());
        Trace.TraceInformation("Elevator " + elevator.GetID() + " has dropped off " + passengersDroppedOff + " passengers");

        elevator.RemoveFloor(elevator.GetCurrentFloor());

        elevator.CloseDoors();
        floorsButtons[elevator.GetCurrentFloor()] = "_";
    }

    public void up(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        elevator.GoUp();
    }

    public void ups(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        updns(awaitingCalls, elevator, floorsButtons);
    }

    public void ssup(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        ssupdn(awaitingCalls, elevator, floorsButtons);
        up(awaitingCalls, elevator, floorsButtons);
    }

    public void ssdn(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        ssupdn(awaitingCalls, elevator, floorsButtons);
        dn(awaitingCalls, elevator, floorsButtons);
    }

    public void ms(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons) {
        GetType().GetMethod(elevator.GetDir())?.Invoke(this, [awaitingCalls, elevator, floorsButtons]);
        GetType().GetMethod(elevator.GetDir() + "s")?.Invoke(this, [awaitingCalls, elevator, floorsButtons]);
    }
}