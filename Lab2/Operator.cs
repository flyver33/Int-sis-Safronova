using System.Diagnostics;
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
        getter.Add(1, 1.ToString() + "ss");
        getter.Add(maxFloor, maxFloor.ToString() + "ss");
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

    }

    private void AddMaxMinFloorState(int floor, int nextFloor, string direction) {
        fSM.Add(floor.ToString() + "ss", new Dictionary<string, string>());
        fSM.Add(floor.ToString() + "ms", new Dictionary<string, string>());

        foreach (string state in new List<string>(){"ss", "ms"}) {

            fSM[floor.ToString() + state].Add("__", floor.ToString() + "ss");
            fSM[floor.ToString() + state].Add("_" + direction, nextFloor.ToString() + direction);

            foreach (string dir in new List<string>(){"_", direction}) {
                for (int fl = 1; fl < floor; fl ++) {
                    fSM[floor.ToString() + state].Add(fl.ToString() + dir, nextFloor.ToString() + direction);
                }
            }

            foreach (string dir in new List<string>(){"_", direction}) {
                for (int fl = maxFloor; fl > floor; fl --) {
                    fSM[floor.ToString() + state].Add(fl.ToString() + dir, nextFloor.ToString() + direction);
                }
            }

        } // для уезда и стоянки

    }

    private void AddUpDownState() {
        
        for (int floor = 3; floor < maxFloor - 1; floor ++) {

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                fSM[floor.ToString() + "up"].Add(floor.ToString() + dir, floor.ToString() + "up" + "s");
                fSM[floor.ToString() + "dn"].Add(floor.ToString() + dir, floor.ToString() + "dn" + "s");
            } // curFloor anyDir

            for (int fl = floor + 1; fl <= maxFloor; fl ++) {
                foreach (string dir in new List<string>(){"_", "dn"}) {
                    fSM[floor.ToString() + "up"].Add(fl.ToString() + dir, (floor + 1).ToString() + "up");
                }
                fSM[floor.ToString() + "up"].Add(fl.ToString() + "up", floor.ToString() + "up" + "s");
            } // upperFloor _ || upperFloor down скипаем и едем вверх и останавливаемся если upperFloor up

            for (int fl = 1; fl < floor; fl ++) {
                foreach (string dir in new List<string>(){"_", "up"}) {
                    fSM[floor.ToString() + "dn"].Add(fl.ToString() + dir, (floor - 1).ToString() + "dn");
                }
                fSM[floor.ToString() + "dn"].Add(fl.ToString() + "dn", floor.ToString() + "dn" + "s");
            } // lowerFloor _ || lowerFloor up едем вниз и останавливаемся если lowerFloor dn
        }

        foreach(int floor in new List<int> {2, maxFloor - 1}) {
            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                fSM[floor.ToString() + "up"].Add(floor.ToString() + dir, floor.ToString() + "up" + "s");
                fSM[floor.ToString() + "dn"].Add(floor.ToString() + dir, floor.ToString() + "dn" + "s");
            }
        } // minMaxFloors currentFloor

        for (int fl = 3; fl <= maxFloor; fl ++) {
                foreach (string dir in new List<string>(){"_", "dn"}) {
                    fSM[2.ToString() + "up"].Add(fl.ToString() + dir, 3.ToString() + "up");
                }
                fSM[2.ToString() + "up"].Add(fl.ToString() + "up", 2.ToString() + "up" + "s");
        }

        for (int fl = 1; fl < maxFloor - 1; fl ++) {
                foreach (string dir in new List<string>(){"_", "up"}) {
                    fSM[(maxFloor - 1).ToString() + "dn"].Add(fl.ToString() + dir, (maxFloor - 2).ToString() + "dn");
                }
                fSM[(maxFloor - 1).ToString() + "dn"].Add(fl.ToString() + "dn", (maxFloor - 1).ToString() + "dn" + "s");
        }

        fSM[2.ToString() + "dn"].Add(1.ToString() + "up", 1.ToString() + "ms");
        fSM[(maxFloor - 1).ToString() + "up"].Add(maxFloor.ToString() + "dn", maxFloor.ToString() + "ms"); // minMaxFloors oppositeDirs

        fSM[2.ToString() + "dn"].Add(1.ToString() + "_", 1.ToString() + "ms");
        fSM[(maxFloor - 1).ToString() + "up"].Add(maxFloor.ToString() + "_", maxFloor.ToString() + "ms"); // minMaxFloors _

    }

    private void AddUpDownStillState() {
        
        for (int floor = 3; floor < maxFloor - 1; floor ++) {
            
            fSM[floor.ToString() + "up" + "s"].Add("__", floor.ToString() + "ss");
            fSM[floor.ToString() + "dn" + "s"].Add("__", floor.ToString() + "ss");

            foreach (string dir in new List<string>(){ "up", "dn"}) {
                fSM[floor.ToString() + "up" + "s"].Add("_" + dir, floor.ToString() + dir);
                fSM[floor.ToString() + "dn" + "s"].Add("_" + dir, floor.ToString() + dir);
            }

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                for (int fl = floor + 1; fl <= maxFloor; fl ++) {
                    fSM[floor.ToString() + "up" + "s"].Add(fl.ToString() + dir, (floor + 1).ToString() + "up");
                }

                for (int fl = floor - 1; fl >= 1; fl --) {
                    fSM[floor.ToString() + "dn" + "s"].Add(fl.ToString() + dir, (floor - 1).ToString() + "dn");
                }

                fSM[floor.ToString() + "dn" + "s"].Add(floor.ToString() + dir, floor.ToString() + "dn" + "s");
                fSM[floor.ToString() + "up" + "s"].Add(floor.ToString() + dir, floor.ToString() + "up" + "s");
            }
        }

        foreach (string dir in new List<string>(){"_", "up", "dn"}) {
            for (int fl = 3; fl <= maxFloor; fl ++) {
                fSM[2.ToString() + "up" + "s"].Add(fl.ToString() + dir, 3.ToString() + "up");
            }

            for (int fl = maxFloor - 2; fl >= 1; fl --) {
                fSM[(maxFloor - 1).ToString() + "dn" + "s"].Add(fl.ToString() + dir, (maxFloor  - 2).ToString() + "dn");
            }

            fSM[2.ToString() + "dn" + "s"].Add(2.ToString() + dir, 2.ToString() + "dn" + "s");
            fSM[2.ToString() + "up" + "s"].Add(2.ToString() + dir, 2.ToString() + "up" + "s");
            fSM[(maxFloor - 1).ToString() + "up" + "s"].Add((maxFloor - 1).ToString() + dir, (maxFloor - 1).ToString() + "up" + "s");
            fSM[(maxFloor - 1).ToString() + "dn" + "s"].Add((maxFloor - 1).ToString() + dir, (maxFloor - 1).ToString() + "dn" + "s");
        }

        fSM[2.ToString() + "up" + "s"].Add("__", 2.ToString() + "ss");
        fSM[(maxFloor - 1).ToString() + "up" + "s"].Add("__", (maxFloor - 1).ToString() + "ss");

        fSM[2.ToString() + "dn" + "s"].Add("__", 2.ToString() + "ss");
        fSM[(maxFloor - 1).ToString() + "dn" + "s"].Add("__", (maxFloor - 1).ToString() + "ss");

        foreach (string dir in new List<string>(){ "up", "dn"}) {
                fSM[2.ToString() + "up" + "s"].Add("_" + dir, 2.ToString() + dir);
                fSM[(maxFloor - 1).ToString() + "up" + "s"].Add("_" + dir, (maxFloor - 1).ToString() + dir);
                
                fSM[2.ToString() + "dn" + "s"].Add("_" + dir, 2.ToString() + dir);
                fSM[(maxFloor - 1).ToString() + "dn" + "s"].Add("_" + dir, (maxFloor - 1).ToString() + dir);
            }

        fSM[2.ToString() + "dn" + "s"].Add(1.ToString() + "_", 1.ToString() + "ms");
        fSM[2.ToString() + "dn" + "s"].Add(1.ToString() + "up", 1.ToString() + "ms");
        fSM[(maxFloor - 1).ToString() + "up" + "s"].Add(maxFloor.ToString() + "_", maxFloor.ToString() + "ms");
        fSM[(maxFloor - 1).ToString() + "up" + "s"].Add(maxFloor.ToString() + "dn", maxFloor.ToString() + "ms");

    }

    private void AddStillStillState() {
        for (int floor = 2; floor < maxFloor; floor ++) {
            fSM[floor.ToString() + "ss"].Add("__", floor.ToString() + "ss");

            fSM[floor.ToString() + "ss"].Add("_" + "up", (floor + 1).ToString() + "up");
            fSM[floor.ToString() + "ss"].Add("_" + "dn", (floor - 1).ToString() + "dn");

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                for (int fl = floor + 1; fl <= maxFloor; fl ++) {
                    fSM[floor.ToString() + "ss"].Add(fl.ToString() + dir, (floor + 1).ToString() + "up");
                }
            }

            foreach (string dir in new List<string>(){"_", "up", "dn"}) {
                for (int fl = floor - 1; fl >= 1; fl --) {
                    fSM[floor.ToString() + "ss"].Add(fl.ToString() + dir, (floor - 1).ToString() + "dn");
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

        AddUpDownStillState();

        AddStillStillState();
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

        manager.ManageCalls(awaitingCalls, elevators, floorsButtons);

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

            manager.ManageCalls(awaitingCalls, elevators, floorsButtons);

            foreach (IElevatable elevator in elevators) {
                string curState = elevator.GetState();
                string action = elevator.GetClosestFloor() + floorsButtons[elevator.GetCurrentFloor()];
                string nextState = fSM[curState][action];
                
                elevator.SetState(nextState);
                typeof(ElevatorLogic).GetMethod(elevator.GetState()[1..])?.Invoke(logic, [awaitingCalls, elevator, floorsButtons]);
            }

        }

        foreach (IElevatable elevator in elevators) {
            Trace.TraceInformation(elevator.GetActionsCount().ToString());
        }

    }

}
