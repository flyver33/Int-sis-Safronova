using System.Globalization;

namespace Lab2;

class MyOperator {
    
    private List<IElevatable> elevators = [];
    private int maxFloor;
    private Queue<(int, int)> calls;
    private Dictionary<string, Dictionary<string, string>> fSM = new();
    private ILogicable logic = new ElevatorLogic();

    private Dictionary<int, string> GetInitialState(int maxFloor) {
        Dictionary<int, string> getter = new();
        getter.Add(1, 1.ToString() + "m");
        getter.Add(maxFloor, maxFloor.ToString() + "m");
        for (int floor = 2; floor < maxFloor; floor ++) {
            getter.Add(floor, floor.ToString() + "ss");
        }
        return getter;
    }
    
    public MyOperator(int maxFloor, List<int> currentFloors, Queue<(int, int)> calls, int numElevs) {

        this.maxFloor = maxFloor;
        this.calls = calls;

        Dictionary<int, string> getterInitState = GetInitialState(maxFloor);
        

        for (int i = 0; i < numElevs; i++) {
            elevators.Add(new Elevator(currentFloors[i], (i+1).ToString(), getterInitState[currentFloors[i]]));
        }

        GenerateFSM();
        Start();

    }

    private void AddMaxMinFloorState(int floor, int nextFloor, string direction) {
        fSM.Add(floor.ToString() + "m", new Dictionary<string, string>());
        fSM[floor.ToString() + "m"].Add("__", floor.ToString() + "m");
        fSM[floor.ToString() + "m"].Add("_" + direction, nextFloor.ToString() + direction);

        foreach (string dir in new List<string>(){"_", direction}) {
            for (int fl = 1; fl < floor; fl ++) {
                fSM[floor.ToString() + "m"].Add(fl.ToString() + dir, nextFloor.ToString() + direction);
            }
        }

        foreach (string dir in new List<string>(){"_", direction}) {
            for (int fl = floor; fl < maxFloor; fl ++) {
                fSM[floor.ToString() + "m"].Add(fl.ToString() + dir, nextFloor.ToString() + direction);
            }
        }

    }

    private void AddUpDownState() {
        
        for (int floor = 2; floor < maxFloor - 1; floor ++) {

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                fSM[floor.ToString() + "up"].Add(floor.ToString() + dir, floor.ToString() + "up" + "s");
            }

            for (int fl = floor + 1; fl < maxFloor; fl ++) {
                foreach (string dir in new List<string>(){"_", "dn"}) {
                    fSM[floor.ToString() + "up"].Add(fl.ToString() + dir, (floor + 1).ToString() + "up");
                }
                fSM[floor.ToString() + "up"].Add(fl.ToString() + "up", floor.ToString() + "up" + "s");
            }
        }

        foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                fSM[(maxFloor - 1).ToString() + "up"].Add(maxFloor.ToString() + dir, maxFloor.ToString() + "m");
            }

        for (int floor = 2; floor < maxFloor - 1; floor ++) {

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                fSM[floor.ToString() + "dn"].Add(floor.ToString() + dir, floor.ToString() + "dn" + "s");
            }

            for (int fl = 1; fl < floor; fl ++) {
                foreach (string dir in new List<string>(){"_", "up"}) {
                    fSM[floor.ToString() + "dn"].Add(fl.ToString() + dir, (floor - 1).ToString() + "dn");
                }
                fSM[floor.ToString() + "dn"].Add(fl.ToString() + "dn", floor.ToString() + "dn" + "s");
            }
        }

        foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                fSM[(maxFloor - 1).ToString() + "dn"].Add(maxFloor.ToString() + dir, 1.ToString() + "m");
            }

    }

    private void AddUpDownStillState(int dirFloor, string direction) {
        
        for (int floor = 2; floor < dirFloor; floor ++) {

            int minv = Math.Min(dirFloor, floor);
            int maxv = Math.Max(dirFloor, floor);
            
            fSM[floor.ToString() + direction + "s"].Add("__", floor.ToString() + "ss");

            foreach (string dir in new List<string>(){ "up", "dn"}) {
                fSM[floor.ToString() + direction + "s"].Add("_" + dir, floor.ToString() + dir);
            }

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                for (int fl = minv + 1; fl < maxv - 1; fl ++) {
                    fSM[floor.ToString() + direction + "s"].Add(fl.ToString() + dir, floor.ToString() + direction);
                }
            }
        }

    }

    private void AddStillStillState(int maxFloor) {
        for (int floor = 2; floor < maxFloor; floor ++) {
            fSM[floor.ToString() + "ss"].Add("__", floor.ToString() + "ss");

            foreach (string dir in new List<string>(){ "up", "dn"}) {
                fSM[floor.ToString() + "ss"].Add("_" + dir, floor.ToString() + dir);
            }

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                for (int fl = floor + 1; fl <= maxFloor; fl ++) {
                    fSM[floor.ToString() + "ss"].Add(fl.ToString() + dir, floor.ToString() + "up");
                }
            }

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                for (int fl = floor; fl > 1; fl --) {
                    fSM[floor.ToString() + "ss"].Add(fl.ToString() + dir, floor.ToString() + "dn");
                }
            }
        }
    }
    

    private void GenerateFSM() {
        AddMaxMinFloorState(maxFloor, maxFloor-1, "dn");
        AddMaxMinFloorState(1, 2, "up");

        for (int floor = 2; floor < maxFloor; floor ++) {
            foreach (string dir in new List<string>(){"ss", "up", "dn", "ups", "dns"}) {
                fSM.Add(floor.ToString() + dir, new Dictionary<string, string>());
            }
        }

        AddUpDownState();

        AddUpDownStillState(1, "dn");
        AddUpDownStillState(maxFloor, "up");

        AddStillStillState(maxFloor);
    }

    public Dictionary<int, string> GenerateFloorsButtons() {
        Dictionary<int, string> floorsButtons = new Dictionary<int, string>();

        for(int i = 1; i <= maxFloor; i++) {
            floorsButtons[i] = "_";
        }

        return floorsButtons;
    }

    public Dictionary<int, Dictionary<int, string>> GenerateDirections() {
        Dictionary<int, Dictionary<int, string>> directions = new();

        for (int floor = 1; floor <= maxFloor; floor ++) {
            directions.Add(floor, new Dictionary<int, string>());
            for (int fl = 1; fl <= floor; fl ++) {
                directions[floor].Add(fl, "dn");
            }
            for (int fl = floor + 1; fl <= maxFloor; fl ++) {
                directions[floor].Add(fl, "up");
            }
        }

        return directions;
    }

    private bool getCalls() {
        bool calls = false;

        foreach(IElevatable elevator in elevators) {
            calls = calls || elevator.GetOperated().Count > 0;
            calls = calls || elevator.GetTaken().Count > 0;
        }

        return calls;
    }


    public void Start() {
        List<(int, int)> awaitingCalls = new List<(int, int)>(); // просто нажалась кнопка

        Dictionary<int, string> floorsButtons = GenerateFloorsButtons();
        Dictionary<int, Dictionary<int, string>> directions = GenerateDirections();

        IElevatorsCallsManager manager = new CallsManagerClosest();

        try {
            floorsButtons[calls.Peek().Item1] = directions[calls.Peek().Item1][calls.Peek().Item2];
            awaitingCalls.Add(calls.Dequeue());
        } catch (InvalidOperationException) {}

        manager.ManageCalls(awaitingCalls, elevators);

        foreach (IElevatable elevator in elevators) {
            typeof(ElevatorLogic).GetMethod(elevator.GetState()[1..])?.Invoke(logic, [awaitingCalls, elevator, floorsButtons]);
        }

        foreach (IElevatable elevator in elevators) {
                string curState = elevator.GetState();
                string action = elevator.GetClosestFloor() + floorsButtons[elevator.GetCurrentFloor()];
                string nextState = fSM[curState][action];
                
                elevator.SetState(nextState);

        }

        while (calls.Count > 0 || getCalls() || awaitingCalls.Count > 0) {

            try {
                floorsButtons[calls.Peek().Item1] = directions[calls.Peek().Item1][calls.Peek().Item2];
                awaitingCalls.Add(calls.Dequeue());
            } catch (InvalidOperationException) {}

            manager.ManageCalls(awaitingCalls, elevators);

            foreach (IElevatable elevator in elevators) {
                string curState = elevator.GetState();
                string action = elevator.GetClosestFloor() + floorsButtons[elevator.GetCurrentFloor()];
                string nextState = fSM[curState][action];
                
                elevator.SetState(nextState);
                typeof(ElevatorLogic).GetMethod(elevator.GetState()[1..])?.Invoke(logic, [awaitingCalls, elevator, floorsButtons]);
            }

        }

    }

}
