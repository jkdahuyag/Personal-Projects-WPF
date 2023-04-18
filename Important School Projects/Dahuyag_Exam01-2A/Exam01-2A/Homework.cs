namespace Exam01_2A;

public class Homework:IComparable<Homework>
{
    public DateTime DueDate { get; set; }
    public string Name { get; set; }

    public Homework(DateTime dueDate, string name)
    {
        DueDate = dueDate;
        Name = name;
    }

    public int CompareTo(Homework? obj)
    {
        if (DueDate == obj.DueDate && Name == obj.Name) return 0;
        if (DueDate > obj.DueDate) return 1;

        return -1;
    }
}