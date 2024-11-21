namespace Lab2;

public interface ILogicable
{
    void up(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons); 
    void dn(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void ups(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void dns(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void ss(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void m(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
}
