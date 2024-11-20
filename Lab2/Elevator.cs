namespace Lab2;

class Elevator(int floor, string id) : IElevatable {

    private int currentFloor = floor;
    private HashSet<int> neededFloors = [];
    private int actionsCount = 0;
    private readonly string id = id;
    private string state = id + "ss";
    private string direction = "_";

    public void GoDown() {
        currentFloor -= 1;

        actionsCount +=1;

        Console.WriteLine("Лифт " + id + " is going down on " + currentFloor.ToString() + " floor");

        SetDirDn();

    }

    public void OpenDoors() {

        Console.WriteLine("Elevator " + id + " has opened doors on " + currentFloor.ToString() + " floor");

    }

    public void CloseDoors() {

        Console.WriteLine("Elevator " + id + " has closed doors on " + currentFloor.ToString() + " floor");

        SetDirNo();

    }

    public void GoUp() {
        currentFloor += 1;

        actionsCount +=1;

        Console.WriteLine("Elevator " + id + " is going up on " + currentFloor.ToString() + " floor");

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

    public string GetClosestFloor() {

        Dictionary<string, HashSet<int>> sortedFloors = new();

        sortedFloors["up"] = neededFloors.Where(x => x >= currentFloor).ToHashSet<int>();
        sortedFloors["dn"] = neededFloors.Where(x => x <= currentFloor).ToHashSet<int>();
        sortedFloors["_"] = neededFloors;

        try {
            return sortedFloors[direction].MinBy(x => Math.Abs((long) x - currentFloor)).ToString();
        }
        catch(Exception) {
            return "_";
        }
        
    }

    public string GetID() {
        return id;
    }

    public void SetDirUp() {
        direction = "up";
    }

    public void SetDirDn() {
        direction = "dn";
    }

    public void SetDirNo() {
        direction = "_";
    }

    
} 
