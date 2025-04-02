namespace FluentDataGridIssueDemo.Util
{
    public class StringLengthComparer : IComparer<string>
    {
        public static readonly StringLengthComparer Instance = new();

        public int Compare(string? x, string? y) => x is null ? y is null ? 0 : -1 : y is null ? 1 : x.Length.CompareTo(y.Length);
    }
}
