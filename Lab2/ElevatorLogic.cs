using System.Runtime.InteropServices;

namespace Lab2;

class ElevatorLogic() : ILogicable
{
    public void down(IElevatable elevator)
    {
        elevator.GoDown();
    }

    public void updowns(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
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

    public void downs(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        updowns(operatedCalls, awaitingCalls, takenCalls, elevator, floorsButtons);
    }

    public void ss_(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, IElevatable elevator)
    {
        operatedCalls.AddRange(awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()));
        awaitingCalls.RemoveAll(x => x.Item1 == elevator.GetCurrentFloor());

        elevator.AddFloor(1);
    }

    public void ssdownup(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        throw new NotImplementedException();
    }

    public void up(IElevatable elevator)
    {
        elevator.GoUp();
    }

    public void ups(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        updowns(operatedCalls, awaitingCalls, takenCalls, elevator, floorsButtons);
    }
}