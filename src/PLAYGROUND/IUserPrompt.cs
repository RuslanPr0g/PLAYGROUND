namespace Playground;

internal interface IUserPrompt
{
    string PromptString(string title);
    int PromptInt(string title);
    T PromptChoice<T>(string title, T[] choices) where T : notnull;
    bool Confirm(string title);
    string PromptMultiLine(string title);
    int[] PromptIntArray(string title);
    
    string PromptStringOrChoice(string title, string[] examples);
    int[] PromptIntArrayOrChoice(string title, int[][] examples);
}
