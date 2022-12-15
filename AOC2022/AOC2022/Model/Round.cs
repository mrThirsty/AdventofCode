using AOC2022.Enums;

namespace AOC2022.Model;

public record Round(int RoundNumber, Shape OpponentsShape, Shape MyShape, RoundResult Result, int Score);