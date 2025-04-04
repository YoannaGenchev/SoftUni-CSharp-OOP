using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RecourceCloud.Tests
{
    public class Tests
    {
        private static string GetRandomString()
        {
            var randomTextLength = Random.Shared.Next(minValue: 5, maxValue: 50);
            return GetRandomString(randomTextLength);
        }

        private static string GetRandomString(int length)
        {
            var symbols = new char[length];
            for (var i = 0; i < length; i++)
            {
                var randomLetterIndex = Random.Shared.Next(maxValue: 26);
                symbols[i] = (char)('a' + randomLetterIndex);
            }

            return new string(symbols);
        }

        private Task task;
        private Resource resource;
        private DepartmentCloud cloud;

        [SetUp]
        public void Setup()
        {
            var priority = Random.Shared.Next();
            var label = GetRandomString();
            var details = GetRandomString();
            task = new Task(priority, label, details);

            var name = GetRandomString();
            var resourceType = GetRandomString();
            bool isTested = false;
            resource = new Resource(name, resourceType);
            resource.IsTested = isTested;

            cloud = new DepartmentCloud();
        }

        [Test]
        public void TestTaskCreationAndProperties()
        {
            var priority = Random.Shared.Next();
            var label = GetRandomString();
            var details = GetRandomString();
            var testTask = new Task(priority, label, details);
            Assert.IsNotNull(testTask);
            Assert.That(testTask.Priority, Is.EqualTo(priority));
            Assert.That(testTask.Label, Is.EqualTo(label));
            Assert.That(testTask.ResourceName, Is.EqualTo(details));

            priority = Random.Shared.Next();
            label = GetRandomString();
            details = GetRandomString();
            task.Priority = priority;
            task.Label = label;
            task.ResourceName = details;
            Assert.That(task.Priority, Is.EqualTo(priority));
            Assert.That(task.Label, Is.EqualTo(label));
            Assert.That(task.ResourceName, Is.EqualTo(details));
        }

        [Test]
        public void TestResourceCreationAndProperties()
        {
            var name = GetRandomString();
            var resourceType = GetRandomString();
            var testResource = new Resource(name, resourceType);
            Assert.IsNotNull(testResource);
            Assert.That(testResource.Name, Is.EqualTo(name));
            Assert.That(testResource.ResourceType, Is.EqualTo(resourceType));

            name = GetRandomString();
            resourceType = GetRandomString();
            var tested = true;
            resource.Name = name;
            resource.ResourceType = resourceType;
            resource.IsTested = tested;
            Assert.That(resource.Name, Is.EqualTo(name));
            Assert.That(resource.ResourceType, Is.EqualTo(resourceType));
            Assert.That(resource.IsTested, Is.EqualTo(tested));
        }

        [Test]
        public void TestDepartmenCloudCreation()
        {
            var testDepartment = new DepartmentCloud();
            Assert.IsNotNull(testDepartment);
            Assert.IsEmpty(testDepartment.Resources);
            Assert.IsEmpty(testDepartment.Tasks);
        }

        [Test]
        public void TestLogTaskWorks()
        {
            string[] taskParams = { task.Priority.ToString(), task.Label, task.ResourceName };
            Assert.That(cloud.LogTask(taskParams), Is.EqualTo($"Task logged successfully."));
            Assert.That(cloud.Tasks.Count, Is.EqualTo(1));

            Assert.That(cloud.LogTask(taskParams), Is.EqualTo($"{task.ResourceName} is already logged."));
            Assert.That(cloud.Tasks.Count, Is.EqualTo(1));

            var lastTask = cloud.Tasks.Last();
            Assert.That(lastTask.Label, Is.EqualTo(task.Label));
            Assert.That(lastTask.ResourceName, Is.EqualTo(task.ResourceName));
            Assert.That(lastTask.Priority, Is.EqualTo(task.Priority));
        }

        private static readonly string[] EmptyParams = { };

        [Test]
        public void TestLogTaskThrows()
        {
            string[] emptyParams = { };
            string[] paramsWithNull = { GetRandomString(), null, GetRandomString() };
            string[] lessParams = { GetRandomString(), GetRandomString() };
            string[] greaterParams = { GetRandomString(), GetRandomString(), GetRandomString(), GetRandomString() };

            Assert.Throws<ArgumentException>(() => cloud.LogTask(emptyParams));
            Assert.Throws<ArgumentException>(() => cloud.LogTask(paramsWithNull));
            Assert.Throws<ArgumentException>(() => cloud.LogTask(lessParams));
            Assert.Throws<ArgumentException>(() => cloud.LogTask(greaterParams));
        }

        [Test]
        public void TestCreateResource()
        {
            Assert.IsFalse(cloud.CreateResource());

            string[] taskParams = { task.Priority.ToString(), task.Label, task.ResourceName };
            cloud.LogTask(taskParams);

            Assert.IsEmpty(cloud.Resources);
            
            Assert.IsTrue(cloud.CreateResource());
            Assert.IsEmpty(cloud.Tasks);
            Assert.That(cloud.Resources.Count, Is.EqualTo(1));

            var lastResource = cloud.Resources.Last();
            Assert.That(lastResource.Name, Is.EqualTo(task.ResourceName));
            Assert.That(lastResource.ResourceType, Is.EqualTo(task.Label));
        }

        [Test]
        public void TestTestResource()
        {
            string[] taskParams = { task.Priority.ToString(), task.Label, task.ResourceName };
            cloud.LogTask(taskParams);
            cloud.CreateResource();

            Assert.IsNull(cloud.TestResource(string.Empty));

            var testedResource = cloud.TestResource(task.ResourceName);
            Assert.IsNotNull(testedResource);
            Assert.IsTrue(testedResource.IsTested);
        }
    }
}