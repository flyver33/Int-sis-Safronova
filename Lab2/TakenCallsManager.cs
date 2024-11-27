namespace Lab2;

class TakenCallsManager {
    
    public void AddOperated((int, int) addingElement, List<(int, int)> operatedCalls) {
        operatedCalls.Add(addingElement);
    }

    public List<(int, int)> FindAllOperated(Predicate<(int, int)> match, List<(int, int)> operatedCalls) {
        return operatedCalls.FindAll(match);
    }
    public int RemoveAllOperated(Predicate<(int, int)> match, List<(int, int)> operatedCalls) {
        return operatedCalls.RemoveAll(match);
    }

    public void AddRangeTaken(List<(int, int)> addingList, List<(int, int)> takenCalls) {
        takenCalls.AddRange(addingList);
    }
    public void AddTaken((int, int) addingElement, List<(int, int)> takenCalls) {
        takenCalls.Add(addingElement);
    }

    public int RemoveAllTaken(Predicate<(int, int)> match, List<(int, int)> takenCalls) {
        return takenCalls.RemoveAll(match);
    }
}