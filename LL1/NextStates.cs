public class NextStates {
    private Dictionary<int, int?> nextStates;

    public NextStates() {
        nextStates  = new() {
            {10, 100},
            {20, 200},
            {21, 210},
            {22, 220},
            {30, 300},
            {40, 400},
            {41, 410},
            {42, 420},
            {50, 500},
            {51, 510},
            {52, 520},
            {100, 30},
            {101, 20},
            {200, 201},
            {201, 30},
            {202, 20},
            {210, 211},
            {211, 30},
            {212, 20},
            {220, null},
            {300, 50},
            {301, 40},
            {400, 401},
            {401, 50},
            {402, 40},
            {410, 411},
            {411, 50},
            {412, 40},
            {420, null},
            {500, null},
            {510, null},
            {520, 521},
            {521, 10},
            {522, null}
        };
    }

    public Dictionary<int, int?> GetDict() {
        return nextStates;
    }
}