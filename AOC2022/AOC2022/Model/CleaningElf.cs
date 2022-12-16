namespace AOC2022.Model;

public record CleaningElf(int StartSection, int EndSection);
public record CleaningPair(int PairIndex, CleaningElf ElfOne, CleaningElf ElfTwo);