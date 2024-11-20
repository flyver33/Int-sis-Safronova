using System.Globalization;

namespace Lab2;

class Operator {
    
    private List<IElevatable> elevators = [];
    private int maxFloor;
    private Queue<(int, int)> calls;
    private Dictionary<string, Dictionary<string, string>> fSM = new Dictionary<string, Dictionary<string, string>>();
    private Func<int, int> plusFloor = floor => floor + 1;
    private Func<int, int> minusFloor = floor => floor - 1;
    private ILogicable logic = new ElevatorLogic();
    

    public Operator(int maxFloor, List<int> currentFloors, Queue<(int, int)> calls, int numElevs) {

        this.maxFloor = maxFloor;
        this.calls = calls;
        Dictionary<string, string> initState = InitialStateMap();
        string initState1 = initState[currentFloors[0].ToString()];
        string initState2 = initState[currentFloors[1].ToString()];

        for (int i = 0; i < numElevs; i++) {
            elevators.Add(new Elevator(currentFloors[i], (i+1).ToString(), initState1));
        }

        GenerateFSM();
        Start();

    }

    private Dictionary<string, string> InitialStateMap() {
        Dictionary<string, string> initState = new Dictionary<string, string>();
        
        initState[1.ToString()] = 1.ToString();
        initState[maxFloor.ToString()] = maxFloor.ToString();
        for (int i = 2; i < maxFloor; i++) {
            initState[i.ToString()] = i.ToString() + "ss";
        }

        return initState;
    }

    private void AddMaxMinFloorState(int floor, string direction) {
        fSM.Add(floor.ToString(), new Dictionary<string, string>());
        fSM[floor.ToString()].Add("__", floor.ToString());
        fSM[floor.ToString()].Add("_" + direction, floor.ToString());

        foreach (string dir in new List<string>(){"_", direction}) {
            for (int fl = 1; fl < floor; fl ++) {
                fSM[floor.ToString()].Add(fl.ToString() + dir, (floor - 1).ToString() + direction);
            }
        }

    }

    private void AddUpDownState(int dirFloor, string direction, Func<int, int> floorOp) {
        
        for (int floor = 2; floor < dirFloor; floor ++) {

            int minv = Math.Min(dirFloor, floor);
            int maxv = Math.Max(dirFloor, floor);

            foreach (string dir in new List<string>(){"_", "up", "down"}) {
                fSM[floor.ToString() + direction].Add(floor.ToString() + dir, floor.ToString() + direction + "s");
                fSM[floor.ToString() + direction].Add(dirFloor.ToString() + dir, dirFloor.ToString());
            }

            foreach (string dir in new List<string>(){"_", direction}) {
                for (int fl = minv + 1; fl < maxv - 1; fl ++) {
                    fSM[floor.ToString() + direction].Add(fl.ToString() + dir, floorOp(floor).ToString() + direction);
                }
            }
        }

    }

    private void AddUpDownStillState(int dirFloor, string direction) {
        
        for (int floor = 2; floor < dirFloor; floor ++) {

            int minv = Math.Min(dirFloor, floor);
            int maxv = Math.Max(dirFloor, floor);
            
            fSM[floor.ToString() + direction + "s"].Add("__", floor.ToString() + "ss");

            foreach (string dir in new List<string>(){ "up", "down"}) {
                fSM[floor.ToString() + direction + "s"].Add("_" + dir, floor.ToString() + dir);
            }

            foreach (string dir in new List<string>(){"_", "up", "down"}) {
                for (int fl = minv + 1; fl < maxv - 1; fl ++) {
                    fSM[floor.ToString() + direction + "s"].Add(fl.ToString() + dir, floor.ToString() + direction);
                }
            }
        }

    }

    private void AddStillStillState(int maxFloor) {
        for (int floor = 2; floor < maxFloor; floor ++) {
            fSM[floor.ToString() + "ss"].Add("__", floor.ToString() + "ss");

            foreach (string dir in new List<string>(){ "up", "down"}) {
                fSM[floor.ToString() + "ss"].Add("_" + dir, floor.ToString() + dir);
            }

            foreach (string dir in new List<string>(){"_", "up", "down"}) {
                for (int fl = 2; fl < floor; fl ++) {
                    fSM[floor.ToString() + "ss"].Add(fl.ToString() + dir, floor.ToString() + "up");
                }
            }

            foreach (string dir in new List<string>(){"_", "up", "down"}) {
                for (int fl = floor; fl > 1; fl --) {
                    fSM[floor.ToString() + "ss"].Add(fl.ToString() + dir, floor.ToString() + "down");
                }
            }
        }
    }
    

    private void GenerateFSM() {
        AddMaxMinFloorState(maxFloor, "down");
        AddMaxMinFloorState(1, "up");

        for (int floor = 2; floor < maxFloor; floor ++) {
            foreach (string dir in new List<string>(){"ss", "up", "down", "ups", "downs"}) {
                fSM.Add(floor.ToString() + dir, new Dictionary<string, string>());
            }
        }

        AddUpDownState(1, "down", minusFloor);
        AddUpDownState(maxFloor, "up", plusFloor);

        AddUpDownStillState(1, "down");
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

    public Dictionary<int, string> GenerateDirections() {
        Dictionary<int, string> directions = new Dictionary<int, string>();

        

        return directions;
    }


    public void Start() {
        List<(int, int)> operatedCalls = new List<(int, int)>();
        List<(int, int)> takenCalls = new List<(int, int)>();
        List<(int, int)> awaitingCalls = new List<(int, int)>();

        Dictionary<int, string> floorsButtons = GenerateFloorsButtons();

        while (calls.Count > 0) {

            awaitingCalls.Add(calls.Dequeue());

            foreach (IElevatable elevator in elevators) {

            }
            

        }

    }

}
