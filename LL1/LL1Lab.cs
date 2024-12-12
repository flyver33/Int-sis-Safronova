
public static class LL1 {
    public static void Main(string[] args) {

        string pathToCase = "C:\\dev\\Int-sis\\LL1\\testCases\\positiveCase.txt";
        string Case = File.ReadAllText(pathToCase);

        string pathToTsv = "C:\\dev\\Int-sis\\LL1\\data\\LL1_data.tsv";

        TsvReader reader = new TsvReader();

        Dictionary<int, int?> nextStatesRead;
        Dictionary<int, List<string>> stateSymbolsRead;

        (nextStatesRead, stateSymbolsRead) = reader.Read(pathToTsv);

        StateSymbols stateSymbols = new(stateSymbolsRead);
        NextStates nextStates = new(nextStatesRead);

        Stack<int?> stack = new();
        stack.Push(null);

        LexerLL1 lexer = new LexerLL1(Case, stateSymbols.GetDict());
        lexer.SplitLine();

        States statesClass = new(lexer, nextStates.GetDict(), stack, stateSymbols);
        Dictionary<int, State> states = statesClass.GetDict();

        int? curNumber = 1;

        try {
            while (curNumber != null) {
                    State curState = states[curNumber.GetValueOrDefault()];
                    curNumber = curState.Next();
            }

            Console.WriteLine("Выражение относится к этой грамматике");

        } catch (NullReferenceException) {
            Console.WriteLine("Выражение не относится к этой грамматике");
        }
    }
}