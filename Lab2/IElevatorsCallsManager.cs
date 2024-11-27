namespace Lab2;

public interface IElevatorsCallsManager
{
    void ManageCalls(List<(int, int)> awaitingCalls, List<IElevatable> elevators, Dictionary<int, string> floorsButtons);
}