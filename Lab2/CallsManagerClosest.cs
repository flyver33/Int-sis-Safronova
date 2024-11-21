namespace Lab2;

class CallsManagerClosest() : IElevatorsCallsManager
{
    public void ManageCalls(List<(int, int)> awaitingCalls, List<(int, int)> operatedCalls, List<IElevatable> elevators) {

        (int, int)[] copyAwaitingCalls = new (int, int)[awaitingCalls.Count];
        awaitingCalls.CopyTo(copyAwaitingCalls);

        foreach((int, int) call in copyAwaitingCalls) {
            List<IElevatable> pickableElevators = new();

            try {
                pickableElevators.AddRange(elevators.FindAll(el => (call.Item1 - el.GetCurrentFloor()) * (el.GetDistantFloor() - call.Item1) >= 0));
                operatedCalls.Add(call);
                awaitingCalls.Remove(call);
                continue;
            } catch(Exception) {}

            try {
                pickableElevators.AddRange(elevators.FindAll(el => el.GetState()[1..] == "ss" || el.GetState()[1..] == "m"));
                
            } catch(ArgumentNullException) {continue;}

            try {
                pickableElevators.Sort((el1, el2) => Math.Abs((long) el1.GetCurrentFloor() - call.Item1).CompareTo(Math.Abs((long) el2.GetCurrentFloor() - call.Item1)));
                pickableElevators[0].AddFloor(call.Item1);
                operatedCalls.Add(call);
                awaitingCalls.Remove(call);
                
            } catch (ArgumentNullException) {}

        }
    }

}