using Lab2;

class Elevator : IElevatable {

    private int floor;
    private List<int> neededFloors = new List<int>();
    private bool isGoing = false;
    private bool isOpened = false;

    public Elevator() {

    }

    public void GoDown() {
        try {

            if (floor == 1) {
                throw new Exception();
            }

            else {
                floor -= 1;
            }

        }
        catch (Exception e) {
            Console.WriteLine("Лифт не может спуститься ниже, он на нижнем этаже");

        }

    }

    public void OpenDoors() {
        try {

            if (isGoing) {
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

            if (floor == maxFloor) {
                throw new Exception();
            }

            else {
                floor += 1;
            }

        }
        catch (Exception e) {
            Console.WriteLine("Лифт не может подняться выше, он на верхнем этаже");

        }

    }
}