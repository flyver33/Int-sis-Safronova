using System.Runtime.InteropServices;

namespace Lab2;

class ElevatorLogic() : ILogicable
{
    public void dn(IElevatable elevator)
    {
        elevator.GoDown();
    }

    public void updns(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        elevator.OpenDoors();

        takenCalls.AddRange(awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()));
        takenCalls.AddRange(operatedCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()));
        
        int passengersTaken = awaitingCalls.RemoveAll(x => x.Item1 == elevator.GetCurrentFloor());
        passengersTaken += operatedCalls.RemoveAll(x => x.Item1 == elevator.GetCurrentFloor());

        int passengersDroppedOff = takenCalls.RemoveAll(x => x.Item2 == elevator.GetCurrentFloor());

        Console.WriteLine("Elevator " + elevator.GetID() + " has dropped off " + passengersDroppedOff + " passengers");
        Console.WriteLine("Elevator " + elevator.GetID() + " has taken " + passengersTaken + " passengers");

        foreach((int, int) floors in takenCalls) {
            elevator.AddFloor(floors.Item2);
        }

        elevator.RemoveFloor(elevator.GetCurrentFloor());

        floorsButtons[elevator.GetCurrentFloor()] = "_";

        elevator.CloseDoors();
    }

    public void dns(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        updns(operatedCalls, awaitingCalls, takenCalls, elevator, floorsButtons);
    }

    public void ss(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons) {
        string curButton = floorsButtons[elevator.GetCurrentFloor()];
        GetType().GetMethod("ss" + curButton)?.Invoke(this, [operatedCalls, takenCalls, awaitingCalls, elevator, floorsButtons]);
    }

    public void ssupdn(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        takenCalls.AddRange(operatedCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()));

        elevator.OpenDoors();
        int passengersTaken = operatedCalls.RemoveAll(x => x.Item1 == elevator.GetCurrentFloor());
        elevator.CloseDoors();
        floorsButtons[elevator.GetCurrentFloor()] = "_";
    }

    public void up(IElevatable elevator)
    {
        elevator.GoUp();
    }

    public void ups(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        updns(operatedCalls, awaitingCalls, takenCalls, elevator, floorsButtons);
    }

    public void ssup(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        ssupdn(operatedCalls, takenCalls, awaitingCalls, elevator, floorsButtons);
    }

    public void ssdn(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        ssupdn(operatedCalls, takenCalls, awaitingCalls, elevator, floorsButtons);
    }
}