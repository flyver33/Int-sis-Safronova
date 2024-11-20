namespace Lab2;

class Elevator(int floor, string id, string state) : IElevatable {

    private int currentFloor = floor;
    private HashSet<int> neededFloors = [];
    private int actionsCount = 0;
    private string id = id;
    private string state = state;

    public void GoDown() {
        currentFloor -= 1;

        actionsCount +=1;

        Console.WriteLine("Лифт " + id + " is going down on " + currentFloor.ToString() + " floor");

    }

    public void OpenDoors() {

        Console.WriteLine("Elevator " + id + " has opened doors on " + currentFloor.ToString() + " floor");

    }

    public void CloseDoors() {

        Console.WriteLine("Elevator " + id + " has closed doors on " + currentFloor.ToString() + " floor");

    }

    public void GoUp() {
        currentFloor += 1;

        actionsCount +=1;

        Console.WriteLine("Elevator " + id + " is going up on " + currentFloor.ToString() + " floor");

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

    public string GetClosestFloor() {
        try {
            return neededFloors.MinBy(x => Math.Abs((long) x - currentFloor)).ToString();
        }
        catch(Exception) {
            return "_";
        }
        
    }

    public string GetID() {
        return id;
    }

    
} 
