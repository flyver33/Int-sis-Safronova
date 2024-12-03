public static class LL1 {
    public static void Main(string[] args) {
        string a = "( 1 + 3 ) * 2 + abc + ( 1 * 2 + 3 )";

        Stack<int?> stack = new();
        stack.Push(null);

        StateSymbols stateSymbols = new();
        NextStates nextStates = new();

        LexerLL1 lexer = new LexerLL1(a, stateSymbols.GetDict());
        lexer.SplitLine();

        States statesClass = new(lexer, nextStates.GetDict(), stack, stateSymbols);
        Dictionary<int, State> states = statesClass.GetDict();

        int? curNumber = 10;

        while (curNumber != null) {
            State curState = states[curNumber.GetValueOrDefault()];
            curNumber = curState.Next();
        }

        Console.WriteLine("Выражение относится к этой грамматике");
    }
}