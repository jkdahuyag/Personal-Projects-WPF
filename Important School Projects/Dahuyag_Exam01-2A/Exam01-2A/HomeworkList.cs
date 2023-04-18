using System.Collections;
using Dahuyag_Assessment_1;

namespace Exam01_2A
{
    public class HomeworkList
    {
        private GenericDoublyLinkedList<Homework> _list = new GenericDoublyLinkedList<Homework>();

        public void AddHomework(Homework homework)
        {
            _list.AddToTail(homework);
        }

        public void RemoveHomework(Homework homework)
        {
            var homeworkList = ListOfHomeworks();
            var index = homeworkList.IndexOf(homework);
            _list.Remove(index);
        }

        public List<Homework> ListOfHomeworks()
        {
            return _list.ToList();
        }

        public List<Homework> EarliestDueDateHomework()
        {
            var homeworkList = ListOfHomeworks();
            homeworkList.Sort(Comparer<Homework>.Default);
            var grouped = homeworkList.GroupBy(c => c);
            return _list.Where(node => node.DueDate == grouped.First().Key.DueDate).ToList();
        }
    }
}