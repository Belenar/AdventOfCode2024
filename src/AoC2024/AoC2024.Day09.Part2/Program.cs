namespace AoC2024.Day09.Part2;

class Program
{
    public static void Main()
    {
        var fileMap = ParseInput();

        var sortedFies = SortFiles(fileMap);
        
        var checksum = CalculateCheckSum(sortedFies);

        Console.WriteLine($"The total checkSum is {checksum}.");
    }

    private static List<FileLocation> SortFiles(FileLocation[] fileMap)
    {
        var optimizedFiles = fileMap.ToList();

        for (var i = fileMap.Length - 1; i >= 0; i--)
        {
            var file = fileMap[i];
            var currentIndex = optimizedFiles.IndexOf(file);

            var firstFreeSpace = optimizedFiles
                .Where((freeSpace, index) => index < currentIndex && freeSpace.FreeSpaceAfter >= file.Length)
                .FirstOrDefault();

            if (firstFreeSpace != null)
            {
                var previousFile = optimizedFiles[currentIndex - 1];
                previousFile.FreeSpaceAfter += file.Length + file.FreeSpaceAfter;
                file.FreeSpaceAfter = firstFreeSpace.FreeSpaceAfter - file.Length;
                firstFreeSpace.FreeSpaceAfter = 0;
                var freeSpaceIndex = optimizedFiles.IndexOf(firstFreeSpace);
                optimizedFiles.Remove(file);
                optimizedFiles.Insert(freeSpaceIndex + 1, file);
            }
        }
        
        return optimizedFiles;
    }
    
    
    private static object CalculateCheckSum(List<FileLocation> sortedFies)
    {
        long checksum = 0;
        var diskPosition = 0;
        
        foreach (var fileLocation in sortedFies)
        {
            for (int i = 0; i < fileLocation.Length; i++)
            {
                checksum += diskPosition * fileLocation.FileId;
                diskPosition++;
            }
            diskPosition += fileLocation.FreeSpaceAfter;
        }

        return checksum;
    }
    
    private static FileLocation[] ParseInput()
    {
        var input = File.ReadAllLines("input.txt");
        var fileMap = input[0]
            .ToCharArray()
            .Select(c => int.Parse(c.ToString()))
            .ToArray();
        
        List<FileLocation> fileLocations = [];

        for (int i = 0; i <= (fileMap.Length/2); i++)
        {
            var file = new FileLocation
            {
                FileId = i,
                Length = fileMap[i*2]
            };
            if (i * 2 + 1 < fileMap.Length)
            {
                file.FreeSpaceAfter = fileMap[i * 2 + 1];
            }
            
            fileLocations.Add(file);
        }

        return fileLocations.ToArray();
    }
}

class FileLocation {
    public int Length { get; set; }
    public int FileId { get; set; }
    public int FreeSpaceAfter { get; set; }
}