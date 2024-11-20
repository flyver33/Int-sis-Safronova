namespace Lab2;

public interface ILogicable
{
    void up(IElevatable elevator); 
    void dn(IElevatable elevator);
    void ups(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void dns(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void ss(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void m(List<(int, int)> operatedCalls, List<(int, int)> takenCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
}
