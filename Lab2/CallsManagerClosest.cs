namespace Lab2;

class CallsManagerClosest() : IElevatorsCallsManager
{
    public void ManageCalls(List<(int, int)> awaitingCalls, List<IElevatable> elevators, Dictionary<int, string> floorsButtons) {

        List<(int, int)> copyAwaitingCalls = [.. awaitingCalls];

        copyAwaitingCalls.ForEach(call => {
            List<IElevatable> pickableElevators = new();

            try {
                pickableElevators.AddRange(elevators.FindAll(el => el.GetFloors().Count != 0).FindAll(el => (call.Item1 - el.GetCurrentFloor()) * (el.GetDistantFloor() - call.Item1) >= 0).FindAll(el => el.GetDir() == floorsButtons[call.Item1]));
                pickableElevators.Sort((el1, el2) => Math.Abs((long) el1.GetCurrentFloor() - call.Item1).CompareTo(Math.Abs((long) el2.GetCurrentFloor() - call.Item1)));
                pickableElevators[0].AddOperated(call);
                pickableElevators[0].AddFloor(call.Item1);
                awaitingCalls.Remove(call);
                return;
            } catch (Exception e) when (e is InvalidOperationException || e is ArgumentOutOfRangeException) {}

            try {
                pickableElevators.AddRange(elevators.FindAll(el => call.Item1 == el.GetCurrentFloor()));
                pickableElevators[0].AddFloor(call.Item2);
                pickableElevators[0].AddTaken(call);
                awaitingCalls.Remove(call);
                return;

            } catch(ArgumentOutOfRangeException) {}

            try {
                pickableElevators.AddRange(elevators.FindAll(el => el.GetState()[1..] == "ss" || el.GetState()[1..] == "ms"));
                
            } catch(ArgumentNullException) {return;}

            try {
                pickableElevators.Sort((el1, el2) => Math.Abs((long) el1.GetCurrentFloor() - call.Item1).CompareTo(Math.Abs((long) el2.GetCurrentFloor() - call.Item1)));
                pickableElevators[0].AddFloor(call.Item1);
                pickableElevators[0].AddOperated(call);
                awaitingCalls.Remove(call);
                
            } catch (ArgumentOutOfRangeException) {}

        });
    }

}