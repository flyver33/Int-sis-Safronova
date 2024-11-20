namespace Lab2;

public interface ILogicable
{
    void up(IElevatable elevator); 
    void down(IElevatable elevator);
    void ups(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void downs(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, List<(int, int)> takenCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void ssthis(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, IElevatable elevator, Dictionary<int, string> floorsButtons);
    void ss_(List<(int, int)> operatedCalls, List<(int, int)> awaitingCalls, IElevatable elevator);
}
