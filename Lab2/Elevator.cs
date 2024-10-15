namespace Lab2;

class Elevator(int floor) : IElevatable {

    private int currentFloor = floor;
    private HashSet<int> neededFloors = [];
    private bool isMoving = false;
    private bool isOpened = false;
    private bool isWorking = true;
    private int actionsCount = 0;

    public void GoDown() {
        try {

            if (currentFloor == 1) {
                throw new Exception();
            }

            else {
                currentFloor -= 1;
            }

        }
        catch (Exception)
        {
            Console.WriteLine("Лифт не может спуститься ниже, он на нижнем этаже");

        }

        actionsCount +=1;

    }

    public void OpenDoors() {
        try
        {

            if (isMoving) {
                throw new Exception();
            }

            else {
                isOpened = true;
            }

        }
        catch (Exception)
        {
            Console.WriteLine("Лифт не может открыть двери в движении");

        }

    }

    public void CloseDoors() {
        isOpened = false;

    }

    public void GoUp(int maxFloor) {
        try {

            if (currentFloor == maxFloor) {
                throw new Exception();
            }

            else {
                currentFloor += 1;
            }

        }
        catch (Exception) {
            Console.WriteLine("Лифт не может подняться выше, он на верхнем этаже");

        }

        actionsCount +=1;

    }

    public bool GetIsGoing() {
        return neededFloors.Count != 0;
    }

    public void AddFloor(int floor) {
        if (floor != currentFloor) {
            neededFloors.Add(floor);
        }
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
    public bool IsInRange(int floor) {

        bool isInRangeUp = floor >= currentFloor && floor <= neededFloors.Max();
        bool isInRangeDown = floor <= currentFloor && floor >= neededFloors.Min();
        
        return isInRangeUp || isInRangeDown;
    }

    public bool IsPickUp(int callFloor, int targetFloor) {

        if (GetIsGoing()) {

            if (IsInRange(callFloor)) {
                
                if (IsInRange(targetFloor)) {
                    return true;
                }
            }
            return false;
        }

        return true;
    }

    public void Start(int maxFloor) {

        while (isWorking) {

            if (isOpened) {

                if (neededFloors.Contains(currentFloor)) {

                    RemoveFloor(currentFloor);
                }
                else {

                    AddFloor(currentFloor);
                }

                CloseDoors();
                isMoving = true;
            }

            else {
                if (neededFloors.Contains(currentFloor)) {

                    OpenDoors();
                    isMoving = false;
                }
                else {
                    if (neededFloors.Min() > currentFloor) {

                        GoUp(maxFloor);
                    }
                    else if (neededFloors.Max() < currentFloor) {

                        GoDown();
                    }
                }
            }
        }
    }
} 
