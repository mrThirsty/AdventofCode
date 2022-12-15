namespace AOC2022.Library.Interfaces;

public interface IPuzzle
{
    string Label { get; }
    int OrderIndex { get; }
    Task ExecutePuzzle(CancellationToken ct);
}