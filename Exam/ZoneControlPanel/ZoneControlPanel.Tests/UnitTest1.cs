using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace ZoneControlPanel.Tests
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

        private Employee employee;
        private SecureZone secureZone;
        private ControlPanel controlPanel;

        [SetUp]
        public void Setup()
        {
            var fullName = GetRandomString();
            var position = GetRandomString();
            var accessStamp = Random.Shared.Next();
            employee = new Employee(fullName, position, accessStamp);

            var name = GetRandomString();
            secureZone = new SecureZone(name);

            controlPanel = new ControlPanel(new string[] { name });
        }

        [Test]
        public void TestEmployee()
        {
            var fullName = GetRandomString();
            var position = GetRandomString();
            var accessStamp = Random.Shared.Next();
            var testEmployee = new Employee(fullName, position, accessStamp);
            Assert.IsNotNull(testEmployee);
            Assert.That(testEmployee.FullName, Is.EqualTo(fullName));
            Assert.That(testEmployee.Position, Is.EqualTo(position));
            Assert.That(testEmployee.AccessStamp, Is.EqualTo(accessStamp));
            Assert.That(testEmployee.ToString(), Is.EqualTo($"{testEmployee.AccessStamp} - ({testEmployee.Position}: {testEmployee.FullName})"));

            employee.FullName = fullName;
            employee.Position = position;
            employee.AccessStamp = accessStamp;
            Assert.That(employee.FullName, Is.EqualTo(fullName));
            Assert.That(employee.Position, Is.EqualTo(position));
            Assert.That(employee.AccessStamp, Is.EqualTo(accessStamp));
        }

        [Test]
        public void TestSecureZone()
        {
            var name = GetRandomString();
            var testSecureZone = new SecureZone(name);
            Assert.IsNotNull(testSecureZone);
            Assert.That(testSecureZone.Name, Is.EqualTo(name));
            Assert.IsEmpty(testSecureZone.AccessLog);

            secureZone.Name = name;
            Assert.That(secureZone.Name, Is.EqualTo(name));

            var list = new List<int>() { 0, 1 };
            secureZone.AccessLog = list;
            Assert.That(secureZone.AccessLog, Is.EqualTo(list));

            secureZone.AccessLog.Clear();
            Assert.IsEmpty(secureZone.AccessLog);

            secureZone.GrantAccess(employee);
            Assert.That(secureZone.AccessLog.Last(), Is.EqualTo(employee.AccessStamp));
        }

        [Test]
        public void TestControlPanelCreation()
        {
            var name = GetRandomString();
            var testControlPanel = new ControlPanel(new string[] { name });
            Assert.IsNotNull(testControlPanel);
            Assert.IsEmpty(controlPanel.Employees);
            Assert.That(testControlPanel.SecureZones.Count, Is.EqualTo(1));
            Assert.True(testControlPanel.SecureZones.Any(s => s.Name == name));
        }

        [Test]
        public void TestControlPanel_AddEmployee()
        {
            var employeeCount = controlPanel.Employees.Count;
            controlPanel.AddEmployee(employee);
            Assert.That(controlPanel.Employees.Count, Is.EqualTo(employeeCount + 1));
            Assert.That(controlPanel.Employees.Last(), Is.EqualTo(employee));

            employeeCount = controlPanel.Employees.Count;
            controlPanel.AddEmployee(employee);
            Assert.That(controlPanel.Employees.Count, Is.EqualTo(employeeCount));
        }

        [Test]
        public void TestControlPanel_AuthorizeEmployee_Works()
        {
            controlPanel.AddEmployee(employee);

            var emptyString = string.Empty;
            Assert.IsFalse(controlPanel.AuthorizeEmployee(employee.FullName, emptyString));
            Assert.IsFalse(controlPanel.AuthorizeEmployee(emptyString, secureZone.Name));

            Assert.IsTrue(controlPanel.AuthorizeEmployee(employee.FullName, secureZone.Name));

            var currentSecureZone = controlPanel.SecureZones.FirstOrDefault(s => s.Name == secureZone.Name);
            Assert.That(currentSecureZone!.AccessLog.Last(), Is.EqualTo(employee.AccessStamp));
        }

        [Test]
        public void TestControlPanel_AuthorizeEmployee_Throws()
        {
            controlPanel.AddEmployee(employee);
            controlPanel.AuthorizeEmployee(employee.FullName, secureZone.Name);
            Assert.Throws<InvalidOperationException>(() => controlPanel.AuthorizeEmployee(employee.FullName, secureZone.Name));
        }

        [Test]
        public void TestControlPanel_SecureZonesStatus()
        {
            var emptyString = string.Empty;
            Assert.That(controlPanel.SecureZonesStatus(emptyString), Is.EqualTo("Secure zone not found"));

            controlPanel.AddEmployee(employee);
            controlPanel.AuthorizeEmployee(employee.FullName, secureZone.Name);

            var result = controlPanel.SecureZonesStatus(secureZone.Name);
            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.StartsWith($"Secure zone: {secureZone.Name}"));
            Assert.IsTrue(result.Contains("Access log:"));
            Assert.IsTrue(result.Contains(employee.ToString().TrimEnd()));
        }
    }
}