public class States {
    private Dictionary<int, State> states;

    public States(Lexer lexer, Dictionary<int, int?> nextStates, Stack<int?> stack, StateSymbols stateSymbols) {
        states  = new() {
            {10, new State4(10, lexer, nextStates[10])},
            {20, new State6(20, stateSymbols, lexer, nextStates[20])},
            {21, new State6(21, stateSymbols, lexer, nextStates[21])},
            {22, new State4(22, lexer, nextStates[22])},
            {30, new State4(30, lexer, nextStates[30])},
            {40, new State6(40, stateSymbols, lexer, nextStates[40])},
            {41, new State6(41, stateSymbols, lexer, nextStates[41])},
            {42, new State4(42, lexer, nextStates[42])},
            {50, new State6(50, stateSymbols, lexer, nextStates[50])},
            {51, new State6(51, stateSymbols, lexer, nextStates[51])},
            {52, new State4(42, lexer, nextStates[52])},
            {100, new State1(100, lexer, stack, nextStates[100])},
            {101, new State4(101, lexer, nextStates[101])},
            {200, new State5(200, lexer, nextStates[200])},
            {201, new State1(201, lexer, stack, nextStates[201])},
            {202, new State4(202, lexer, nextStates[202])},
            {210, new State5(210, lexer, nextStates[210])},
            {211, new State1(211, lexer, stack, nextStates[211])},
            {212, new State4(212, lexer, nextStates[212])},
            {220, new State2(220, lexer, stack)},
            {300, new State1(300, lexer, stack, nextStates[300])},
            {301, new State4(101, lexer, nextStates[301])},
            {400, new State5(400, lexer, nextStates[400])},
            {401, new State1(401, lexer, stack, nextStates[401])},
            {402, new State4(402, lexer, nextStates[402])},
            {410, new State5(410, lexer, nextStates[410])},
            {411, new State1(411, lexer, stack, nextStates[411])},
            {412, new State4(412, lexer, nextStates[412])},
            {420, new State2(420, lexer, stack)},
            {500, new State3(500, lexer, stack)},
            {510, new State3(510, lexer, stack)},
            {520, new State5(520, lexer, nextStates[520])},
            {521, new State1(521, lexer, stack, nextStates[521])},
            {522, new State3(522, lexer, stack)}
        };
    }

    public Dictionary<int, State> GetDict() {
        return states;
    }
}