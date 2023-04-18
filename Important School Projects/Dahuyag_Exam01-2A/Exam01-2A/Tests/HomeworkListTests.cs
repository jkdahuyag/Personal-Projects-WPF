using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Exam01_2A.Tests
{
    public class HomeworkListTests
    {
        [Test]
        public void ListOfHomeworks_ListWithNoHomework_GiveEmptyList()
        {
            //arrange
            var homeworkList = new HomeworkList();
            //act
            var list = homeworkList.ListOfHomeworks();
            //assert
            var expectedCount = 0;
            list.Should().BeEmpty();
            list.Count.Should().Be(expectedCount);
        }
        [Test]
        public void ListOfHomeworks_ListWithInitialHomework_GiveListOfHomeWorksInOrder()
        {
            //arrange
            var homeworkList = new HomeworkList();
            var dueDate1 = new DateTime(2022, 10, 1);
            var name1 = "Assignment 1";
            var dueDate2 = new DateTime(2022, 11, 12);
            var name2 = "Assignment 2";
            var dueDate3 = new DateTime(2022, 4, 21);
            var name3 = "Assignment 3";
            var dueDate4 = new DateTime(2022, 9, 26);
            var name4 = "Assignment 4";
            var dueDate5 = new DateTime(2022, 10, 1);
            var name5 = "Assignment 5";
            var homework1 = new Homework(dueDate1, name1);
            var homework2 = new Homework(dueDate2, name2);
            var homework3 = new Homework(dueDate3, name3);
            var homework4 = new Homework(dueDate4, name4);
            var homework5 = new Homework(dueDate5, name5);
            var expectedList = new List<Homework>()
            {
                homework1,homework2,homework3, homework4, homework5
            };
            homeworkList.AddHomework(homework1);
            homeworkList.AddHomework(homework2);
            homeworkList.AddHomework(homework3);
            homeworkList.AddHomework(homework4);
            homeworkList.AddHomework(homework5);
            //act
            var list = homeworkList.ListOfHomeworks();
            //assert
            var expectedCount = 5;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Should().Be(expectedList[i]);
            }
            homeworkList.ListOfHomeworks().Count.Should().Be(expectedCount);
        }
        [Test]
        public void AddHomework_0InitialAssignments_ListContainsHomeworkCount1()
        {
            //arrange
            var homeworkList = new HomeworkList();
            var dueDate1 = new DateTime(2022, 10, 1);
            var name1 = "Assignment 1";
            var homework1 = new Homework(dueDate1, name1);
            //act
            homeworkList.AddHomework(homework1);
            //assert
            var expectedCount = 1;
            homeworkList.ListOfHomeworks().Should().Contain(homework1);
            homeworkList.ListOfHomeworks().Count.Should().Be(expectedCount);
        }
        [Test]
        public void RemoveHomework_0InitialAssignments_ThrowError()
        {
            //arrange
            var homeworkList = new HomeworkList();
            var dueDate1 = new DateTime(2022, 10, 1);
            var name1 = "Assignment 1";
            var homework1 = new Homework(dueDate1, name1);
            //act
            var act = () =>
            {
                homeworkList.RemoveHomework(homework1);
            };
            //assert
            act.Should().Throw<InvalidOperationException>().WithMessage("List is empty");
        }
        [Test]
        public void RemoveHomework_HomeWorkDoesNotExist_ThrowError()
        {
            //arrange
            var homeworkList = new HomeworkList();
            var dueDate1 = new DateTime(2022, 10, 1);
            var name1 = "Assignment 1";
            var homework1 = new Homework(dueDate1, name1);
            var homework2 = new Homework(dueDate1, "asdasd");
            homeworkList.AddHomework(homework1);
            //act
            var act = () =>
            {
                homeworkList.RemoveHomework(homework2);
            };
            //assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Position is out of*");
        }
        [Test]
        public void RemoveHomework_5initialHomework_RemoveCorrectHomeWorkCount4()
        {
            //arrange
            var homeworkList = new HomeworkList();
            var dueDate1 = new DateTime(2022, 10, 1);
            var name1 = "Assignment 1";
            var dueDate2 = new DateTime(2022, 11, 12);
            var name2 = "Assignment 2";
            var dueDate3 = new DateTime(2022, 4, 21);
            var name3 = "Assignment 3";
            var dueDate4 = new DateTime(2022, 9, 26);
            var name4 = "Assignment 4";
            var dueDate5 = new DateTime(2022, 10, 11);
            var name5 = "Assignment 5";
            var homework1 = new Homework(dueDate1, name1);
            var homework2 = new Homework(dueDate2, name2);
            var expectedHomework = new Homework(dueDate3, name3);
            var homework4 = new Homework(dueDate4, name4);
            var homework5 = new Homework(dueDate5, name5);
            homeworkList.AddHomework(homework1);
            homeworkList.AddHomework(homework2);
            homeworkList.AddHomework(expectedHomework);
            homeworkList.AddHomework(homework4);
            homeworkList.AddHomework(homework5);
            //act
            homeworkList.RemoveHomework(expectedHomework);
            //assert
            var expectedCount = 4;
            homeworkList.ListOfHomeworks().Count.Should().Be(expectedCount);
            homeworkList.ListOfHomeworks().Should().NotContain(expectedHomework);
        }
        [Test]
        public void EarliestDueDateHomework_5InitialHomeworkNoSameDueDate_ListContainsHomeWork()
        {
            //arrange
            var homeworkList = new HomeworkList();
            var dueDate1 = new DateTime(2022, 10, 1);
            var name1 = "Assignment 1";
            var dueDate2 = new DateTime(2022, 11, 12);
            var name2 = "Assignment 2";
            var dueDate3 = new DateTime(2022, 4, 21);
            var name3 = "Assignment 3";
            var dueDate4 = new DateTime(2022, 9, 26);
            var name4 = "Assignment 4";
            var dueDate5 = new DateTime(2022, 10, 11);
            var name5 = "Assignment 5";
            var homework1 = new Homework(dueDate1, name1);
            var homework2 = new Homework(dueDate2, name2);
            var expectedHomework = new Homework(dueDate3, name3);
            var homework4 = new Homework(dueDate4, name4);
            var homework5 = new Homework(dueDate5, name5);
            homeworkList.AddHomework(homework1);
            homeworkList.AddHomework(homework2);
            homeworkList.AddHomework(expectedHomework);
            homeworkList.AddHomework(homework4);
            homeworkList.AddHomework(homework5);
            //act
            var earliest = homeworkList.EarliestDueDateHomework();
            //assert
            var expectedCountOfEarliestHomeworks = 1;
            earliest.Should().Contain(expectedHomework);
            earliest.Count.Should().Be(expectedCountOfEarliestHomeworks);
        }
        [Test]
        public void EarliestDueDateHomework_5InitialHomeworkSameEarlyDueDate_ListContainsHomeworks()
        {
            //arrange
            var homeworkList = new HomeworkList();
            var dueDate1 = new DateTime(2022, 10, 1);
            var name1 = "Assignment 1";
            var dueDate2 = new DateTime(2022, 4, 21);
            var name2 = "Assignment 2";
            var dueDate3 = new DateTime(2022, 4, 21);
            var name3 = "Assignment 3";
            var dueDate4 = new DateTime(2022, 4, 21);
            var name4 = "Assignment 4";
            var dueDate5 = new DateTime(2023, 10, 11);
            var name5 = "Assignment 5";
            var homework1 = new Homework(dueDate1, name1);
            var expectedHomework1 = new Homework(dueDate2, name2);
            var expectedHomework2 = new Homework(dueDate3, name3);
            var expectedHomework3 = new Homework(dueDate4, name4);
            var homework5 = new Homework(dueDate5, name5);
            homeworkList.AddHomework(homework1);
            homeworkList.AddHomework(expectedHomework1);
            homeworkList.AddHomework(expectedHomework2);
            homeworkList.AddHomework(expectedHomework3);
            homeworkList.AddHomework(homework5);
            //act
            var earliest = homeworkList.EarliestDueDateHomework();
            //assert
            var expectedCountOfEarliestHomeworks = 3;
            earliest.Should().Contain(expectedHomework1);
            earliest.Should().Contain(expectedHomework2);
            earliest.Should().Contain(expectedHomework3);
            earliest.Count.Should().Be(expectedCountOfEarliestHomeworks);
        }
        [Test]
        public void EarliestDueDateHomework_0InitialHomework_ReturnEmpty()
        {
            //arrange
            var homeworkList = new HomeworkList();
            //act
            var earliest = homeworkList.EarliestDueDateHomework();
            //assert
            var expectedCount = 0;
            earliest.Count.Should().Be(expectedCount);
            earliest.Should().BeEmpty();
        }
    }
}
