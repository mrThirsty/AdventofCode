namespace AOC2022.Model;

public record Rucksack(int Index, string AllItems, string CompartmentOne, string CompartmentTwo, Dictionary<string, int> CommonPriorities);