using System.Runtime.InteropServices;

namespace Lab2;

class ElevatorLogic() : ILogicable
{
    public void dn(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        elevator.GoDown();
    }

    public void updns(List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons)
    {
        elevator.OpenDoors();

        elevator.AddRangeTaken(awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()));
        elevator.AddRangeTaken(elevator.FindAllOperated(x => x.Item1 == elevator.GetCurrentFloor()));

        foreach((int, int) floors in awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor())) {
            elevator.AddFloor(floors.Item2);
        }

        foreach((int, int) floors in elevator.FindAllOperated(x => x.Item1 == elevator.GetCurrentFloor())) {
            elevator.AddFloor(floors.Item2);
        }
        
        int passengersTaken = awaitingCalls.RemoveAll(x => x.Item1 == elevator.GetCurrentFloor());
        passengersTaken += elevator.RemoveAllOperated(x => x.Item1 == elevator.GetCurrentFloor());

        int passengersDroppedOff = elevator.RemoveAllTaken(x => x.Item2 == elevator.GetCurrentFloor());

        Console.WriteLine("Elevator " + elevator.GetID() + " has dropped off " + passengersDroppedOff + " passengers");
        Console.WriteLine("Elevator " + elevator.GetID() + " has taken " + passengersTaken + " passengers");

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
        elevator.AddRangeTaken(elevator.FindAllOperated(x => x.Item1 == elevator.GetCurrentFloor()));
        elevator.AddRangeTaken(awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor()));

        foreach((int, int) floors in elevator.FindAllOperated(x => x.Item1 == elevator.GetCurrentFloor())) {
            elevator.AddFloor(floors.Item2);
        }

        foreach((int, int) floors in awaitingCalls.FindAll(x => x.Item1 == elevator.GetCurrentFloor())) {
            elevator.AddFloor(floors.Item2);
        }

        elevator.OpenDoors();
        int passengersTaken = elevator.RemoveAllOperated(x => x.Item1 == elevator.GetCurrentFloor());
        passengersTaken += awaitingCalls.RemoveAll(x => x.Item1 == elevator.GetCurrentFloor());
        Console.WriteLine("Elevator " + elevator.GetID() + " has taken " + passengersTaken + " passengers");

        int passengersDroppedOff = elevator.RemoveAllTaken(x => x.Item2 == elevator.GetCurrentFloor());
        Console.WriteLine("Elevator " + elevator.GetID() + " has dropped off " + passengersDroppedOff + " passengers");

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