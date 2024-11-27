using System.Diagnostics;
using System.Globalization;

namespace Lab2;

class MyOperator {
    
    private List<IElevatable> elevators = [];
    private int maxFloor;
    private Queue<(int, int)> calls;
    private Dictionary<string, Dictionary<string, string>> fSM = new();
    private ILogicable logic = new ElevatorLogic();
    private Dictionary<int, string> floorsButtons = new();
    private Dictionary<int, Dictionary<int, string>> directions = new();

    private Dictionary<int, string> GetInitialState(int maxFloor) {
        Dictionary<int, string> getter = new();
        getter.Add(1, 1.ToString() + "ss");
        getter.Add(maxFloor, maxFloor.ToString() + "ss");
        List<int> floors = Enumerable.Range(2, maxFloor - 2).ToList();
        floors.ForEach(floor => {
            getter.Add(floor, floor.ToString() + "ss");
        });
        return getter;
    }
    
    public MyOperator(int maxFloor, List<int> currentFloors, Queue<(int, int)> calls, int numElevs) {

        this.maxFloor = maxFloor;
        this.calls = calls;

        Dictionary<int, string> getterInitState = GetInitialState(maxFloor);
        
        List<int> elevs = Enumerable.Range(0, numElevs).ToList();
        elevs.ForEach(i => {
            elevators.Add(new Elevator(currentFloors[i], (i+1).ToString(), getterInitState[currentFloors[i]]));
        });
        
        FSMGenerator generator = new(maxFloor);
        fSM = generator.GenerateFSM();

        floorsButtons = generator.GenerateFloorsButtons();
        directions = generator.GenerateDirections();

    }

    private bool getCalls() {
        bool calls = false;

        elevators.ForEach(elevator => {
            calls = calls || elevator.GetOperated().Count > 0;
            calls = calls || elevator.GetTaken().Count > 0;
        });

        return calls;
    }


    public void Start() {
        List<(int, int)> awaitingCalls = []; // просто нажалась кнопка

        IElevatorsCallsManager manager = new CallsManagerClosest();
        FloorsManager floorsManager = new();

        try {
            floorsButtons[calls.Peek().Item1] = directions[calls.Peek().Item1][calls.Peek().Item2];
            awaitingCalls.Add(calls.Dequeue());
        } catch (InvalidOperationException) {}

        manager.ManageCalls(awaitingCalls, elevators, floorsButtons);

        elevators.ForEach(elevator => {
            typeof(ElevatorLogic).GetMethod(elevator.GetState()[1..])?.Invoke(logic, [awaitingCalls, elevator, floorsButtons]);
        });

        elevators.ForEach(elevator => {
                string curState = elevator.GetState();
                string action = floorsManager.GetClosestFloor(elevator.GetFloors(), elevator.GetDir(), elevator.GetCurrentFloor()) + floorsButtons[elevator.GetCurrentFloor()];
                string nextState = fSM[curState][action];
                
                elevator.SetState(nextState);

        });

        while (calls.Count > 0 || getCalls() || awaitingCalls.Count > 0) {

            try {
                floorsButtons[calls.Peek().Item1] = directions[calls.Peek().Item1][calls.Peek().Item2];
                awaitingCalls.Add(calls.Dequeue());
            } catch (InvalidOperationException) {}

            manager.ManageCalls(awaitingCalls, elevators, floorsButtons);

            elevators.ForEach(elevator => {
                string curState = elevator.GetState();
                string action = floorsManager.GetClosestFloor(elevator.GetFloors(), elevator.GetDir(), elevator.GetCurrentFloor()) + floorsButtons[elevator.GetCurrentFloor()];
                string nextState = fSM[curState][action];
                
                elevator.SetState(nextState);
                typeof(ElevatorLogic).GetMethod(elevator.GetState()[1..])?.Invoke(logic, [awaitingCalls, elevator, floorsButtons]);
            });

        }

        elevators.ForEach(elevator => {
            Trace.TraceInformation(elevator.GetActionsCount().ToString());
        });

    }

}
