using Lab2;

class Elevator : IElevatable {

    private int currentFloor;
    private List<int> neededFloors = new List<int>();
    private bool isMoving = false;
    private bool isOpened = false;

    public Elevator() {

    }

    public void GoDown() {
        try {

            if (currentFloor == 1) {
                throw new Exception();
            }

            else {
                currentFloor -= 1;
            }

        }
        catch (Exception e) {
            Console.WriteLine("Лифт не может спуститься ниже, он на нижнем этаже");

        }

    }

    public void OpenDoors() {
        try {

            if (isMoving) {
                throw new Exception();
            }

            else {
                isOpened = true;
            }

        }
        catch (Exception e) {
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
        catch (Exception e) {
            Console.WriteLine("Лифт не может подняться выше, он на верхнем этаже");

        }

    }

    public bool getIsGoing() {
        return neededFloors.Count != 0;
    }

    public void takePassengers(int targetFloor) {
        neededFloors.Add(targetFloor);
    }

    public void dropPassengers(int targetFloor) {
        neededFloors.Remove(targetFloor);
    }

    public List<int> getFloors() {
        return neededFloors;
    }

    public int getCurrentFloor() {
        return currentFloor;
    } 
    public bool isInRange(int targetFloor) {

        bool isInRangeUp = targetFloor > currentFloor && targetFloor <= neededFloors.Max();
        bool isInRangeDown = targetFloor < currentFloor && targetFloor >= neededFloors.Min();
        
        return isInRangeUp || isInRangeDown;
    }
}
