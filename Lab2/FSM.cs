namespace Lab2;

class FSM {
    
    private Elevator elevator1;
    private Elevator elevator2;
    private int maxFloor;
    private Queue<(int, int)> calls;
    private Queue<(int, int)> delayed;
    

    public FSM(int maxFloor, List<int> currentFloors, Queue<(int, int)> calls) {

        elevator1 = new Elevator(currentFloors[0]);
        elevator2 = new Elevator(currentFloors[1]);

        this.maxFloor = maxFloor;
        this.calls = calls;

        delayed = new Queue<(int, int)>();

        Start();

    }

    public (bool, bool) GetState(int callFloor, int targetFloor) {

        bool isPickUp1 = elevator1.IsPickUp(callFloor, targetFloor);
        bool isPickUp2 = elevator2.IsPickUp(callFloor, targetFloor);

        return (isPickUp1, isPickUp2);

    }

    public bool ChangeState(int callFloor, int targetFloor) {
        
        (bool isPickUp1, bool isPickUp2) = GetState(callFloor, targetFloor);

        if (!isPickUp1 && !isPickUp2) {
            return false;
        }

        else if (isPickUp1 && isPickUp2) {
            int dist1 = Math.Abs(elevator1.GetCurrentFloor() - callFloor);
            int dist2 = Math.Abs(elevator2.GetCurrentFloor() - callFloor);

            if (dist1 <= dist2) {
                elevator1.AddFloor(callFloor);
                elevator1.AddFloor(targetFloor);
            }

            else if (dist1 > dist2) {
                elevator2.AddFloor(callFloor);
                elevator2.AddFloor(targetFloor);
            }
        }

        else if (isPickUp1 && !isPickUp2) {
            elevator1.AddFloor(callFloor);
            elevator1.AddFloor(targetFloor);
        }

        else if (!isPickUp1 && isPickUp2) {
            elevator2.AddFloor(callFloor);
            elevator2.AddFloor(targetFloor);
        }

        return true;

    }

    public void Start() {
        List<(int, int)> waitingCalls = new List<(int, int)>();

        foreach ((int, int) floors in calls) {

            (int callFloor, int targetFloor) = floors;

            

            bool callProceed = ChangeState(callFloor, targetFloor);

            if (! callProceed) {
                delayed.Enqueue((callFloor, targetFloor));
            }

            calls.Dequeue();

            foreach ((int, int) delayedFloors in delayed) {

                (int delayedCallFloor, int delayedTargetFloor) = delayedFloors;

                bool delayedCallProceed = ChangeState(delayedCallFloor, delayedTargetFloor);

                if (!delayedCallProceed) {
                    delayed.Enqueue(delayed.Dequeue());
                }
                else {delayed.Dequeue();}
            }

        }

    }

}
