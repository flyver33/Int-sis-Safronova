using System.Diagnostics;

namespace Lab2;

class Elevator(int floor, string id, string initState) : IElevatable {

    private int currentFloor = floor;
    private HashSet<int> neededFloors = [];
    private int actionsCount = 0;
    private readonly string id = id;
    private string state = initState;
    private string direction = "_";
    private List<(int, int)> operatedCalls = []; // те вызовы, на которые лифты уже едут
    private List<(int, int)> takenCalls = []; // вызовы, которые лифты уже взяли

    public string GetDir() {
        return direction;
    }

    public List<(int, int)> GetTaken() {
        return takenCalls;
    }

    public List<(int, int)> GetOperated() {
        return operatedCalls;
    }

    public void GoDown() {
        currentFloor -= 1;

        actionsCount +=1;

        Trace.TraceInformation("Elevator " + id + " is going down on " + currentFloor.ToString() + " floor");

        SetDirDn();

    }

    public void OpenDoors() {

        Trace.TraceInformation("Elevator " + id + " has opened doors on " + currentFloor.ToString() + " floor");

    }

    public void CloseDoors() {

        Trace.TraceInformation("Elevator " + id + " has closed doors on " + currentFloor.ToString() + " floor");

    }

    public void GoUp() {
        currentFloor += 1;

        actionsCount +=1;

        Trace.TraceInformation("Elevator " + id + " is going up on " + currentFloor.ToString() + " floor");

        SetDirUp();

    }

    public void AddFloor(int floor) {
        neededFloors.Add(floor);
    }

    public void RemoveFloor(int floor) {
        neededFloors.Remove(floor);
    }

    public HashSet<int> GetFloors() {
        return neededFloors;
    }

    public int GetCurrentFloor() {
        return currentFloor;
    } 

    public int GetActionsCount() {
        return actionsCount;
    } 

    public void SetState(string nextState) {
        state = nextState;
    } 

    public string GetState() {
        return state;
    }

    public string GetID() {
        return id;
    }

    private void SetDirUp() {
        direction = "up";
    }

    private void SetDirDn() {
        direction = "dn";
    }

    public void SetDirNo() {
        direction = "_";
    }

    
} 
